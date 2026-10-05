using Nk_Colletion_New.Negocios.Autenticacion;

namespace Nk_Colletion_New.Presentacion.Autenticacion;

public partial class Form_restablecer : Form
{
    private readonly LoginServicio? _loginServicio;
    private readonly string _correo = string.Empty;

    public Form_restablecer()
    {
        InitializeComponent();
        btncontinuar.Click += btncontinuar_Click;
        btncancelar.Click += (_, _) => Close();

        textBox1.Clear();
        textBox1.UseSystemPasswordChar = true;
        textBox2.Clear();
    }

    public Form_restablecer(
        string correo,
        LoginServicio loginServicio) : this()
    {
        _correo = correo;
        _loginServicio = loginServicio;
    }

    private async void btncontinuar_Click(object? sender, EventArgs e)
    {
        if (_loginServicio is null)
        {
            MessageBox.Show("El servicio de recuperación no está configurado.");
            return;
        }

        string codigo = textBox2.Text.Trim();
        string nuevaContrasena = textBox1.Text;

        if (codigo.Length != 6 || !codigo.All(char.IsDigit))
        {
            MessageBox.Show(
                "Ingrese el código de 6 dígitos enviado a su correo.",
                "Validación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            textBox2.Focus();
            return;
        }

        if (nuevaContrasena.Length < 6)
        {
            MessageBox.Show(
                "La nueva contraseña debe tener al menos 6 caracteres.",
                "Validación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            textBox1.Focus();
            return;
        }

        try
        {
            btncontinuar.Enabled = false;

            bool actualizado = await _loginServicio.CambiarContrasenaAsync(
                _correo,
                codigo,
                nuevaContrasena);

            if (!actualizado)
            {
                MessageBox.Show(
                    "El código es incorrecto o ya expiró.",
                    "Recuperación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                "Contraseña actualizada correctamente.",
                "Recuperación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Recuperación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            btncontinuar.Enabled = true;
        }
    }
}
