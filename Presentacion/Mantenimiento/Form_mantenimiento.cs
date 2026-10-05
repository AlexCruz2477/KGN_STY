using System.Diagnostics;
using Npgsql;
using Nk_Colletion_New.Datos;

namespace Nk_Colletion_New;

public partial class Form_mantenimiento : Form
{
    public Form_mantenimiento()
    {
        InitializeComponent();
    }

    private void Form_mantenimiento_Load(object sender, EventArgs e)
    {
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
            proceso.ArgumentList.Add("-f");
            proceso.ArgumentList.Add(guardar.FileName);
            proceso.ArgumentList.Add(conexion.Database ?? "NK_COLLECTION");

            string error = await EjecutarProcesoAsync(proceso);

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
        using var abrir = new OpenFileDialog
        {
            Title = "Seleccionar copia de seguridad",
            Filter = "Archivo de respaldo PostgreSQL (*.backup)|*.backup"
        };

        if (abrir.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        var confirmar = MessageBox.Show(
            "¿Está seguro de que desea restaurar la base de datos?\n\n" +
            "Los datos actuales pueden ser reemplazados.",
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

            var proceso = CrearProcesoPostgreSql(pgRestore, conexion);
            proceso.ArgumentList.Add("-h");
            proceso.ArgumentList.Add(conexion.Host);
            proceso.ArgumentList.Add("-p");
            proceso.ArgumentList.Add(conexion.Port.ToString());
            proceso.ArgumentList.Add("-U");
            proceso.ArgumentList.Add(conexion.Username);
            proceso.ArgumentList.Add("-d");
            proceso.ArgumentList.Add(conexion.Database ?? "NK_COLLECTION");
            proceso.ArgumentList.Add("--clean");
            proceso.ArgumentList.Add("--if-exists");
            proceso.ArgumentList.Add("--no-owner");
            proceso.ArgumentList.Add(abrir.FileName);

            string error = await EjecutarProcesoAsync(proceso);

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
