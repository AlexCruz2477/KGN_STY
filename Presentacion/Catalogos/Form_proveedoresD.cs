using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Helpers;
using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Negocios.Catalogos;

namespace Nk_Colletion_New.Presentacion.Catalogos;

public partial class Form_proveedoresD : Form
{
    private readonly int _idProveedor;
    private readonly UsuarioSesion? _sesion;
    private readonly Proveedor_Service? _servicio;

    public Form_proveedoresD() : this(0, null)
    {
    }

    public Form_proveedoresD(int idProveedor, UsuarioSesion? sesion = null)
    {
        InitializeComponent();
        _idProveedor = idProveedor;
        _sesion = sesion;

        if (AppConfig.DbOptions is not null)
        {
            _servicio = new Proveedor_Service(AppConfig.DbOptions);
        }

        label13.Text = _sesion is null || string.IsNullOrWhiteSpace(_sesion.NombreCompleto)
            ? "Usuario"
            : _sesion.NombreCompleto;
        guna2DateTimePicker1.Enabled = false;

        Load += Form_proveedoresD_Load;
        guna2Button1.Click += Guardar_Click;
        guna2Button2.Click += (_, _) => Close();
    }

    private async void Form_proveedoresD_Load(object? sender, EventArgs e)
    {
        if (_servicio is null || _idProveedor <= 0)
        {
            guna2Button1.Enabled = false;
            MessageBox.Show(
                "No se pudo identificar el proveedor a editar.",
                "Proveedores",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var proveedor = await _servicio.ObtenerPorIdAsync(_idProveedor);
            if (proveedor is null)
            {
                throw new InvalidOperationException("El proveedor ya no existe.");
            }

            guna2TextBox1.Text = proveedor.Nombre;
            guna2TextBox2.Text = proveedor.Correo ?? string.Empty;
            guna2TextBox3.Text = proveedor.Telefono ?? string.Empty;
            guna2TextBox4.Text = proveedor.Ruc ?? string.Empty;
            guna2TextBox5.Text = proveedor.Direccion ?? string.Empty;
            guna2DateTimePicker1.Value = proveedor.FechaRegistro ?? DateTime.Now;
        }
        catch (Exception ex)
        {
            guna2Button1.Enabled = false;
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Proveedores",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private async void Guardar_Click(object? sender, EventArgs e)
    {
        if (_servicio is null || _idProveedor <= 0)
        {
            return;
        }

        string nombre = guna2TextBox1.Text.Trim();
        string correo = guna2TextBox2.Text.Trim();
        string telefono = guna2TextBox3.Text.Trim();
        string ruc = guna2TextBox4.Text.Trim();
        string direccion = guna2TextBox5.Text.Trim();

        if (!FormValidators.IsValidName(nombre))
        {
            MessageBox.Show("Ingrese un nombre válido.", "Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            guna2TextBox1.Focus();
            return;
        }

        if (!string.IsNullOrWhiteSpace(telefono) && !FormValidators.IsValidPhone(telefono))
        {
            MessageBox.Show("Ingrese un teléfono válido.", "Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            guna2TextBox3.Focus();
            return;
        }

        if (!string.IsNullOrWhiteSpace(correo) && !FormValidators.IsValidEmail(correo))
        {
            MessageBox.Show("Ingrese un correo electrónico válido.", "Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            guna2TextBox2.Focus();
            return;
        }

        try
        {
            guna2Button1.Enabled = false;
            await _servicio.EditarAsync(
                _idProveedor,
                nombre,
                telefono,
                correo,
                direccion,
                ruc,
                null);

            MessageBox.Show(
                "Proveedor actualizado correctamente.",
                "Proveedores",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "No se pudo actualizar el proveedor",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            guna2Button1.Enabled = true;
        }
    }
}
