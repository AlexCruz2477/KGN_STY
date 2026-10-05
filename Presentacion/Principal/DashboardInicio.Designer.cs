namespace Nk_Colletion_New;

internal sealed partial class DashboardInicio
{
    private TableLayoutPanel _root = null!;
    private Panel _header = null!;
    private Label _titulo = null!;
    private Label _bienvenida = null!;
    private Label _fecha = null!;
    private Label _ultimaActualizacion = null!;
    private Button _botonActualizar = null!;
    private TableLayoutPanel _cards = null!;
    private TableLayoutPanel _body = null!;
    private Panel _chartCard = null!;
    private Panel _comprasCard = null!;
    private Panel _bestCard = null!;
    private Panel _statusCard = null!;
    private Panel _grafico = null!;
    private Panel _graficoCompras = null!;
    private FlowLayoutPanel _productosLista = null!;
    private FlowLayoutPanel _acciones = null!;
    private Label _ventasValor = null!;
    private Label _comprasValor = null!;
    private Label _cajaValor = null!;
    private Label _estadoLabel = null!;
    private Label _error = null!;
    private Button _botonReintentar = null!;
    private Button _btnNuevaVenta = null!;
    private Button _btnRegistrarCompra = null!;
    private Button _btnVerProductos = null!;
    private Button _btnVerClientes = null!;
    private Button _btnAdministrarCaja = null!;

    private void InitializeComponent()
    {
        SuspendLayout();
        BackColor = Color.FromArgb(245, 241, 232);
        Dock = DockStyle.Fill;
        _root = new TableLayoutPanel
        {
            Name = "_root",
            Dock = DockStyle.Fill,
            AutoScroll = true,
            BackColor = BackColor,
            ColumnCount = 1,
            RowCount = 4,
            Padding = new Padding(20),
            Margin = Padding.Empty
        };
        _root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        for (int i = 0; i < 4; i++)
            _root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        _header = new Panel { Dock = DockStyle.Fill, Height = 104, MinimumSize = new Size(0, 104), BackColor = BackColor, Margin = new Padding(0, 0, 0, 10) };
        var encabezado = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 2, Margin = Padding.Empty };
        encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        encabezado.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        encabezado.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        encabezado.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        var tituloPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoSize = true, Margin = Padding.Empty };
        _titulo = new Label { Text = "PANEL PRINCIPAL", AutoSize = true, ForeColor = Color.FromArgb(119, 116, 109), Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold), Margin = new Padding(0, 0, 0, 3) };
        _bienvenida = new Label { Text = "¡Hola!", AutoSize = true, ForeColor = Color.FromArgb(41, 42, 40), Font = new Font("Segoe UI", 20F, FontStyle.Bold), Margin = new Padding(0, 0, 0, 2) };
        _fecha = new Label { AutoSize = true, ForeColor = Color.FromArgb(119, 116, 109), Font = new Font("Segoe UI", 9F), Margin = Padding.Empty };
        tituloPanel.Controls.AddRange(new Control[] { _titulo, _bienvenida, _fecha });
        _botonActualizar = CrearBoton("Actualizar", Color.FromArgb(63, 65, 64));
        _botonActualizar.Margin = new Padding(8, 10, 0, 0);
        encabezado.Controls.Add(tituloPanel, 0, 0);
        encabezado.SetRowSpan(tituloPanel, 2);
        encabezado.Controls.Add(_botonActualizar, 1, 0);
        _ultimaActualizacion = new Label { Text = "Pendiente de actualización", AutoSize = true, Anchor = AnchorStyles.Top | AnchorStyles.Right, ForeColor = Color.FromArgb(119, 116, 109), Font = new Font("Segoe UI", 8.5F), Margin = new Padding(0, 4, 0, 0) };
        encabezado.Controls.Add(_ultimaActualizacion, 1, 1);
        _header.Controls.Add(encabezado);

        _error = new Label { Dock = DockStyle.Fill, AutoSize = true, Visible = false, ForeColor = Color.FromArgb(171, 84, 69), BackColor = Color.FromArgb(246, 229, 223), Padding = new Padding(12, 9, 145, 9), Margin = new Padding(0, 0, 0, 10) };
        _botonReintentar = CrearBoton("Reintentar", Color.FromArgb(171, 84, 69));
        _botonReintentar.Size = new Size(112, 34);
        _botonReintentar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        _error.Controls.Add(_botonReintentar);

        _cards = new TableLayoutPanel { Name = "_cards", Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, ColumnCount = 3, RowCount = 1, Margin = new Padding(0, 0, 0, 16) };
        for (int i = 0; i < 3; i++)
            _cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / 3));
        _ventasValor = CrearValorKpi();
        _comprasValor = CrearValorKpi();
        _cajaValor = CrearValorKpi();
        _cards.Controls.Add(CrearTarjetaKpi("VENTAS REALIZADAS HOY", _ventasValor, Color.FromArgb(184, 149, 85)), 0, 0);
        _cards.Controls.Add(CrearTarjetaKpi("COMPRAS REALIZADAS HOY", _comprasValor, Color.FromArgb(101, 112, 90)), 1, 0);
        _cards.Controls.Add(CrearTarjetaKpi("SALDO EN CAJA", _cajaValor, Color.FromArgb(63, 65, 64)), 2, 0);

        _body = new TableLayoutPanel { Name = "_body", Dock = DockStyle.Top, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, ColumnCount = 2, RowCount = 2, Margin = Padding.Empty };
        _chartCard = CrearTarjeta("VENTAS · ÚLTIMOS 7 DÍAS", out _grafico);
        _comprasCard = CrearTarjeta("COMPRAS · ÚLTIMOS 7 DÍAS", out _graficoCompras);
        _bestCard = CrearTarjeta("PRODUCTOS MÁS VENDIDOS", out var bestContent);
        _productosLista = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, FlowDirection = FlowDirection.TopDown, WrapContents = false, Padding = new Padding(12, 5, 12, 8), BackColor = Color.White, Margin = Padding.Empty };
        bestContent.Controls.Add(_productosLista);
        _statusCard = CrearTarjeta("ACCESOS RÁPIDOS", out var statusContent);
        _estadoLabel = new Label { AutoSize = true, Text = "  ●  ESTADO DE CAJA  ", ForeColor = Color.FromArgb(101, 112, 90), BackColor = Color.FromArgb(229, 235, 221), Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold), Margin = new Padding(12, 4, 8, 8) };
        _acciones = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, WrapContents = true, Padding = new Padding(8, 0, 8, 8), BackColor = Color.White, Margin = Padding.Empty };
        _btnNuevaVenta = CrearBoton("Nueva venta", Color.FromArgb(184, 149, 85));
        _btnRegistrarCompra = CrearBoton("Registrar compra", Color.FromArgb(101, 112, 90));
        _btnVerProductos = CrearBoton("Ver productos", Color.FromArgb(63, 65, 64));
        _btnVerClientes = CrearBoton("Ver clientes", Color.FromArgb(63, 65, 64));
        _btnAdministrarCaja = CrearBoton("Administrar caja", Color.FromArgb(63, 65, 64));
        _acciones.Controls.AddRange(new Control[] { _btnNuevaVenta, _btnRegistrarCompra, _btnVerProductos, _btnVerClientes, _btnAdministrarCaja });
        var statusLayout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Margin = Padding.Empty };
        statusLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        statusLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        statusLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        statusLayout.Controls.Add(_estadoLabel, 0, 0);
        statusLayout.Controls.Add(_acciones, 0, 1);
        statusContent.Controls.Add(statusLayout);
        _body.Controls.Add(_chartCard, 0, 0);
        _body.Controls.Add(_comprasCard, 1, 0);
        _body.Controls.Add(_bestCard, 0, 1);
        _body.Controls.Add(_statusCard, 1, 1);

        _root.Controls.Add(_header, 0, 0);
        _root.Controls.Add(_error, 0, 1);
        _root.Controls.Add(_cards, 0, 2);
        _root.Controls.Add(_body, 0, 3);
        Controls.Add(_root);
        ResumeLayout(false);
    }

    private static Label CrearValorKpi() => new()
    {
        AutoSize = true,
        Text = "—",
        ForeColor = Color.FromArgb(41, 42, 40),
        Font = new Font("Segoe UI Semibold", 17F, FontStyle.Bold),
        Margin = new Padding(0, 8, 0, 0)
    };

    private static Control CrearTarjetaKpi(string titulo, Label valor, Color acento)
    {
        var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, MinimumSize = new Size(150, 92), Margin = new Padding(5), Padding = new Padding(14, 12, 8, 8) };
        var contenido = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, BackColor = Color.White, Margin = Padding.Empty };
        contenido.Controls.Add(new Label { Text = titulo, AutoSize = true, ForeColor = Color.FromArgb(119, 116, 109), Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold), Margin = Padding.Empty });
        contenido.Controls.Add(valor);
        panel.Controls.Add(contenido);
        panel.Paint += (_, e) =>
        {
            using var brush = new SolidBrush(acento);
            e.Graphics.FillRectangle(brush, 0, 0, 4, panel.Height);
        };
        return panel;
    }

    private static Panel CrearTarjeta(string titulo, out Panel contenido)
    {
        var tarjeta = new Panel { Dock = DockStyle.Fill, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, BackColor = Color.White, MinimumSize = new Size(280, 300), Margin = new Padding(6), Padding = Padding.Empty };
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, ColumnCount = 1, RowCount = 2, Margin = Padding.Empty };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        var encabezado = new Label { Text = titulo, Dock = DockStyle.Fill, AutoSize = true, ForeColor = Color.FromArgb(41, 42, 40), Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold), Padding = new Padding(14, 13, 10, 11), Margin = Padding.Empty };
        contenido = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, MinimumSize = new Size(0, 225), Margin = Padding.Empty };
        layout.Controls.Add(encabezado, 0, 0);
        layout.Controls.Add(contenido, 0, 1);
        tarjeta.Controls.Add(layout);
        return tarjeta;
    }

    private static Button CrearBoton(string texto, Color fondo) => new()
    {
        Text = texto,
        AutoSize = true,
        MinimumSize = new Size(112, 38),
        FlatStyle = FlatStyle.Flat,
        BackColor = fondo,
        ForeColor = Color.White,
        Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
        Cursor = Cursors.Hand,
        Padding = new Padding(10, 4, 10, 4),
        Margin = new Padding(4)
    };
}
