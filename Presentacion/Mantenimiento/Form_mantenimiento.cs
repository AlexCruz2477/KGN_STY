using System.Diagnostics;
using System.Text.Json;
using Npgsql;
using Nk_Colletion_New.Datos;

namespace Nk_Colletion_New;

public partial class Form_mantenimiento : Form
{
    private sealed record RegistroRespaldo(
        DateTime Fecha,
        long TamanoSalida,
        long TamanoEntrada,
        long DatosExportados,
        long DatosImportados,
        string Tipo,
        string Archivo);

    private readonly string _archivoHistorial = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "NK_Collection", "historial_respaldo.json");

    public Form_mantenimiento()
    {
        InitializeComponent();
    }

    private void Form_mantenimiento_Load(object sender, EventArgs e)
    {
        cmbTipoRespaldo.SelectedIndex = 0;
        CargarHistorial();
    }

    private async void btn_Crear_Click(object sender, EventArgs e)
    {
        using var guardar = new SaveFileDialog
        {
            Title = "Guardar copia de seguridad",
            Filter = "Archivo de respaldo PostgreSQL (*.backup)|*.backup",
            FileName = $"NK_COLLECTION_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.backup"
        };

        if (guardar.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        try
        {
            btn_Crear.Enabled = false;
            string pgDump = BuscarHerramientaPostgreSql("pg_dump.exe");
            var conexion = new NpgsqlConnectionStringBuilder(AppConfig.CadenaConexion);

            var proceso = CrearProcesoPostgreSql(pgDump, conexion);
            proceso.ArgumentList.Add("-h");
            proceso.ArgumentList.Add(conexion.Host);
            proceso.ArgumentList.Add("-p");
            proceso.ArgumentList.Add(conexion.Port.ToString());
            proceso.ArgumentList.Add("-U");
            proceso.ArgumentList.Add(conexion.Username);
            proceso.ArgumentList.Add("-F");
            proceso.ArgumentList.Add("c");
            string tipoRespaldo = cmbTipoRespaldo.SelectedItem?.ToString() ?? "Inferencial";
            if (tipoRespaldo == "Incremental")
                proceso.ArgumentList.Add("--data-only");
            proceso.ArgumentList.Add("-f");
            proceso.ArgumentList.Add(guardar.FileName);
            proceso.ArgumentList.Add(conexion.Database ?? "NK_COLLECTION");

            long cantidadRegistros = await ContarRegistrosAsync(conexion);
            await EjecutarProcesoAsync(proceso);
            var registro = new RegistroRespaldo(
                DateTime.Now,
                new FileInfo(guardar.FileName).Length,
                0,
                cantidadRegistros,
                0,
                tipoRespaldo,
                guardar.FileName);
            File.WriteAllText(guardar.FileName + ".json", JsonSerializer.Serialize(registro));
            RegistrarRespaldo(registro);

            MessageBox.Show(
                "La copia de seguridad se creó correctamente.",
                "Respaldo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudo crear la copia de seguridad.\n\n" +
                ex.GetBaseException().Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            btn_Crear.Enabled = true;
        }
    }

    private async void btn_Restaurar_Click(object sender, EventArgs e)
    {
        if (guna2DataGridView1.CurrentRow is null || guna2DataGridView1.SelectedRows.Count == 0)
        {
            MessageBox.Show("Seleccione primero un respaldo del historial para restaurarlo.",
                "Restaurar respaldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        string archivo = Convert.ToString(guna2DataGridView1.CurrentRow.Cells[nameof(colArchivoRespaldo)].Value) ?? string.Empty;
        if (string.IsNullOrWhiteSpace(archivo))
        {
            using var abrir = new OpenFileDialog
            {
                Title = "Seleccionar el archivo del respaldo elegido",
                Filter = "Archivo de respaldo PostgreSQL (*.backup)|*.backup"
            };
            if (abrir.ShowDialog(this) != DialogResult.OK) return;
            archivo = abrir.FileName;
            guna2DataGridView1.CurrentRow.Cells[nameof(colArchivoRespaldo)].Value = archivo;
        }
        if (string.IsNullOrWhiteSpace(archivo) || !File.Exists(archivo))
        {
            MessageBox.Show("El respaldo de la fila seleccionada no está disponible. Seleccione un respaldo válido.",
                "Restaurar respaldo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string tipoSeleccionado = Convert.ToString(
            guna2DataGridView1.CurrentRow.Cells[nameof(colTipoRespaldo)].Value)
            ?? LeerTipoRespaldo(archivo)
            ?? "Inferencial";
        string mensajeConfirmacion = tipoSeleccionado == "Incremental"
            ? "El respaldo incremental contiene los datos completos, no solo cambios nuevos.\n\n" +
              "Para evitar claves duplicadas, se reemplazarán los datos actuales de las tablas conservando su estructura. ¿Desea continuar?"
            : $"¿Está seguro de que desea restaurar la base de datos con el respaldo {tipoSeleccionado}?\n\nLos datos actuales pueden ser reemplazados.";
        var confirmar = MessageBox.Show(
            mensajeConfirmacion,
            "Confirmar restauración",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirmar != DialogResult.Yes)
        {
            return;
        }

        try
        {
            btn_Restaurar.Enabled = false;
            string pgRestore = BuscarHerramientaPostgreSql("pg_restore.exe");
            var conexion = new NpgsqlConnectionStringBuilder(AppConfig.CadenaConexion);
            string tipo = tipoSeleccionado;

            var validar = new ProcessStartInfo
            {
                FileName = pgRestore,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };
            validar.ArgumentList.Add("--list");
            validar.ArgumentList.Add(archivo);
            await EjecutarProcesoAsync(validar);

            var proceso = CrearProcesoPostgreSql(pgRestore, conexion);
            proceso.ArgumentList.Add("-h");
            proceso.ArgumentList.Add(conexion.Host);
            proceso.ArgumentList.Add("-p");
            proceso.ArgumentList.Add(conexion.Port.ToString());
            proceso.ArgumentList.Add("-U");
            proceso.ArgumentList.Add(conexion.Username);
            proceso.ArgumentList.Add("-d");
            proceso.ArgumentList.Add(conexion.Database ?? "NK_COLLECTION");
            if (tipo is "Inferencial" or "Base de datos completa")
            {
                proceso.ArgumentList.Add("--clean");
                proceso.ArgumentList.Add("--if-exists");
                proceso.ArgumentList.Add("--single-transaction");
            }
            else
            {
                proceso.ArgumentList.Add("--data-only");
                proceso.ArgumentList.Add("--disable-triggers");
                proceso.ArgumentList.Add("--single-transaction");
            }
            proceso.ArgumentList.Add("--no-owner");
            proceso.ArgumentList.Add("--exit-on-error");
            proceso.ArgumentList.Add(archivo);

            if (tipo == "Incremental")
            {
                string respaldoPrevio = Path.Combine(Path.GetTempPath(), $"nk_collection_pre_restore_{Guid.NewGuid():N}.backup");
                try
                {
                    await CrearRespaldoCompletoAsync(conexion, respaldoPrevio);
                    await VaciarDatosActualesAsync(conexion);
                    await EjecutarProcesoAsync(proceso);
                }
                catch (Exception errorRestauracion)
                {
                    if (File.Exists(respaldoPrevio))
                    {
                        try
                        {
                            await VaciarDatosActualesAsync(conexion);
                            await RestaurarDatosDesdeRespaldoAsync(conexion, respaldoPrevio);
                        }
                        catch (Exception errorRecuperacion)
                        {
                            throw new AggregateException(
                                "Falló la restauración incremental y también la recuperación automática del respaldo de seguridad. " +
                                "No continúe usando la base de datos hasta verificarla.",
                                errorRestauracion,
                                errorRecuperacion);
                        }

                        throw new InvalidOperationException(
                            "La restauración incremental falló. Se recuperaron los datos que estaban en la base antes de intentarlo.",
                            errorRestauracion);
                    }

                    throw;
                }
                finally
                {
                    if (File.Exists(respaldoPrevio)) File.Delete(respaldoPrevio);
                }
            }
            else
            {
                await EjecutarProcesoAsync(proceso);
            }

            long cantidadRegistros = await ContarRegistrosAsync(conexion);
            long tamanoEntrada = new FileInfo(archivo).Length;
            RegistrarRespaldo(new RegistroRespaldo(
                DateTime.Now,
                0,
                tamanoEntrada,
                0,
                cantidadRegistros,
                tipo,
                archivo));

            MessageBox.Show(
                "La base de datos se restauró correctamente.",
                "Restauración",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudo restaurar la base de datos.\n\n" +
                ex.GetBaseException().Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            btn_Restaurar.Enabled = true;
        }
    }

    private static async Task VaciarDatosActualesAsync(NpgsqlConnectionStringBuilder conexion)
    {
        await using var conexionDb = new NpgsqlConnection(conexion.ConnectionString);
        await conexionDb.OpenAsync();
        await using var listarTablas = new NpgsqlCommand(
            "SELECT table_schema, table_name FROM information_schema.tables " +
            "WHERE table_schema = 'public' AND table_type = 'BASE TABLE'",
            conexionDb);
        await using var lector = await listarTablas.ExecuteReaderAsync();
        var tablas = new List<(string Esquema, string Nombre)>();
        while (await lector.ReadAsync())
            tablas.Add((lector.GetString(0), lector.GetString(1)));
        await lector.CloseAsync();

        if (tablas.Count == 0) return;
        var sqlBuilder = new NpgsqlCommandBuilder();
        string lista = string.Join(", ", tablas.Select(tabla =>
            $"{sqlBuilder.QuoteIdentifier(tabla.Esquema)}.{sqlBuilder.QuoteIdentifier(tabla.Nombre)}"));
        await using var vaciar = new NpgsqlCommand(
            $"TRUNCATE TABLE {lista} RESTART IDENTITY CASCADE",
            conexionDb);
        await vaciar.ExecuteNonQueryAsync();
    }

    private static async Task CrearRespaldoCompletoAsync(
        NpgsqlConnectionStringBuilder conexion,
        string archivo)
    {
        var proceso = CrearProcesoPostgreSql(BuscarHerramientaPostgreSql("pg_dump.exe"), conexion);
        proceso.ArgumentList.Add("-h");
        proceso.ArgumentList.Add(conexion.Host);
        proceso.ArgumentList.Add("-p");
        proceso.ArgumentList.Add(conexion.Port.ToString());
        proceso.ArgumentList.Add("-U");
        proceso.ArgumentList.Add(conexion.Username);
        proceso.ArgumentList.Add("-F");
        proceso.ArgumentList.Add("c");
        proceso.ArgumentList.Add("-f");
        proceso.ArgumentList.Add(archivo);
        proceso.ArgumentList.Add(conexion.Database ?? "NK_COLLECTION");
        await EjecutarProcesoAsync(proceso);
    }

    private static async Task RestaurarDatosDesdeRespaldoAsync(
        NpgsqlConnectionStringBuilder conexion,
        string archivo)
    {
        var proceso = CrearProcesoPostgreSql(BuscarHerramientaPostgreSql("pg_restore.exe"), conexion);
        proceso.ArgumentList.Add("-h");
        proceso.ArgumentList.Add(conexion.Host);
        proceso.ArgumentList.Add("-p");
        proceso.ArgumentList.Add(conexion.Port.ToString());
        proceso.ArgumentList.Add("-U");
        proceso.ArgumentList.Add(conexion.Username);
        proceso.ArgumentList.Add("-d");
        proceso.ArgumentList.Add(conexion.Database ?? "NK_COLLECTION");
        proceso.ArgumentList.Add("--data-only");
        proceso.ArgumentList.Add("--disable-triggers");
        proceso.ArgumentList.Add("--single-transaction");
        proceso.ArgumentList.Add("--no-owner");
        proceso.ArgumentList.Add("--exit-on-error");
        proceso.ArgumentList.Add(archivo);
        await EjecutarProcesoAsync(proceso);
    }

    private void CargarHistorial()
    {
        guna2DataGridView1.Rows.Clear();
        if (!File.Exists(_archivoHistorial)) return;
        try
        {
            var registros = JsonSerializer.Deserialize<List<RegistroRespaldo>>(File.ReadAllText(_archivoHistorial)) ?? new();
            foreach (var registro in registros.OrderByDescending(r => r.Fecha))
                guna2DataGridView1.Rows.Add(registro.Fecha.ToString("dd/MM/yyyy HH:mm:ss"),
                    FormatearTamano(registro.TamanoSalida), FormatearTamano(registro.TamanoEntrada),
                    registro.DatosExportados, registro.DatosImportados, registro.Tipo, registro.Archivo);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo cargar el historial de respaldos. {ex.Message}",
                "Historial de mantenimiento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void RegistrarRespaldo(RegistroRespaldo registro)
    {
        try
        {
            string? directorio = Path.GetDirectoryName(_archivoHistorial);
            if (!string.IsNullOrWhiteSpace(directorio)) Directory.CreateDirectory(directorio);
            var registros = File.Exists(_archivoHistorial)
                ? JsonSerializer.Deserialize<List<RegistroRespaldo>>(File.ReadAllText(_archivoHistorial)) ?? new()
                : new List<RegistroRespaldo>();
            registros.Add(registro);
            File.WriteAllText(_archivoHistorial, JsonSerializer.Serialize(registros));
            CargarHistorial();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"La operación se completó, pero no se pudo guardar el historial. {ex.Message}",
                "Historial de mantenimiento", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private static async Task<long> ContarRegistrosAsync(NpgsqlConnectionStringBuilder conexion)
    {
        await using var conexionDb = new NpgsqlConnection(conexion.ConnectionString);
        await conexionDb.OpenAsync();
        await using var comandoTablas = new NpgsqlCommand(
            "SELECT table_schema, table_name FROM information_schema.tables WHERE table_schema NOT IN ('information_schema', 'pg_catalog') AND table_type = 'BASE TABLE'",
            conexionDb);
        await using var lector = await comandoTablas.ExecuteReaderAsync();
        var tablas = new List<(string Esquema, string Tabla)>();
        while (await lector.ReadAsync())
            tablas.Add((lector.GetString(0), lector.GetString(1)));
        await lector.CloseAsync();

        long total = 0;
        var builder = new NpgsqlCommandBuilder();
        foreach (var tabla in tablas)
        {
            string consulta = $"SELECT COUNT(*) FROM {builder.QuoteIdentifier(tabla.Esquema)}.{builder.QuoteIdentifier(tabla.Tabla)}";
            await using var comando = new NpgsqlCommand(consulta, conexionDb);
            total += Convert.ToInt64(await comando.ExecuteScalarAsync());
        }
        return total;
    }

    private static string? LeerTipoRespaldo(string archivo)
    {
        string metadata = archivo + ".json";
        if (!File.Exists(metadata)) return null;
        try
        {
            using var documento = JsonDocument.Parse(File.ReadAllText(metadata));
            return documento.RootElement.TryGetProperty("Tipo", out var tipo) ? tipo.GetString() : null;
        }
        catch { return null; }
    }

    private static string FormatearTamano(long bytes)
    {
        if (bytes <= 0) return "—";
        string[] unidades = { "B", "KB", "MB", "GB" };
        double tamano = bytes;
        int indice = 0;
        while (tamano >= 1024 && indice < unidades.Length - 1) { tamano /= 1024; indice++; }
        return $"{tamano:N2} {unidades[indice]}";
    }

    private static ProcessStartInfo CrearProcesoPostgreSql(
        string ejecutable,
        NpgsqlConnectionStringBuilder conexion)
    {
        var proceso = new ProcessStartInfo
        {
            FileName = ejecutable,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true
        };

        if (!string.IsNullOrWhiteSpace(conexion.Password))
        {
            proceso.Environment["PGPASSWORD"] = conexion.Password;
        }

        return proceso;
    }

    private static async Task<string> EjecutarProcesoAsync(ProcessStartInfo inicio)
    {
        using var proceso = Process.Start(inicio)
            ?? throw new InvalidOperationException(
                "No se pudo iniciar la herramienta de PostgreSQL.");

        Task<string> stderr = proceso.StandardError.ReadToEndAsync();
        Task<string> stdout = proceso.StandardOutput.ReadToEndAsync();

        await proceso.WaitForExitAsync();
        string error = await stderr;
        _ = await stdout;

        if (proceso.ExitCode != 0)
        {
            throw new InvalidOperationException(
                string.IsNullOrWhiteSpace(error)
                    ? $"PostgreSQL finalizó con código {proceso.ExitCode}."
                    : error.Trim());
        }

        return error;
    }

    private static string BuscarHerramientaPostgreSql(string ejecutable)
    {
        string? desdePath = (Environment.GetEnvironmentVariable("PATH") ?? string.Empty)
            .Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries)
            .Select(ruta => Path.Combine(ruta.Trim(), ejecutable))
            .FirstOrDefault(File.Exists);

        if (!string.IsNullOrWhiteSpace(desdePath))
        {
            return desdePath;
        }

        foreach (string variable in new[] { "ProgramFiles", "ProgramFiles(x86)" })
        {
            string? baseProgramas = Environment.GetEnvironmentVariable(variable);
            if (string.IsNullOrWhiteSpace(baseProgramas))
            {
                continue;
            }

            string raiz = Path.Combine(baseProgramas, "PostgreSQL");
            if (!Directory.Exists(raiz))
            {
                continue;
            }

            string? encontrada = Directory
                .EnumerateDirectories(raiz)
                .OrderByDescending(ruta => ruta)
                .Select(ruta => Path.Combine(ruta, "bin", ejecutable))
                .FirstOrDefault(File.Exists);

            if (!string.IsNullOrWhiteSpace(encontrada))
            {
                return encontrada;
            }
        }

        throw new FileNotFoundException(
            $"No se encontró {ejecutable}. Instale las herramientas de PostgreSQL " +
            "o agregue su carpeta bin al PATH de Windows.");
    }

    private void panel2_Paint(object sender, PaintEventArgs e)
    {
    }
}
