using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Metodos_Ordenamiento;
using Nk_Colletion_New.Negocios.Servicios.Compras;

namespace Nk_Colletion_New;

public partial class form_compras : Form
{
    private sealed record VarianteCompraOpcion(ProductoVariante Variante)
    {
        public int IdVariante => Variante.IdVariante;
        public string Texto => $"{Variante.IdProductoNavigation?.NombreProducto ?? Variante.Codigo} — {Variante.Codigo}";
    }

    private readonly Compra_Service? _servicio;
    private readonly int _idUsuario;
    private readonly ListaEnlazada<DetalleCompraTemporal> _detalles = new();
    private List<VarianteCompraOpcion> _variantes = new();

    public form_compras() : this(0)
    {
    }

    public form_compras(int idUsuario)
    {
        InitializeComponent();
        _idUsuario = idUsuario;

        if (AppConfig.DbOptions is not null)
        {
            _servicio = new Compra_Service(AppConfig.DbOptions);
        }

        PrepararFormulario();
        ConectarEventos();
    }

    private void PrepararFormulario()
    {
        guna2TextBox4.PlaceholderText = "Cantidad";
        guna2TextBox4.Text = "1";
        guna2TextBox5.PlaceholderText = "Precio de compra";
        guna2TextBox2.PlaceholderText = "Precio de venta";

        // Ambos precios parten del valor actual de la variante, pero en una compra
        // deben poder modificarse porque la factura puede traer un nuevo costo.
        guna2TextBox5.ReadOnly = false;
        guna2TextBox2.ReadOnly = false;

        label5.Text = "Producto / variante:";
        label13.Text = _idUsuario > 0 ? $"Usuario #{_idUsuario}" : "Usuario no identificado";
        guna2DateTimePicker1.Value = DateTime.Today;

        guna2DataGridView1.Columns.Clear();
        guna2DataGridView1.Columns.Add("IdVariante", "IdVariante");
        guna2DataGridView1.Columns[0].Visible = false;
        guna2DataGridView1.Columns.Add("Producto", "Producto");
        guna2DataGridView1.Columns.Add("Codigo", "Código");
        guna2DataGridView1.Columns.Add("Cantidad", "Cantidad");
        guna2DataGridView1.Columns.Add("PrecioCompra", "Precio compra");
        guna2DataGridView1.Columns.Add("PrecioVenta", "Precio venta");
        guna2DataGridView1.Columns.Add("Subtotal", "Subtotal");
        guna2DataGridView1.AllowUserToAddRows = false;
        guna2DataGridView1.AllowUserToDeleteRows = false;
        guna2DataGridView1.ReadOnly = true;
        guna2DataGridView1.MultiSelect = false;
        guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private void ConectarEventos()
    {
        guna2Button3.Click += AgregarDetalle_Click;
        guna2Button1.Click += GuardarCompra_Click;
        guna2Button2.Click += (_, _) => LimpiarCompra();
        guna2ComboBox1.SelectedIndexChanged += VarianteSeleccionada;
        guna2DataGridView1.CellDoubleClick += (_, e) => EliminarDetalle(e.RowIndex);
    }

    private async void form_compras_Load(object sender, EventArgs e)
    {
        if (_servicio is null)
        {
            MessageBox.Show(
                "No está configurada la conexión a la base de datos.",
                "Compras",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        try
        {
            await CargarProveedoresAsync();
            await CargarVariantesAsync();
            ActualizarDatosVariante();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Compras",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private async Task CargarProveedoresAsync()
    {
        if (_servicio is null)
        {
            return;
        }

        var proveedores = await _servicio.ListarProveedoresAsync();
        guna2ComboBox2.DataSource = null;
        guna2ComboBox2.DisplayMember = nameof(Proveedor.Nombre);
        guna2ComboBox2.ValueMember = nameof(Proveedor.IdProveedor);
        guna2ComboBox2.DataSource = proveedores;
        guna2ComboBox2.SelectedIndex = -1;
    }

    private async Task CargarVariantesAsync()
    {
        if (_servicio is null)
        {
            return;
        }

        var variantes = await _servicio.ListarVariantesAsync();
        _variantes = variantes.Select(v => new VarianteCompraOpcion(v)).ToList();

        guna2ComboBox1.DataSource = null;
        guna2ComboBox1.DisplayMember = nameof(VarianteCompraOpcion.Texto);
        guna2ComboBox1.ValueMember = nameof(VarianteCompraOpcion.IdVariante);
        guna2ComboBox1.DataSource = _variantes;
        guna2ComboBox1.SelectedIndex = -1;
    }

    private ProductoVariante? ObtenerVarianteSeleccionada() =>
        (guna2ComboBox1.SelectedItem as VarianteCompraOpcion)?.Variante;

    private void VarianteSeleccionada(object? sender, EventArgs e) => ActualizarDatosVariante();

    private void ActualizarDatosVariante()
    {
        var variante = ObtenerVarianteSeleccionada();
        if (variante is null)
        {
            guna2TextBox5.Clear();
            guna2TextBox2.Clear();
            return;
        }

        guna2TextBox5.Text = variante.PrecioCompra.ToString("0.00");
        guna2TextBox2.Text = variante.PrecioVenta.ToString("0.00");
    }

    private void AgregarDetalle_Click(object? sender, EventArgs e)
    {
        var variante = ObtenerVarianteSeleccionada();
        if (variante is null)
        {
            MessageBox.Show("Seleccione un producto / variante.");
            return;
        }

        if (!int.TryParse(guna2TextBox4.Text, out int cantidad) || cantidad <= 0)
        {
            MessageBox.Show("Ingrese una cantidad mayor que cero.");
            return;
        }

        if (!decimal.TryParse(guna2TextBox5.Text, out decimal precioCompra) || precioCompra < 0m ||
            !decimal.TryParse(guna2TextBox2.Text, out decimal precioVenta) || precioVenta < 0m)
        {
            MessageBox.Show("Ingrese precios de compra y venta válidos.");
            return;
        }

        var existente = _detalles.FirstOrDefault(d => d.IdVariante == variante.IdVariante);
        if (existente is not null)
        {
            _detalles.Eliminar(d => d.IdVariante == variante.IdVariante);
        }

        _detalles.Agregar(new DetalleCompraTemporal(
            variante.IdVariante,
            variante.IdProductoNavigation?.NombreProducto ?? variante.Codigo,
            variante.Codigo,
            cantidad + (existente?.Cantidad ?? 0),
            precioCompra,
            precioVenta));

        ActualizarGrilla();
        guna2TextBox4.Text = "1";
    }

    private void ActualizarGrilla()
    {
        guna2DataGridView1.Rows.Clear();

        foreach (var detalle in _detalles)
        {
            guna2DataGridView1.Rows.Add(
                detalle.IdVariante,
                detalle.Producto,
                detalle.Codigo,
                detalle.Cantidad,
                detalle.PrecioCompra.ToString("0.00"),
                detalle.PrecioVenta.ToString("0.00"),
                detalle.Subtotal.ToString("0.00"));
        }

        label14.Text = $"Total: C$ {_detalles.Sum(d => d.Subtotal):N2}";
    }

    private void EliminarDetalle(int index)
    {
        if (index < 0 || index >= guna2DataGridView1.Rows.Count)
        {
            return;
        }

        object? valor = guna2DataGridView1.Rows[index].Cells[0].Value;
        if (valor is null)
        {
            return;
        }

        int idVariante = Convert.ToInt32(valor);
        _detalles.Eliminar(d => d.IdVariante == idVariante);
        ActualizarGrilla();
    }

    private async void GuardarCompra_Click(object? sender, EventArgs e) => await GuardarCompraAsync();

    private async Task GuardarCompraAsync()
    {
        if (_servicio is null || _idUsuario <= 0)
        {
            MessageBox.Show("No se identificó el usuario.");
            return;
        }

        if (guna2ComboBox2.SelectedValue is not int idProveedor || idProveedor <= 0)
        {
            MessageBox.Show("Seleccione un proveedor.");
            return;
        }

        if (_detalles.Count == 0)
        {
            MessageBox.Show("Agregue al menos un producto a la compra.");
            return;
        }

        try
        {
            guna2Button1.Enabled = false;
            guna2Button3.Enabled = false;

            int idCompra = await _servicio.GuardarAsync(
                idProveedor,
                _idUsuario,
                guna2TextBox3.Text,
                guna2DateTimePicker1.Value,
                0m,
                _detalles);

            MessageBox.Show(
                $"Compra #{idCompra} registrada correctamente.\n\n" +
                $"Total: C$ {_detalles.Sum(d => d.Subtotal):N2}",
                "Compras",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarCompra();
            await CargarVariantesAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "No se pudo registrar la compra",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            guna2Button1.Enabled = true;
            guna2Button3.Enabled = true;
        }
    }

    private void LimpiarCompra()
    {
        _detalles.Limpiar();
        ActualizarGrilla();
        guna2TextBox3.Clear();
        guna2TextBox4.Text = "1";
        guna2ComboBox1.SelectedIndex = -1;
        guna2ComboBox2.SelectedIndex = -1;
        ActualizarDatosVariante();
    }

    // Eventos heredados del Designer original.
    private async void guna2Button1_Click(object sender, EventArgs e) => await GuardarCompraAsync();

    private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void label1_Click(object sender, EventArgs e)
    {

    }
}
