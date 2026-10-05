using Guna.UI2.WinForms;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Metodos_Ordenamiento;
using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Negocios.Servicios.Cambio;
using Nk_Colletion_New.Negocios.Servicios.Ventas;

namespace Nk_Colletion_New;

public partial class Form_ventas : Form
{
    private sealed record ClienteOpcion(Cliente Cliente)
    {
        public int IdCliente => Cliente.IdCliente;
        public string NombreCompleto
        {
            get
            {
                string nombre = $"{Cliente.Nombre} {Cliente.Apellido}".Trim();
                return string.IsNullOrWhiteSpace(nombre) ? $"Cliente #{Cliente.IdCliente}" : nombre;
            }
        }
    }

    private sealed record VarianteOpcion(ProductoVariante Variante)
    {
        public int IdVariante => Variante.IdVariante;
        public string NombreProducto => Variante.IdProductoNavigation?.NombreProducto ?? Variante.Codigo;

        public string TextoProducto
        {
            get
            {
                var extras = new[]
                {
                    Variante.IdTallaNavigation?.NombreTalla,
                    Variante.IdColorNavigation?.NombreColor
                }
                .Where(v => !string.IsNullOrWhiteSpace(v));

                string detalle = string.Join(" / ", extras.Select(valor => valor!));
                return string.IsNullOrWhiteSpace(detalle)
                    ? NombreProducto
                    : $"{NombreProducto} — {detalle}";
            }
        }
    }

    private readonly Venta_Service? _servicio;
    private readonly TasaCambio_Service? _tasaService;
    private readonly int _idUsuario;
    private readonly string _nombreUsuario;
    private readonly ListaEnlazada<DetalleVentaTemporal> _detalles = new();
    private readonly Guna2ComboBox _clientesCombo = new();

    private List<VarianteOpcion> _opcionesVariantes = new();
    private TipoCambioBcn? _tasa;
    private int _idMetodoEfectivo;
    private bool _cargando;

    public Form_ventas() : this(0, string.Empty)
    {
    }

    public Form_ventas(int idUsuario) : this(idUsuario, string.Empty)
    {
    }

    public Form_ventas(UsuarioSesion sesion)
        : this(sesion.IdUsuario, sesion.NombreCompleto)
    {
    }

    private Form_ventas(int idUsuario, string? nombreUsuario)
    {
        InitializeComponent();
        _idUsuario = idUsuario;
        _nombreUsuario = nombreUsuario?.Trim() ?? string.Empty;

        if (AppConfig.DbOptions is not null)
        {
            _servicio = new Venta_Service(AppConfig.DbOptions);
            _tasaService = new TasaCambio_Service(AppConfig.DbOptions);
        }

        PrepararSelectorClientes();
        PrepararFormulario();
        ConectarEventos();
    }

    private void PrepararSelectorClientes()
    {
        _clientesCombo.BorderRadius = 8;
        _clientesCombo.DrawMode = DrawMode.OwnerDrawFixed;
        _clientesCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        _clientesCombo.Font = guna2TextBox1.Font;
        _clientesCombo.ForeColor = System.Drawing.Color.FromArgb(41, 42, 40);
        _clientesCombo.ItemHeight = 30;
        _clientesCombo.Location = guna2TextBox1.Location;
        _clientesCombo.Size = guna2TextBox1.Size;
        _clientesCombo.Anchor = guna2TextBox1.Anchor;
        _clientesCombo.TabIndex = guna2TextBox1.TabIndex;

        guna2TextBox1.Visible = false;
        guna2ShadowPanel1.Controls.Add(_clientesCombo);
        _clientesCombo.BringToFront();
    }

    private void PrepararFormulario()
    {
        guna2Button2.Text = "Eliminar";
        guna2Button3.Visible = false;

        guna2NumericUpDown1.Minimum = 1;
        guna2NumericUpDown1.Maximum = 1;
        guna2NumericUpDown1.Value = 1;

        // Cliente: selector de nombre + datos informativos.
        guna2TextBox7.ReadOnly = true; // Cédula
        guna2TextBox4.ReadOnly = true; // Teléfono
        guna2TextBox3.ReadOnly = true; // Dirección
        guna2TextBox7.PlaceholderText = "Cédula del cliente";
        guna2TextBox4.PlaceholderText = "Teléfono del cliente";
        guna2TextBox3.PlaceholderText = "Dirección del cliente";

        // Producto: el combo muestra el nombre. El resto se completa desde la variante.
        guna2TextBox2.ReadOnly = true; // Código
        guna2TextBox8.ReadOnly = true; // Precio
        guna2ComboBox2.Enabled = false; // Talla
        guna2ComboBox3.Enabled = false; // Categoría
        guna2ComboBox4.Enabled = false; // Marca
        guna2ComboBox5.Enabled = false; // Color

        guna2ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        guna2TextBox12.PlaceholderText = "Córdobas";
        guna2TextBox13.PlaceholderText = "Dólares";
        label21.Text = "Efectivo C$:";
        label14.Text = "Efectivo $:";
        label19.Text = "Cantidad:";
        guna2DateTimePicker1.Value = DateTime.Now;
        guna2DateTimePicker1.Enabled = false;
    }

    private void ConectarEventos()
    {
        _clientesCombo.SelectedIndexChanged += ClienteSeleccionado;
        guna2Button1.Click += AgregarDetalle_Click;
        guna2Button2.Click += EliminarDetalle_Click;
        guna2Button4.Click += GuardarVenta_Click;
        guna2DataGridView1.CellDoubleClick += (_, e) => EliminarDetallePorFila(e.RowIndex);
        guna2ComboBox1.SelectedIndexChanged += VarianteSeleccionada;
        guna2NumericUpDown1.ValueChanged += (_, _) => ActualizarTotales();
        guna2TextBox10.TextChanged += (_, _) => ActualizarTotales();
        guna2TextBox12.TextChanged += (_, _) => ActualizarTotales();
        guna2TextBox13.TextChanged += (_, _) => ActualizarTotales();
    }

    private async void Form_ventas_Load(object sender, EventArgs e)
    {
        if (_servicio is null)
        {
            MessageBox.Show(
                "No está configurada la conexión a la base de datos.",
                "Ventas",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        try
        {
            _cargando = true;
            PrepararGrilla();
            await CargarClientesAsync();
            await CargarVariantesAsync();

            _idMetodoEfectivo = await _servicio.ObtenerMetodoPagoIdAsync("Efectivo");

            if (_tasaService is not null)
            {
                _tasa = await _tasaService.ObtenerAsync();
                label14.Text = $"Efectivo $ (TC {_tasa.TasaNioPorUsd:N4}):";
                guna2TextBox13.Enabled = true;
            }
            else
            {
                guna2TextBox13.Enabled = false;
            }

            label11.Text = !string.IsNullOrWhiteSpace(_nombreUsuario)
                ? _nombreUsuario
                : _idUsuario > 0
                    ? $"Usuario #{_idUsuario}"
                    : "Usuario no identificado";

            ActualizarDatosVariante();
            ActualizarDatosCliente();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Ventas",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            _cargando = false;
            ActualizarTotales();
        }
    }

    private void PrepararGrilla()
    {
        guna2DataGridView1.Columns.Clear();
        guna2DataGridView1.Columns.Add("VarianteId", "VarianteId");
        guna2DataGridView1.Columns[0].Visible = false;
        guna2DataGridView1.Columns.Add("Producto", "Producto");
        guna2DataGridView1.Columns.Add("Codigo", "Código");
        guna2DataGridView1.Columns.Add("Cantidad", "Cantidad");
        guna2DataGridView1.Columns.Add("Precio", "Precio unitario");
        guna2DataGridView1.Columns.Add("Subtotal", "Subtotal");
        guna2DataGridView1.AllowUserToAddRows = false;
        guna2DataGridView1.AllowUserToDeleteRows = false;
        guna2DataGridView1.ReadOnly = true;
        guna2DataGridView1.MultiSelect = false;
        guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private async Task CargarClientesAsync()
    {
        if (_servicio is null)
        {
            return;
        }

        var clientes = await _servicio.ListarClientesAsync();
        var opciones = clientes
            .Select(cliente => new ClienteOpcion(cliente))
            .ToList();

        _clientesCombo.DataSource = null;
        _clientesCombo.DisplayMember = nameof(ClienteOpcion.NombreCompleto);
        _clientesCombo.ValueMember = nameof(ClienteOpcion.IdCliente);
        _clientesCombo.DataSource = opciones;
        _clientesCombo.SelectedIndex = -1;
    }

    private async Task CargarVariantesAsync()
    {
        if (_servicio is null)
        {
            return;
        }

        int? seleccionActual = guna2ComboBox1.SelectedValue is int id ? id : null;

        var variantes = await _servicio.BuscarVariantesAsync(string.Empty);
        _opcionesVariantes = variantes.Select(v => new VarianteOpcion(v)).ToList();

        guna2ComboBox1.DataSource = null;
        guna2ComboBox1.DisplayMember = nameof(VarianteOpcion.TextoProducto);
        guna2ComboBox1.ValueMember = nameof(VarianteOpcion.IdVariante);
        guna2ComboBox1.DataSource = _opcionesVariantes;

        if (seleccionActual.HasValue &&
            _opcionesVariantes.Any(item => item.IdVariante == seleccionActual.Value))
        {
            guna2ComboBox1.SelectedValue = seleccionActual.Value;
        }
        else
        {
            guna2ComboBox1.SelectedIndex = _opcionesVariantes.Count > 0 ? 0 : -1;
        }
    }

    private void ClienteSeleccionado(object? sender, EventArgs e) => ActualizarDatosCliente();

    private void ActualizarDatosCliente()
    {
        if (_clientesCombo.SelectedItem is not ClienteOpcion opcion)
        {
            guna2TextBox7.Clear();
            guna2TextBox4.Clear();
            guna2TextBox3.Clear();
            return;
        }

        guna2TextBox7.Text = opcion.Cliente.Cedula ?? string.Empty;
        guna2TextBox4.Text = opcion.Cliente.Telefono ?? string.Empty;
        guna2TextBox3.Text = opcion.Cliente.Direccion ?? string.Empty;
    }

    private void VarianteSeleccionada(object? sender, EventArgs e) => ActualizarDatosVariante();

    private ProductoVariante? ObtenerVarianteSeleccionada() =>
        (guna2ComboBox1.SelectedItem as VarianteOpcion)?.Variante;

    private void ActualizarDatosVariante()
    {
        var variante = ObtenerVarianteSeleccionada();
        if (variante is null)
        {
            guna2TextBox2.Clear();
            guna2TextBox8.Clear();
            MostrarValorEnCombo(guna2ComboBox2, string.Empty);
            MostrarValorEnCombo(guna2ComboBox3, string.Empty);
            MostrarValorEnCombo(guna2ComboBox4, string.Empty);
            MostrarValorEnCombo(guna2ComboBox5, string.Empty);
            guna2NumericUpDown1.Maximum = 1;
            guna2NumericUpDown1.Value = 1;
            label19.Text = "Cantidad:";
            return;
        }

        guna2TextBox2.Text = variante.Codigo;
        guna2TextBox8.Text = variante.PrecioVenta.ToString("0.00");

        MostrarValorEnCombo(guna2ComboBox2, variante.IdTallaNavigation?.NombreTalla ?? string.Empty);
        MostrarValorEnCombo(
            guna2ComboBox3,
            variante.IdProductoNavigation?.IdCategoriaNavigation?.NombreCategoria ?? string.Empty);
        MostrarValorEnCombo(
            guna2ComboBox4,
            variante.IdProductoNavigation?.IdMarcaNavigation?.Nombre ?? string.Empty);
        MostrarValorEnCombo(guna2ComboBox5, variante.IdColorNavigation?.NombreColor ?? string.Empty);

        decimal maximo = Math.Max(1, variante.StockActual);
        guna2NumericUpDown1.Maximum = maximo;
        if (guna2NumericUpDown1.Value > maximo)
        {
            guna2NumericUpDown1.Value = maximo;
        }

        label19.Text = $"Cantidad (stock {variante.StockActual}):";
    }

    private static void MostrarValorEnCombo(Guna2ComboBox combo, string valor)
    {
        combo.DataSource = null;
        combo.Items.Clear();

        if (string.IsNullOrWhiteSpace(valor))
        {
            combo.SelectedIndex = -1;
            return;
        }

        combo.Items.Add(valor);
        combo.SelectedIndex = 0;
    }

    private void AgregarDetalle_Click(object? sender, EventArgs e)
    {
        var variante = ObtenerVarianteSeleccionada();
        if (variante is null)
        {
            MessageBox.Show("Seleccione un producto.");
            return;
        }

        int cantidad = (int)guna2NumericUpDown1.Value;
        if (cantidad <= 0)
        {
            MessageBox.Show("La cantidad debe ser mayor que cero.");
            return;
        }

        if (!TryObtenerDescuento(out _))
        {
            MessageBox.Show("Ingrese un descuento válido.");
            return;
        }

        var actual = _detalles.FirstOrDefault(detalle => detalle.IdVariante == variante.IdVariante);
        int cantidadTotal = cantidad + (actual?.Cantidad ?? 0);

        if (cantidadTotal > variante.StockActual)
        {
            MessageBox.Show(
                $"Stock disponible: {variante.StockActual}. Ya tiene {actual?.Cantidad ?? 0} unidad(es) agregadas.",
                "Ventas",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (actual is not null)
        {
            _detalles.Eliminar(detalle => detalle.IdVariante == variante.IdVariante);
        }

        _detalles.Agregar(new DetalleVentaTemporal(
            variante.IdVariante,
            variante.IdProductoNavigation?.NombreProducto ?? variante.Codigo,
            variante.Codigo,
            cantidadTotal,
            variante.PrecioVenta,
            variante.StockActual,
            variante.IdTallaNavigation?.NombreTalla,
            variante.IdColorNavigation?.NombreColor));

        ActualizarGrilla();
        guna2NumericUpDown1.Value = 1;
    }

    private void EliminarDetalle_Click(object? sender, EventArgs e)
    {
        if (guna2DataGridView1.CurrentRow is null || guna2DataGridView1.CurrentRow.IsNewRow)
        {
            return;
        }

        EliminarDetallePorFila(guna2DataGridView1.CurrentRow.Index);
    }

    private void EliminarDetallePorFila(int indice)
    {
        if (indice < 0 || indice >= guna2DataGridView1.Rows.Count)
        {
            return;
        }

        object? valor = guna2DataGridView1.Rows[indice].Cells[0].Value;
        if (valor is null)
        {
            return;
        }

        int idVariante = Convert.ToInt32(valor);
        _detalles.Eliminar(detalle => detalle.IdVariante == idVariante);
        ActualizarGrilla();
    }

    private void ActualizarGrilla()
    {
        guna2DataGridView1.Rows.Clear();

        foreach (var item in _detalles)
        {
            guna2DataGridView1.Rows.Add(
                item.IdVariante,
                item.Producto,
                item.Codigo,
                item.Cantidad,
                item.PrecioUnitario.ToString("0.00"),
                item.Subtotal.ToString("0.00"));
        }

        ActualizarTotales();
    }

    private void ActualizarTotales()
    {
        if (_cargando)
        {
            return;
        }

        decimal subtotal = _detalles.Sum(detalle => detalle.Subtotal);
        decimal descuento = TryObtenerDescuento(out decimal valorDescuento) ? valorDescuento : 0m;
        descuento = Math.Min(descuento, subtotal);
        decimal iva = 0m;
        decimal total = Math.Max(0m, subtotal - descuento + iva);

        lbl_subtotalF.Text = subtotal.ToString("0.00");
        lbl_descuentoF.Text = descuento.ToString("0.00");
        lbl_totalF.Text = total.ToString("0.00");
        label1.Text = iva.ToString("0.00");

        TryObtenerRecibido(out decimal recibido);
        lbl_cambioF.Text = Math.Max(0m, recibido - total).ToString("0.00");
    }

    private bool TryObtenerDescuento(out decimal descuento)
    {
        descuento = 0m;

        if (string.IsNullOrWhiteSpace(guna2TextBox10.Text))
        {
            return true;
        }

        return decimal.TryParse(guna2TextBox10.Text, out descuento) && descuento >= 0m;
    }

    private bool TryObtenerRecibido(out decimal recibido)
    {
        recibido = 0m;

        decimal cordobas = 0m;
        if (!string.IsNullOrWhiteSpace(guna2TextBox12.Text) &&
            !decimal.TryParse(guna2TextBox12.Text, out cordobas))
        {
            return false;
        }

        if (cordobas < 0m)
        {
            return false;
        }

        recibido = cordobas;

        decimal dolares = 0m;
        if (!string.IsNullOrWhiteSpace(guna2TextBox13.Text) &&
            !decimal.TryParse(guna2TextBox13.Text, out dolares))
        {
            return false;
        }

        if (dolares < 0m)
        {
            return false;
        }

        if (dolares > 0m)
        {
            decimal tasa = _tasa?.TasaNioPorUsd ?? 0m;
            if (tasa <= 0m)
            {
                return false;
            }

            recibido += dolares * tasa;
        }

        recibido = decimal.Round(recibido, 2, MidpointRounding.AwayFromZero);
        return true;
    }

    private async void GuardarVenta_Click(object? sender, EventArgs e)
    {
        if (_servicio is null || _idUsuario <= 0)
        {
            MessageBox.Show("No se identificó el usuario de la sesión.");
            return;
        }

        if (_detalles.Count == 0)
        {
            MessageBox.Show("Agregue al menos un producto a la venta.");
            return;
        }

        if (!TryObtenerDescuento(out decimal descuento))
        {
            MessageBox.Show("Ingrese un descuento válido.");
            return;
        }

        decimal subtotal = _detalles.Sum(detalle => detalle.Subtotal);
        if (descuento > subtotal)
        {
            MessageBox.Show("El descuento no puede superar el subtotal de la venta.");
            return;
        }

        if (!TryObtenerRecibido(out decimal recibido))
        {
            MessageBox.Show("Revise los montos recibidos y la tasa de cambio disponible.");
            return;
        }

        decimal total = Math.Max(0m, subtotal - descuento);
        if (recibido < total)
        {
            MessageBox.Show(
                $"El efectivo recibido no cubre el total. Faltan C$ {total - recibido:N2}.");
            return;
        }

        if (_idMetodoEfectivo <= 0)
        {
            MessageBox.Show("No se encontró el método de pago 'Efectivo' en la base de datos.");
            return;
        }

        try
        {
            guna2Button4.Enabled = false;

            int? idCliente = _clientesCombo.SelectedValue is int id && id > 0 ? id : null;
            var pagos = new[] { new DatosPagoVenta(_idMetodoEfectivo, total) };

            int idVenta = await _servicio.GuardarAsync(
                _idUsuario,
                idCliente,
                descuento,
                0m,
                _detalles,
                pagos);

            decimal cambio = recibido - total;
            MessageBox.Show(
                $"Venta V-{idVenta:D6} registrada correctamente.\n\n" +
                $"Total: C$ {total:N2}\n" +
                $"Cambio: C$ {cambio:N2}",
                "Ventas",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarVenta();
            await CargarVariantesAsync();
            ActualizarDatosVariante();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "No se pudo registrar la venta",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            guna2Button4.Enabled = true;
        }
    }

    private void LimpiarVenta()
    {
        _detalles.Limpiar();
        guna2TextBox12.Clear();
        guna2TextBox13.Clear();
        guna2TextBox10.Clear();
        guna2NumericUpDown1.Value = 1;
        _clientesCombo.SelectedIndex = -1;
        ActualizarDatosCliente();
        ActualizarGrilla();
    }

    private void lbl_subtotal_Click(object sender, EventArgs e)
    {
    }

    private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
    {
    }
}
