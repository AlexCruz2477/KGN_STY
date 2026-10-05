using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Helpers;
using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Negocios.Catalogos;

namespace Nk_Colletion_New;

public partial class Form_proveedores : Form
{
    private readonly Proveedor_Service? _servicio;
    private readonly UsuarioSesion? _sesion;

    public Form_proveedores() : this(null)
    {
    }

    public Form_proveedores(UsuarioSesion? sesion)
    {
        InitializeComponent();
        _sesion = sesion;

        if (AppConfig.DbOptions is not null)
        {
            _servicio = new Proveedor_Service(AppConfig.DbOptions);
        }

        guna2DateTimePicker1.Value = DateTime.Now;
        guna2DateTimePicker1.Enabled = false;
        label13.Text = _sesion is null || string.IsNullOrWhiteSpace(_sesion.NombreCompleto)
            ? "Usuario"
            : _sesion.NombreCompleto;

        guna2Button1.Click += Guardar_Click;
        guna2Button2.Click += (_, _) => Close();
    }

    private async void Guardar_Click(object? sender, EventArgs e)
    {
        if (_servicio is null)
        {
            MessageBox.Show(
                "No está configurada la conexión a la base de datos.",
                "Proveedores",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
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
            await _servicio.GuardarAsync(nombre, telefono, correo, direccion, ruc);

            MessageBox.Show(
                "Proveedor guardado correctamente.",
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
                "No se pudo guardar el proveedor",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            guna2Button1.Enabled = true;
        }
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }
}
