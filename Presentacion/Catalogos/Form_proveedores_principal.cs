using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Negocios.Catalogos;

namespace Nk_Colletion_New;

public partial class Form_proveedores_principal : Form
{
    private readonly Proveedor_Service? _servicio;
    private readonly UsuarioSesion? _sesion;
    private readonly ContextMenuStrip _menuContextual = new();

    public Form_proveedores_principal() : this(null)
    {
    }

    public Form_proveedores_principal(UsuarioSesion? sesion)
    {
        InitializeComponent();
        _sesion = sesion;

        if (AppConfig.DbOptions is not null)
        {
            _servicio = new Proveedor_Service(AppConfig.DbOptions);
        }

        PrepararFormulario();
        ConectarEventos();
    }

    private void PrepararFormulario()
    {
        CBbuscarpor.DropDownStyle = ComboBoxStyle.DropDown;
        CBbuscarpor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        CBbuscarpor.AutoCompleteSource = AutoCompleteSource.CustomSource;

        guna2DataGridView1.AllowUserToAddRows = false;
        guna2DataGridView1.AllowUserToDeleteRows = false;
        guna2DataGridView1.ReadOnly = true;
        guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        guna2DataGridView1.MultiSelect = false;
        guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        var editar = _menuContextual.Items.Add("Editar proveedor");
        editar.Click += (_, _) => EditarProveedorSeleccionado();
        var cambiarEstado = _menuContextual.Items.Add("Activar / desactivar");
        cambiarEstado.Click += async (_, _) => await CambiarEstadoSeleccionadoAsync();
        guna2DataGridView1.ContextMenuStrip = _menuContextual;

        label20.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        label11.Text = _sesion is null || string.IsNullOrWhiteSpace(_sesion.NombreCompleto)
            ? "Usuario"
            : _sesion.NombreCompleto;
    }

    private void ConectarEventos()
    {
        Load += async (_, _) => await CargarAsync();
        guna2Button2.Click += async (_, _) => await BuscarAsync();
        guna2Button3.Click += async (_, _) => await CargarAsync();
        guna2Button4.Click += async (_, _) =>
        {
            CBbuscarpor.Text = string.Empty;
            await CargarAsync();
        };
        guna2Button5.Click += AgregarProveedor_Click;
        guna2Button6.Click += EditarProveedor_Click;
        guna2DataGridView1.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex >= 0)
            {
                EditarProveedorSeleccionado();
            }
        };
        CBbuscarpor.KeyDown += async (_, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await BuscarAsync();
            }
        };
    }

    private async Task CargarAsync()
    {
        if (_servicio is null)
        {
            MostrarErrorConexion();
            return;
        }

        try
        {
            var proveedores = await _servicio.ListarAsync();
            MostrarProveedores(proveedores);
            ActualizarAutocompletado(proveedores);
            label20.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Proveedores",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private async Task BuscarAsync()
    {
        if (_servicio is null)
        {
            MostrarErrorConexion();
            return;
        }

        try
        {
            var proveedores = await _servicio.BuscarAsync(CBbuscarpor.Text);
            MostrarProveedores(proveedores);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Proveedores",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void MostrarProveedores(IEnumerable<Proveedor> proveedores)
    {
        guna2DataGridView1.Rows.Clear();

        foreach (var proveedor in proveedores)
        {
            int indice = guna2DataGridView1.Rows.Add(
                proveedor.IdProveedor,
                proveedor.Nombre,
                proveedor.Telefono ?? string.Empty,
                proveedor.Ruc ?? string.Empty,
                proveedor.Direccion ?? string.Empty,
                proveedor.Correo ?? string.Empty,
                proveedor.Estado == false ? "Inactivo" : "Activo");

            guna2DataGridView1.Rows[indice].Tag = proveedor.IdProveedor;
        }
    }

    private void ActualizarAutocompletado(IEnumerable<Proveedor> proveedores)
    {
        var origen = new AutoCompleteStringCollection();
        origen.AddRange(
            proveedores
                .SelectMany(p => new[] { p.Nombre, p.Ruc, p.Telefono, p.Correo })
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(valor => valor!)
                .ToArray());

        CBbuscarpor.AutoCompleteCustomSource = origen;
    }

    private async void AgregarProveedor_Click(object? sender, EventArgs e)
    {
        using var formulario = new Form_proveedores(_sesion);
        if (formulario.ShowDialog(this) == DialogResult.OK)
        {
            await CargarAsync();
        }
    }

    private void EditarProveedor_Click(object? sender, EventArgs e) =>
        EditarProveedorSeleccionado();

    private async void EditarProveedorSeleccionado()
    {
        if (guna2DataGridView1.CurrentRow?.Tag is not int idProveedor || idProveedor <= 0)
        {
            MessageBox.Show(
                "Seleccione un proveedor de la lista para editar.",
                "Proveedores",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        using var formulario = new Presentacion.Catalogos.Form_proveedoresD(
            idProveedor,
            _sesion);

        if (formulario.ShowDialog(this) == DialogResult.OK)
        {
            await CargarAsync();
        }
    }


    private async Task CambiarEstadoSeleccionadoAsync()
    {
        if (_servicio is null || guna2DataGridView1.CurrentRow?.Tag is not int idProveedor)
        {
            return;
        }

        try
        {
            var proveedor = await _servicio.ObtenerPorIdAsync(idProveedor);
            if (proveedor is null)
            {
                return;
            }

            bool nuevoEstado = proveedor.Estado == false;
            string accion = nuevoEstado ? "activar" : "desactivar";
            var respuesta = MessageBox.Show(
                $"¿Desea {accion} al proveedor '{proveedor.Nombre}'?",
                "Proveedores",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            await _servicio.CambiarEstadoAsync(idProveedor, nuevoEstado);
            await CargarAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Proveedores",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void MostrarErrorConexion()
    {
        MessageBox.Show(
            "No está configurada la conexión a la base de datos.",
            "Proveedores",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }

    private void label20_Click(object sender, EventArgs e)
    {
    }
}
