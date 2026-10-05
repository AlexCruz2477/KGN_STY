using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Negocios.Servicios.Principal;

namespace Nk_Colletion_New;

internal sealed partial class DashboardInicio : UserControl
{
    private static readonly Color Texto = Color.FromArgb(41, 42, 40);
    private static readonly Color Secundario = Color.FromArgb(119, 116, 109);
    private static readonly Color Oro = Color.FromArgb(184, 149, 85);
    private static readonly Color Verde = Color.FromArgb(101, 112, 90);

    private readonly string _nombreUsuario;
    private readonly int _idAperturaCaja;
    private readonly Dashboard_Service? _servicio;
    private readonly Func<string, bool> _puedeAcceder;
    private readonly Action _abrirVentas;
    private readonly Action _abrirCompras;
    private readonly Action _abrirProductos;
    private readonly Action _abrirClientes;
    private readonly Action _abrirCaja;
    private bool _cargando;
    private Control[] _kpiCards = Array.Empty<Control>();
    private Control[] _graficas = Array.Empty<Control>();
    private bool _permisoVentas;
    private bool _permisoCompras;
    private bool _permisoProductos;
    private bool _permisoClientes;
    private bool _permisoCaja;
    private IReadOnlyList<VentaDiariaResumen> _ventasSemana = Array.Empty<VentaDiariaResumen>();
    private IReadOnlyList<VentaDiariaResumen> _comprasSemana = Array.Empty<VentaDiariaResumen>();

    public DashboardInicio()
        : this(
            new UsuarioSesion(),
            0,
            null,
            _ => true,
            () => { },
            () => { },
            () => { },
            () => { },
            () => { })
    {
    }

    public DashboardInicio(
        UsuarioSesion sesion,
        int idAperturaCaja,
        Dashboard_Service? servicio,
        Func<string, bool> puedeAcceder,
        Action abrirVentas,
        Action abrirCompras,
        Action abrirProductos,
        Action abrirClientes,
        Action abrirCaja)
    {
        _nombreUsuario = sesion.NombreCompleto;
        _idAperturaCaja = idAperturaCaja;
        _servicio = servicio;
        _puedeAcceder = puedeAcceder;
        _abrirVentas = abrirVentas;
        _abrirCompras = abrirCompras;
        _abrirProductos = abrirProductos;
        _abrirClientes = abrirClientes;
        _abrirCaja = abrirCaja;
        InitializeComponent();
        _fecha.Text = DateTime.Now.ToString("dddd, dd 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-NI"));
        _bienvenida.Text = $"¡Hola, {_nombreUsuario}!";
        _botonActualizar.Click += async (_, _) => await CargarAsync();
        _botonReintentar.Click += async (_, _) => await CargarAsync();
        _error.Resize += (_, _) => _botonReintentar.Left = Math.Max(0, _error.ClientSize.Width - _botonReintentar.Width - 10);
        _botonReintentar.Left = Math.Max(0, _error.ClientSize.Width - _botonReintentar.Width - 10);
        _grafico.Paint += (_, e) => PintarGraficoBarras(_grafico, e, _ventasSemana, Oro);
        _graficoCompras.Paint += (_, e) => PintarGraficoBarras(_graficoCompras, e, _comprasSemana, Verde);
        _productosLista.SizeChanged += (_, _) => AjustarAnchoProductos();
        _btnNuevaVenta.Click += (_, _) => _abrirVentas();
        _btnRegistrarCompra.Click += (_, _) => _abrirCompras();
        _btnVerProductos.Click += (_, _) => _abrirProductos();
        _btnVerClientes.Click += (_, _) => _abrirClientes();
        _btnAdministrarCaja.Click += (_, _) => _abrirCaja();
        _error.Resize += (_, _) => _botonReintentar.Left = Math.Max(0, _error.ClientSize.Width - _botonReintentar.Width - 10);
        _botonReintentar.Location = new Point(Math.Max(0, _error.ClientSize.Width - _botonReintentar.Width - 10), 1);
        _ventasValor.Visible = true;
        _comprasValor.Visible = true;
        _cajaValor.Visible = true;
        AplicarPermisosDashboard();
        AjustarDiseno();
        Resize += (_, _) => AjustarDiseno();
    }

    public async Task CargarAsync()
    {
        if (_cargando)
        {
            return;
        }

        _cargando = true;
        _botonActualizar.Enabled = false;
        _botonReintentar.Enabled = false;
        _error.Text = "Actualizando indicadores…";
        _error.ForeColor = Secundario;
        if (_servicio is null)
        {
            MostrarError("El servicio de datos no está inicializado. Reinicia la aplicación e inténtalo de nuevo.");
            FinalizarCarga();
            return;
        }

        try
        {
            var resultado = await _servicio.IntentarObtenerAsync(_idAperturaCaja);
            if (!resultado.Exitoso || resultado.Resumen is null)
            {
                MostrarError($"No se pudieron consultar los datos ({resultado.ConsultaFallida}). {resultado.Error}");
                return;
            }

            var resumen = resultado.Resumen;
            ActualizarIndicador(_ventasValor, resumen.VentasRealizadasHoy.ToString("N0"));
            ActualizarIndicador(_comprasValor, resumen.ComprasRealizadasHoy.ToString("N0"));
            ActualizarIndicador(_cajaValor, $"C$ {resumen.SaldoCaja:N2}");
            _estadoLabel.Text = resumen.CajaAbierta ? "  ●  CAJA ABIERTA  " : "  ●  CAJA CERRADA  ";
            _estadoLabel.ForeColor = resumen.CajaAbierta ? Verde : Color.FromArgb(171, 84, 69);
            _estadoLabel.BackColor = resumen.CajaAbierta ? Color.FromArgb(229, 235, 221) : Color.FromArgb(246, 229, 223);
            _ventasSemana = resumen.VentasSemana;
            _comprasSemana = resumen.ComprasSemana;
            _grafico.Invalidate();
            _graficoCompras.Invalidate();

            _productosLista.SuspendLayout();
            _productosLista.Controls.Clear();
            if (resumen.ProductosMasVendidos.Count == 0)
            {
                _productosLista.Controls.Add(new Label
                {
                    Text = "Sin ventas registradas",
                    ForeColor = Secundario,
                    AutoSize = true,
                    Padding = new Padding(8)
                });
            }
            else
            {
                int posicion = 1;
                foreach (var producto in resumen.ProductosMasVendidos)
                    _productosLista.Controls.Add(CrearFilaProducto(posicion++, producto));
            }
            AjustarAnchoProductos();
            _productosLista.ResumeLayout(true);

            if (resumen.Advertencias.Count > 0)
            {
                MostrarError(string.Join(" | ", resumen.Advertencias));
            }
            else
            {
                _error.Text = string.Empty;
                _error.ForeColor = Secundario;
                _error.Visible = false;
            }
            _ultimaActualizacion.Text = $"Actualizado {DateTime.Now:HH:mm:ss}";
            _cards.PerformLayout();
            _cards.Refresh();
        }
        catch (Exception ex)
        {
            MostrarError($"No se pudieron consultar los datos. {ex.GetBaseException().Message}");
        }
        finally
        {
            FinalizarCarga();
        }
    }

    private static void ActualizarIndicador(Label indicador, string valor)
    {
        indicador.Text = valor;
        indicador.Visible = true;
        indicador.ForeColor = Texto;
        indicador.Invalidate();
        indicador.Update();
    }

    private void FinalizarCarga()
    {
        _cargando = false;
        _botonActualizar.Enabled = true;
        _botonReintentar.Enabled = true;
    }

    private void MostrarError(string mensaje)
    {
        _error.Text = mensaje;
        _error.ForeColor = Color.FromArgb(171, 84, 69);
        _error.Visible = true;
    }

    private void AplicarPermisosDashboard()
    {
        _permisoVentas = _puedeAcceder("Ventas");
        _permisoCompras = _puedeAcceder("Compras");
        _permisoProductos = _puedeAcceder("Productos");
        _permisoClientes = _puedeAcceder("Clientes");
        _permisoCaja = _puedeAcceder("Caja");
        _chartCard.Visible = _permisoVentas;
        _comprasCard.Visible = _permisoCompras;
        _bestCard.Visible = _permisoProductos;
        _estadoLabel.Visible = _permisoCaja;
        _btnNuevaVenta.Visible = _permisoVentas;
        _btnRegistrarCompra.Visible = _permisoCompras;
        _btnVerProductos.Visible = _permisoProductos;
        _btnVerClientes.Visible = _permisoClientes;
        _btnAdministrarCaja.Visible = _permisoCaja;
        _statusCard.Visible = _permisoCaja || _permisoVentas || _permisoCompras || _permisoProductos || _permisoClientes;
        _graficas = new Control[] { _chartCard, _comprasCard, _bestCard, _statusCard };
    }

    private void AjustarDiseno()
    {
        if (_root.IsDisposed)
            return;

        bool compacto = ClientSize.Width < 1150;
        var visibles = _graficas.Where(PuedeMostrarGrafica).ToArray();
        _body.SuspendLayout();
        _body.Controls.Clear();
        _body.ColumnStyles.Clear();
        _body.RowStyles.Clear();
        _body.ColumnCount = compacto ? 1 : 2;
        _body.RowCount = compacto ? visibles.Length : Math.Max(1, (visibles.Length + 1) / 2);
        _body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, compacto ? 100F : 50F));
        if (!compacto)
            _body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        for (int i = 0; i < _body.RowCount; i++)
            _body.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        for (int i = 0; i < visibles.Length; i++)
            _body.Controls.Add(visibles[i], compacto ? 0 : i % 2, compacto ? i : i / 2);
        _body.ResumeLayout(true);
        _root.AutoScroll = true;
        AjustarAnchoProductos();
        _grafico.Invalidate();
        _graficoCompras.Invalidate();
    }

    private bool PuedeMostrarGrafica(Control control) =>
        ReferenceEquals(control, _chartCard) && _permisoVentas ||
        ReferenceEquals(control, _comprasCard) && _permisoCompras ||
        ReferenceEquals(control, _bestCard) && _permisoProductos ||
        ReferenceEquals(control, _statusCard) &&
            (_permisoCaja || _permisoVentas || _permisoCompras || _permisoProductos || _permisoClientes);

    private void AjustarAnchoProductos()
    {
        int ancho = Math.Max(180, _productosLista.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - _productosLista.Padding.Horizontal);
        foreach (Control fila in _productosLista.Controls)
            fila.Width = ancho;
    }

    private static Control CrearFilaProducto(int posicion, ProductoMasVendidoResumen producto)
    {
        var fila = new TableLayoutPanel
        {
            Width = 320,
            Height = 42,
            ColumnCount = 3,
            RowCount = 1,
            BackColor = Color.FromArgb(248, 244, 236),
            Margin = new Padding(0, 2, 0, 2)
        };
        fila.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 42));
        fila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        fila.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 48));
        fila.Controls.Add(new Label { Text = posicion.ToString(), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, ForeColor = Texto }, 0, 0);
        fila.Controls.Add(new Label { Text = producto.Producto, Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft, ForeColor = Texto, AutoEllipsis = true }, 1, 0);
        fila.Controls.Add(new Label { Text = producto.Cantidad.ToString("N0"), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, ForeColor = Secundario }, 2, 0);
        return fila;
    }

    private void PintarGraficoBarras(Control panel, PaintEventArgs e, IReadOnlyList<VentaDiariaResumen> datos, Color color)
    {
        var area = panel.ClientRectangle;
        area.Inflate(-8, -8);
        if (area.Width <= 0 || area.Height <= 0)
            return;

        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        using var gridPen = new Pen(Color.FromArgb(214, 204, 190));
        using var barBrush = new SolidBrush(color);
        using var labelBrush = new SolidBrush(Secundario);
        decimal maximo = Math.Max(1m, datos.Count == 0 ? 0m : datos.Max(v => v.Total));
        int chartLeft = area.Left + 48;
        int chartRight = area.Right - 8;
        int baseY = area.Bottom - 30;
        int chartTop = area.Top + 14;
        int chartHeight = Math.Max(1, baseY - chartTop);
        using var format = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
        for (int i = 0; i < 4; i++)
        {
            int y = chartTop + chartHeight * i / 3;
            e.Graphics.DrawLine(gridPen, chartLeft, y, chartRight, y);
            decimal escala = maximo * (3 - i) / 3m;
            string etiqueta = escala >= 1000m ? $"{escala / 1000m:N0}k" : $"{escala:N0}";
            e.Graphics.DrawString(etiqueta, Font, labelBrush, new RectangleF(area.Left, y - 9, 40, 18), format);
        }

        int slotWidth = Math.Max(1, (chartRight - chartLeft) / 7);
        for (int i = 0; i < 7; i++)
        {
            var fecha = DateTime.Today.AddDays(i - 6);
            decimal valor = datos.FirstOrDefault(v => v.Fecha.Date == fecha.Date)?.Total ?? 0m;
            int barWidth = Math.Clamp(slotWidth / 2, 8, 28);
            int barHeight = (int)Math.Round((double)(valor / maximo * (chartHeight - 8)));
            int x = chartLeft + i * slotWidth + (slotWidth - barWidth) / 2;
            if (valor > 0)
            {
                var bounds = new Rectangle(x, baseY - Math.Max(4, barHeight), barWidth, Math.Max(4, barHeight));
                e.Graphics.FillRectangle(barBrush, bounds);
            }
            using var centered = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            e.Graphics.DrawString(fecha.ToString("ddd"), Font, labelBrush, new RectangleF(chartLeft + i * slotWidth, baseY + 5, slotWidth, 20), centered);
        }

        if (datos.All(d => d.Total == 0))
            e.Graphics.DrawString("Sin movimientos en este período", Font, labelBrush, new PointF(chartLeft + 10, chartTop + chartHeight / 2));
    }

}
