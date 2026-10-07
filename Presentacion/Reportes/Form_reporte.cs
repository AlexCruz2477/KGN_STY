using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Nk_Colletion_New
{
    public partial class Form_reporte : Form
    {
        private sealed record OpcionReporte(int Id, string Nombre);
        private sealed record FilaReporte(string[] Valores);
        private static readonly CultureInfo CulturaNicaragua = CultureInfo.GetCultureInfo("es-NI");

        public Form_reporte()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            btn_Ventas.Click += async (_, _) => await GenerarVentasAsync();
            button1.Click += async (_, _) => await GenerarInventarioAsync();
            button2.Click += async (_, _) => await GenerarComprasAsync();
            btnCajaGenerar.Click += async (_, _) => await GenerarCajaAsync();
            btnActualizarHistorial.Click += async (_, _) => await CargarRegistrosAsync("Caja");
            btnReportes.Click += async (_, _) => await CargarRegistrosAsync("Caja");
            btnCaja.Click += async (_, _) => await CargarRegistrosAsync("Caja");
            btnVentasModulo.Click += async (_, _) => await CargarRegistrosAsync("Ventas");
            btnComprasModulo.Click += async (_, _) => await CargarRegistrosAsync("Compras");
            btnInventarioModulo.Click += async (_, _) => await CargarRegistrosAsync("Inventario");
            comboBox1.SelectedValueChanged += async (_, _) => await CargarRegistrosAsync("Ventas");
            comboBox3.SelectedValueChanged += async (_, _) => await CargarRegistrosAsync("Compras");
            dateTimePicker1.ValueChanged += async (_, _) => await ActualizarModuloSeleccionadoAsync();
            dateTimePicker2.ValueChanged += async (_, _) => await ActualizarModuloSeleccionadoAsync();
        }

        private async Task ActualizarModuloSeleccionadoAsync()
        {
            string modulo = lblTituloReportes.Text.Split('·').LastOrDefault()?.Trim() ?? "Caja";
            if (modulo is not ("Caja" or "Ventas" or "Compras" or "Inventario")) modulo = "Caja";
            await CargarRegistrosAsync(modulo);
        }

        private async void Form_reporte_Load(object sender, EventArgs e)
        {
            if (AppConfig.DbOptions is null)
            {
                MostrarErrorCarga("La conexión a la base de datos no está inicializada.");
                return;
            }

            try
            {
                await using var contexto = new NkCollectionContext(AppConfig.DbOptions);
                var usuarios = await contexto.Usuarios.AsNoTracking()
                    .OrderBy(usuario => usuario.Nombre).ThenBy(usuario => usuario.Apellido)
                    .Select(usuario => new OpcionReporte(usuario.IdUsuario,
                        (usuario.Nombre + " " + usuario.Apellido).Trim() + " · " + usuario.Usuario1)).ToListAsync();
                var proveedores = await contexto.Proveedors.AsNoTracking().OrderBy(proveedor => proveedor.Nombre)
                    .Select(proveedor => new OpcionReporte(proveedor.IdProveedor, proveedor.Nombre)).ToListAsync();
                EnlazarOpciones(comboBox1, usuarios, "Todos los usuarios");
                EnlazarOpciones(comboBox3, proveedores, "Todos los proveedores");
                await CargarRegistrosAsync("Caja");
            }
            catch (Exception ex)
            {
                MostrarErrorCarga($"No se pudieron cargar usuarios y proveedores. {ex.GetBaseException().Message}");
            }
        }

        private static void EnlazarOpciones(ComboBox combo, List<OpcionReporte> opciones, string textoTodos)
        {
            opciones.Insert(0, new OpcionReporte(0, textoTodos));
            combo.DataSource = null;
            combo.DisplayMember = nameof(OpcionReporte.Nombre);
            combo.ValueMember = nameof(OpcionReporte.Id);
            combo.DataSource = opciones;
        }

        private void MostrarErrorCarga(string mensaje) =>
            MessageBox.Show(mensaje, "Filtros de reportes", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private async Task CargarRegistrosAsync(string modulo)
        {
            if (AppConfig.DbOptions is null || !IsHandleCreated) return;
            try
            {
                await using var db = new NkCollectionContext(AppConfig.DbOptions);
                dgvReportes.Rows.Clear();
                DateTime desde = dateTimePicker1.Value.Date;
                DateTime hasta = dateTimePicker2.Value.Date.AddDays(1);

                if (modulo == "Caja")
                {
                    var aperturas = await db.AperturaCajas.AsNoTracking()
                        .Include(a => a.IdCajaNavigation).ThenInclude(c => c.IdUsuarioNavigation)
                        .Include(a => a.ArqueoCajas)
                        .Where(a => a.FechaApertura >= desde && a.FechaApertura < hasta)
                        .OrderByDescending(a => a.FechaApertura).ToListAsync();
                    foreach (var apertura in aperturas)
                    {
                        var usuario = apertura.IdCajaNavigation.IdUsuarioNavigation;
                        dgvReportes.Rows.Add("Caja", apertura.FechaApertura?.ToString("dd/MM/yyyy HH:mm") ?? "—",
                            $"{usuario.Nombre} {usuario.Apellido}", "—",
                            $"{apertura.IdCajaNavigation.NumeroCaja} · {apertura.ArqueoCajas.Count} arqueo(s)");
                    }
                }
                else if (modulo == "Ventas")
                {
                    var consulta = db.Venta.AsNoTracking().Include(v => v.IdAperturaCajaNavigation)
                        .ThenInclude(a => a.IdCajaNavigation).ThenInclude(c => c.IdUsuarioNavigation)
                        .Where(v => v.FechaVenta >= desde && v.FechaVenta < hasta && v.Estado != false);
                    if (comboBox1.SelectedValue is int idUsuario && idUsuario > 0)
                        consulta = consulta.Where(v => v.IdAperturaCajaNavigation.IdCajaNavigation.IdUsuario == idUsuario);
                    foreach (var venta in await consulta.OrderByDescending(v => v.FechaVenta).ToListAsync())
                    {
                        var usuario = venta.IdAperturaCajaNavigation.IdCajaNavigation.IdUsuarioNavigation;
                        dgvReportes.Rows.Add("Ventas", venta.FechaVenta?.ToString("dd/MM/yyyy HH:mm") ?? "—",
                            $"{usuario.Nombre} {usuario.Apellido}", "—", venta.NumeroComprobante ?? $"V-{venta.IdVenta:D6}");
                    }
                }
                else if (modulo == "Compras")
                {
                    var consulta = db.Compras.AsNoTracking().Include(c => c.IdProveedorNavigation)
                        .Include(c => c.IdUsuarioNavigation)
                        .Where(c => c.FechaCompra >= desde && c.FechaCompra < hasta && c.Estado != false);
                    if (comboBox3.SelectedValue is int idProveedor && idProveedor > 0)
                        consulta = consulta.Where(c => c.IdProveedor == idProveedor);
                    foreach (var compra in await consulta.OrderByDescending(c => c.FechaCompra).ToListAsync())
                        dgvReportes.Rows.Add("Compras", compra.FechaCompra?.ToString("dd/MM/yyyy HH:mm") ?? "—",
                            $"{compra.IdUsuarioNavigation.Nombre} {compra.IdUsuarioNavigation.Apellido}",
                            compra.IdProveedorNavigation.Nombre, compra.NumeroFactura ?? $"C-{compra.IdCompra:D6}");
                }
                else
                {
                    var variantes = await db.ProductoVariantes.AsNoTracking().Include(v => v.IdProductoNavigation)
                        .Where(v => v.Estado != false && v.IdProductoNavigation.Estado != false)
                        .OrderBy(v => v.IdProductoNavigation.NombreProducto).ToListAsync();
                    foreach (var variante in variantes)
                        dgvReportes.Rows.Add("Inventario", DateTime.Now.ToString("dd/MM/yyyy"), "—", "—",
                            $"{variante.IdProductoNavigation.NombreProducto} · Stock: {variante.StockActual}");
                }

                lblTituloReportes.Text = $"Reportes · {modulo}";
                dgvReportes.ClearSelection();
            }
            catch (Exception ex)
            {
                MostrarErrorCarga($"No se pudieron cargar los registros de {modulo}. {ex.GetBaseException().Message}");
            }
        }

        private async Task GenerarCajaAsync()
        {
            if (!TryObtenerRango(dateTimePicker1.Value, dateTimePicker2.Value, out var desde, out var hasta)) return;
            if (AppConfig.DbOptions is null) return;
            try
            {
                await using var db = new NkCollectionContext(AppConfig.DbOptions);
                var aperturas = await db.AperturaCajas.AsNoTracking()
                    .Include(a => a.IdCajaNavigation).ThenInclude(c => c.IdUsuarioNavigation)
                    .Include(a => a.ArqueoCajas).Include(a => a.Egresos)
                    .Where(a => a.FechaApertura >= desde && a.FechaApertura < hasta)
                    .OrderBy(a => a.FechaApertura).ToListAsync();
                var filas = aperturas.Select(a => new FilaReporte(new[]
                {
                    a.IdCajaNavigation.NumeroCaja,
                    a.FechaApertura?.ToString("dd/MM/yyyy HH:mm") ?? "—",
                    a.FechaCierre?.ToString("dd/MM/yyyy HH:mm") ?? "Abierta",
                    $"{a.IdCajaNavigation.IdUsuarioNavigation.Nombre} {a.IdCajaNavigation.IdUsuarioNavigation.Apellido}",
                    (a.MontoApertura ?? 0m).ToString("C2", CulturaNicaragua),
                    a.ArqueoCajas.Sum(arqueo => arqueo.TotalVentas).ToString("C2", CulturaNicaragua),
                    a.Egresos.Sum(egreso => egreso.Monto).ToString("C2", CulturaNicaragua),
                    a.ArqueoCajas.LastOrDefault()?.SaldoContado.ToString("C2", CulturaNicaragua) ?? "Sin arqueo"
                })).ToList();
                await CrearPdfAsync("Reporte de caja",
                    new[] { "Caja", "Apertura", "Cierre", "Usuario", "Monto apertura", "Ventas", "Egresos", "Saldo contado" }, filas,
                    new[] { ("Aperturas", aperturas.Count.ToString("N0", CulturaNicaragua)),
                        ("Arqueos", aperturas.Sum(a => a.ArqueoCajas.Count).ToString("N0", CulturaNicaragua)),
                        ("Total ventas", aperturas.Sum(a => a.ArqueoCajas.Sum(arqueo => arqueo.TotalVentas)).ToString("C2", CulturaNicaragua)),
                        ("Total egresos", aperturas.Sum(a => a.Egresos.Sum(egreso => egreso.Monto)).ToString("C2", CulturaNicaragua)) },
                    desde, hasta.AddDays(-1));
                await CargarRegistrosAsync("Caja");
            }
            catch (Exception ex) { MostrarErrorReporte(ex); }
        }

        private async Task GenerarVentasAsync()
        {
            if (!TryObtenerRango(dateTimePicker1.Value, dateTimePicker2.Value, out var desde, out var hasta)) return;
            if (AppConfig.DbOptions is null) return;

            try
            {
                await using var db = new NkCollectionContext(AppConfig.DbOptions);
                var consulta = db.Venta.AsNoTracking()
                    .Include(v => v.IdClienteNavigation)
                    .Include(v => v.IdAperturaCajaNavigation)
                        .ThenInclude(a => a.IdCajaNavigation)
                    .Include(v => v.DetalleVenta)
                        .ThenInclude(d => d.IdVarianteNavigation)
                            .ThenInclude(variante => variante.IdProductoNavigation)
                    .Include(v => v.PagoVenta)
                        .ThenInclude(p => p.IdMetodoPagoNavigation)
                    .Where(v => v.Estado != false && v.FechaVenta >= desde && v.FechaVenta < hasta);

                if (comboBox1.SelectedValue is int idUsuario && idUsuario > 0)
                    consulta = consulta.Where(v => v.IdAperturaCajaNavigation.IdCajaNavigation.IdUsuario == idUsuario);

                var ventas = await consulta.OrderBy(v => v.FechaVenta).ToListAsync();
                var filas = ventas.SelectMany(v => v.DetalleVenta.Select(d => new FilaReporte(new[]
                {
                    v.NumeroComprobante ?? $"V-{v.IdVenta:D6}",
                    v.FechaVenta?.ToString("dd/MM/yyyy HH:mm") ?? "—",
                    v.IdClienteNavigation is null ? "Venta general" : $"{v.IdClienteNavigation.Nombre} {v.IdClienteNavigation.Apellido}",
                    d.IdVarianteNavigation.IdProductoNavigation.NombreProducto,
                    d.Cantidad.ToString("N0", CulturaNicaragua),
                    d.PrecioUnitario.ToString("C2", CulturaNicaragua),
                    (d.Cantidad * d.PrecioUnitario).ToString("C2", CulturaNicaragua),
                    string.Join(", ", v.PagoVenta.Select(p => $"{p.IdMetodoPagoNavigation.Nombre}: {p.Monto.ToString("C2", CulturaNicaragua)}")),
                    v.TotalVenta.ToString("C2", CulturaNicaragua)
                }))).ToList();

                await CrearPdfAsync("Reporte de ventas", new[] { "Comprobante", "Fecha", "Cliente", "Producto", "Cant.", "Precio", "Subtotal", "Pago", "Total" }, filas,
                    new[] { ("Ventas", ventas.Count.ToString("N0", CulturaNicaragua)), ("Unidades", ventas.Sum(v => v.DetalleVenta.Sum(d => d.Cantidad)).ToString("N0", CulturaNicaragua)), ("Descuentos", ventas.Sum(v => v.Descuento).ToString("C2", CulturaNicaragua)), ("Total vendido", ventas.Sum(v => v.TotalVenta).ToString("C2", CulturaNicaragua)) }, desde, hasta.AddDays(-1));
            }
            catch (Exception ex) { MostrarErrorReporte(ex); }
        }

        private async Task GenerarComprasAsync()
        {
            if (!TryObtenerRango(dateTimePicker1.Value, dateTimePicker2.Value, out var desde, out var hasta)) return;
            if (AppConfig.DbOptions is null) return;

            try
            {
                await using var db = new NkCollectionContext(AppConfig.DbOptions);
                var consulta = db.Compras.AsNoTracking()
                    .Include(c => c.IdProveedorNavigation)
                    .Include(c => c.IdUsuarioNavigation)
                    .Include(c => c.DetalleCompras)
                        .ThenInclude(d => d.IdVarianteNavigation)
                            .ThenInclude(variante => variante.IdProductoNavigation)
                    .Where(c => c.Estado != false && c.FechaCompra >= desde && c.FechaCompra < hasta);
                if (comboBox3.SelectedValue is int idProveedor && idProveedor > 0)
                    consulta = consulta.Where(c => c.IdProveedor == idProveedor);

                var compras = await consulta.OrderBy(c => c.FechaCompra).ToListAsync();
                var filas = compras.SelectMany(c => c.DetalleCompras.Select(d => new FilaReporte(new[]
                {
                    c.NumeroFactura ?? $"C-{c.IdCompra:D6}",
                    c.FechaCompra?.ToString("dd/MM/yyyy") ?? "—",
                    c.IdProveedorNavigation.Nombre,
                    $"{c.IdUsuarioNavigation.Nombre} {c.IdUsuarioNavigation.Apellido}",
                    d.IdVarianteNavigation.IdProductoNavigation.NombreProducto,
                    d.Cantidad.ToString("N0", CulturaNicaragua),
                    d.PrecioUnitario.ToString("C2", CulturaNicaragua),
                    (d.Subtotal ?? d.Cantidad * d.PrecioUnitario).ToString("C2", CulturaNicaragua),
                    c.Total.ToString("C2", CulturaNicaragua)
                }))).ToList();

                await CrearPdfAsync("Reporte de compras", new[] { "Factura", "Fecha", "Proveedor", "Usuario", "Producto", "Cant.", "Costo", "Subtotal", "Total" }, filas,
                    new[] { ("Compras", compras.Count.ToString("N0", CulturaNicaragua)), ("Unidades", compras.Sum(c => c.DetalleCompras.Sum(d => d.Cantidad)).ToString("N0", CulturaNicaragua)), ("Impuestos", compras.Sum(c => c.Impuesto).ToString("C2", CulturaNicaragua)), ("Total comprado", compras.Sum(c => c.Total).ToString("C2", CulturaNicaragua)) }, desde, hasta.AddDays(-1));
            }
            catch (Exception ex) { MostrarErrorReporte(ex); }
        }

        private async Task GenerarInventarioAsync()
        {
            if (AppConfig.DbOptions is null) return;

            try
            {
                await using var db = new NkCollectionContext(AppConfig.DbOptions);
                var variantes = await db.ProductoVariantes.AsNoTracking()
                    .Include(v => v.IdProductoNavigation)
                    .Include(v => v.IdTallaNavigation)
                    .Include(v => v.IdColorNavigation)
                    .Where(v => v.Estado != false && v.IdProductoNavigation.Estado != false)
                    .OrderBy(v => v.IdProductoNavigation.NombreProducto)
                    .ThenBy(v => v.Codigo)
                    .ToListAsync();
                var filas = variantes.Select(v => new FilaReporte(new[]
                {
                    v.Codigo,
                    v.IdProductoNavigation.NombreProducto,
                    v.IdTallaNavigation?.NombreTalla ?? "—",
                    v.IdColorNavigation?.NombreColor ?? "—",
                    v.StockActual.ToString("N0", CulturaNicaragua),
                    v.StockMinimo.ToString("N0", CulturaNicaragua),
                    v.PrecioCompra.ToString("C2", CulturaNicaragua),
                    v.PrecioVenta.ToString("C2", CulturaNicaragua),
                    (v.StockActual * v.PrecioCompra).ToString("C2", CulturaNicaragua)
                })).ToList();

                await CrearPdfAsync("Reporte de inventario", new[] { "Código", "Producto", "Talla", "Color", "Stock", "Mínimo", "Costo unitario", "Precio venta", "Valor a costo" }, filas,
                    new[] { ("Variantes activas", variantes.Count.ToString("N0", CulturaNicaragua)), ("Unidades", variantes.Sum(v => v.StockActual).ToString("N0", CulturaNicaragua)), ("Bajo mínimo", variantes.Count(v => v.StockActual <= v.StockMinimo).ToString("N0", CulturaNicaragua)), ("Valor total a costo", variantes.Sum(v => v.StockActual * v.PrecioCompra).ToString("C2", CulturaNicaragua)) }, null, null);
            }
            catch (Exception ex) { MostrarErrorReporte(ex); }
        }

        private static bool TryObtenerRango(DateTime fechaInicio, DateTime fechaFin, out DateTime desde, out DateTime hasta)
        {
            desde = DateTime.SpecifyKind(fechaInicio.Date, DateTimeKind.Unspecified);
            hasta = DateTime.SpecifyKind(fechaFin.Date.AddDays(1), DateTimeKind.Unspecified);
            if (fechaInicio.Date <= fechaFin.Date) return true;
            MessageBox.Show("La fecha inicial no puede ser posterior a la fecha final.", "Rango de fechas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private async Task CrearPdfAsync(string titulo, string[] columnas, IReadOnlyCollection<FilaReporte> filas,
            IReadOnlyCollection<(string Etiqueta, string Valor)> resumen, DateTime? desde, DateTime? hasta)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            using var logoStream = new MemoryStream();
            Properties.Resources.imagen_circular_recortada.Save(logoStream, System.Drawing.Imaging.ImageFormat.Png);
            var documento = new DocumentoReporte(titulo, DateTime.Now, columnas, filas, resumen, desde, hasta, logoStream.ToArray());
            string archivoTemporal = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.pdf");
            await Task.Run(() => documento.GeneratePdf(archivoTemporal));

            using var dialogo = new SaveFileDialog
            {
                Title = "Guardar reporte PDF",
                Filter = "Archivo PDF (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                AddExtension = true,
                FileName = $"{titulo.Replace(' ', '_')}_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            };
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(archivoTemporal) { UseShellExecute = true });
            if (MessageBox.Show(
                    this,
                    "Se abrió el reporte para revisarlo. ¿Desea guardar una copia del PDF?",
                    "Revisar reporte",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            string archivoVista = dialogo.ShowDialog(this) == DialogResult.OK
                ? GuardarCopiaPdf(archivoTemporal, dialogo.FileName)
                : archivoTemporal;
            if (archivoVista != archivoTemporal)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(archivoVista) { UseShellExecute = true });
            }
        }

        private static string GuardarCopiaPdf(string origen, string destino)
        {
            File.Copy(origen, destino, true);
            return destino;
        }

        private static void MostrarErrorReporte(Exception ex) =>
            MessageBox.Show(ex.GetBaseException().Message, "No se pudo generar el reporte", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private sealed class DocumentoReporte : IDocument
        {
            private readonly string _titulo;
            private readonly DateTime _generado;
            private readonly string[] _columnas;
            private readonly IReadOnlyCollection<FilaReporte> _filas;
            private readonly IReadOnlyCollection<(string Etiqueta, string Valor)> _resumen;
            private readonly DateTime? _desde;
            private readonly DateTime? _hasta;
            private readonly byte[] _logo;

            public DocumentoReporte(string titulo, DateTime generado, string[] columnas, IReadOnlyCollection<FilaReporte> filas,
                IReadOnlyCollection<(string Etiqueta, string Valor)> resumen, DateTime? desde, DateTime? hasta, byte[] logo)
            {
                _titulo = titulo;
                _generado = generado;
                _columnas = columnas;
                _filas = filas;
                _resumen = resumen;
                _desde = desde;
                _hasta = hasta;
                _logo = logo;
            }

            public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

            public void Compose(IDocumentContainer container)
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(28);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(style => style.FontFamily("Lato").FontSize(8).FontColor("#292A28"));
                    page.Header().Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            row.ConstantItem(58).Height(58).Image(_logo).FitArea();
                            row.RelativeItem().Column(title =>
                            {
                                title.Item().Text("NK STYLE POINT").FontSize(10).SemiBold().FontColor("#8C6A38");
                                title.Item().Text(_titulo).FontSize(21).Bold().FontColor("#292A28");
                                title.Item().Text(_desde.HasValue && _hasta.HasValue
                                    ? $"Período: {_desde:dd/MM/yyyy} — {_hasta:dd/MM/yyyy}  |  Generado: {_generado:dd/MM/yyyy HH:mm}"
                                    : $"Inventario actual  |  Generado: {_generado:dd/MM/yyyy HH:mm}").FontSize(8).FontColor("#77746D");
                            });
                            row.RelativeItem().AlignRight().AlignMiddle().Text("REPORTE ADMINISTRATIVO").FontSize(9).Bold().FontColor("#65705A");
                        });
                        column.Item().PaddingTop(10).LineHorizontal(2).LineColor("#B89555");
                    });
                    page.Content().PaddingVertical(12).Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            foreach (var item in _resumen)
                                row.RelativeItem().PaddingRight(6).Border(1).BorderColor("#D6CCBE").Background("#F8F4EC").Padding(9).Column(card =>
                                {
                                    card.Item().Text(item.Etiqueta.ToUpperInvariant()).FontSize(7).SemiBold().FontColor("#77746D");
                                    card.Item().PaddingTop(4).Text(item.Valor).FontSize(12).Bold().FontColor("#3F4140");
                                });
                        });

                        if (_filas.Count == 0)
                        {
                            column.Item().PaddingTop(14).Border(1).BorderColor("#D6CCBE").Background("#F8F4EC").Padding(14)
                                .AlignCenter().Text("No se encontraron registros para los filtros seleccionados.").Italic().FontColor("#77746D");
                        }
                        else
                        {
                            column.Item().PaddingTop(14).Table(table =>
                            {
                                table.ColumnsDefinition(definition =>
                                {
                                    foreach (var _ in _columnas) definition.RelativeColumn();
                                });
                                table.Header(header =>
                                {
                                    foreach (string tituloColumna in _columnas)
                                        header.Cell().Background("#3F4140").Padding(6).Text(tituloColumna).FontSize(7).Bold().FontColor(Colors.White);
                                });
                                int indice = 0;
                                foreach (var fila in _filas)
                                {
                                    string fondo = indice++ % 2 == 0 ? "#FFFFFF" : "#F5F1E8";
                                    foreach (string valor in fila.Valores)
                                        table.Cell().Background(fondo).BorderBottom(0.5f).BorderColor("#D6CCBE").Padding(5).Text(valor).FontSize(7);
                                }
                            });
                        }
                    });
                    page.Footer().Row(row =>
                    {
                        row.RelativeItem().Text("Documento generado por NK Style Point").FontSize(7).FontColor("#77746D");
                        row.AutoItem().Text(text =>
                        {
                            text.Span("Página ").FontSize(7).FontColor("#77746D");
                            text.CurrentPageNumber().FontSize(7).FontColor("#77746D");
                            text.Span(" de ").FontSize(7).FontColor("#77746D");
                            text.TotalPages().FontSize(7).FontColor("#77746D");
                        });
                    });
                });
            }
        }
    }
}
