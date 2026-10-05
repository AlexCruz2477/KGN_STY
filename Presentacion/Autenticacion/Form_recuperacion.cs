using Nk_Colletion_New.Negocios.Autenticacion;

namespace Nk_Colletion_New;

public partial class Form_recuperacion : Form
{
    private readonly LoginServicio? _loginServicio;

    public Form_recuperacion()
    {
        InitializeComponent();
        btncancelar.Click += (_, _) => Close();
    }

    public Form_recuperacion(LoginServicio loginServicio) : this()
    {
        _loginServicio = loginServicio;
    }

    private void Form_recuperacion_Load(object sender, EventArgs e)
    {
        txt_correo.Clear();
        txt_correo.Focus();
    }

    private async void btncontinuar_Click(object sender, EventArgs e)
    {
        string correo = txt_correo.Text.Trim();

        if (!System.Net.Mail.MailAddress.TryCreate(correo, out var direccion) ||
            !string.Equals(
                direccion.Address,
                correo,
                StringComparison.OrdinalIgnoreCase))
        {
            MessageBox.Show(
                "Ingrese un correo electrónico válido.",
                "Validación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txt_correo.Focus();
            return;
        }

        if (_loginServicio is null)
        {
            MessageBox.Show(
                "El servicio de recuperación no está configurado.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        try
        {
            btn_continuar.Enabled = false;
            bool enviado = await _loginServicio.EnviarCodigoRecuperacionAsync(correo);

            if (!enviado)
            {
                MessageBox.Show(
                    "No existe una cuenta activa con ese correo.",
                    "Recuperación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                "Se envió un código de 6 dígitos a su correo.",
                "Recuperación",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            using var restablecer = new Presentacion.Autenticacion.Form_restablecer(
                correo,
                _loginServicio);

            if (restablecer.ShowDialog(this) == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
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
            btn_continuar.Enabled = true;
        }
    }
}
