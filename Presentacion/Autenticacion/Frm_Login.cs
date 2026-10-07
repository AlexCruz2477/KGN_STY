using Nk_Colletion_New.Negocios.Autenticacion;

namespace Nk_Colletion_New.Presentacion.Autenticacion;

public partial class Frm_Login : Form
{
    private readonly ServicioAuth? _servicioAuth;
    private readonly LoginServicio? _loginServicio;

    public Frm_Login()
    {
        InitializeComponent();
    }

    public Frm_Login(ServicioAuth servicioAuth, LoginServicio loginServicio) : this()
    {
        _servicioAuth = servicioAuth;
        _loginServicio = loginServicio;
    }

    private void Frm_Login_Load(object sender, EventArgs e)
    {
        txt_Usuario.Clear();
        txt_Contrasena.Clear();
        txt_Contrasena.Multiline = false;
        txt_Contrasena.PasswordChar = '●';
        txt_Contrasena.UseSystemPasswordChar = true;
        txt_Usuario.Focus();
    }

    private async void btn_Ingresar_Click(object sender, EventArgs e)
    {
        string usuario = txt_Usuario.Text.Trim();
        string contrasena = txt_Contrasena.Text;

        if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
        {
            MessageBox.Show(
                "Ingrese su usuario y contraseña.",
                "Inicio de sesión",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (_servicioAuth is null)
        {
            MessageBox.Show(
                "El servicio de autenticación no está configurado.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        try
        {
            btn_Ingresar.Enabled = false;
            btn_Ingresar.Text = "Ingresando...";

            var sesion = await _servicioAuth.ValidarCredencialesAsync(usuario, contrasena);
            if (sesion is null)
            {
                MessageBox.Show(
                    "Usuario o contraseña incorrectos, o la cuenta está inactiva.",
                    "Acceso denegado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txt_Contrasena.Clear();
                txt_Contrasena.Focus();
                return;
            }

            // El flujo de la aplicación obliga a pasar por apertura de caja.
            // El menú principal nunca se abre con una apertura inexistente.
            Hide();

            using (var apertura = new Formapertura(sesion))
            {
                var resultado = apertura.ShowDialog();
                if (resultado != DialogResult.OK || apertura.IdAperturaCaja <= 0)
                {
                    Show();
                    txt_Contrasena.Clear();
                    txt_Usuario.Focus();
                    return;
                }

                MessageBox.Show(
                    $"¡Bienvenido/a, {sesion.NombreCompleto}!",
                    "Inicio de sesión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                using var principal = new Main(sesion, apertura.IdAperturaCaja);
                principal.ShowDialog();
            }

            Show();
            txt_Contrasena.Clear();
            txt_Usuario.Focus();
        }
        catch (Exception ex)
        {
            if (!Visible && !IsDisposed)
            {
                Show();
            }

            MessageBox.Show(
                "No se pudo iniciar sesión.\n\n" + ex.GetBaseException().Message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        finally
        {
            if (!IsDisposed)
            {
                btn_Ingresar.Enabled = true;
                btn_Ingresar.Text = "Ingresar";
            }
        }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (_loginServicio is null)
        {
            MessageBox.Show(
                "El servicio de recuperación no está configurado.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        using var recuperacion = new Form_recuperacion(_loginServicio);
        recuperacion.ShowDialog(this);
    }
}
