using System;
using System.Windows.Forms;
using Nk_Colletion_New.Negocios;

namespace Nk_Colletion_New.Presentacion.Autenticacion
{
    public partial class Frm_Login : Form
    {
        private readonly ServicioAuth? _servicioAuth;

        // Constructor para el diseñador
        public Frm_Login()
        {
            InitializeComponent();
        }

        // Constructor usado al ejecutar el programa
        public Frm_Login(ServicioAuth servicioAuth) : this()
        {
            _servicioAuth = servicioAuth;
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {

        }

        private async void btn_Ingresar_Click(object sender, EventArgs e)
        {
            string usuario = txt_Usuario.Text.Trim();
            string contrasena = txt_Contrasena.Text;

            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show(
                    "Ingrese su usuario y contraseña.",
                    "Campos vacíos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // Comprobar que el servicio existe
            if (_servicioAuth == null)
            {
                MessageBox.Show(
                    "El servicio de autenticación no está configurado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            try
            {
                btn_Ingresar.Enabled = false;

                var resultado = await _servicioAuth.IniciarSesionAsync(
                    usuario,
                    contrasena
                );

                if (resultado == null)
                {
                    MessageBox.Show(
                        "Usuario o contraseña incorrectos.",
                        "Error de acceso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    txt_Contrasena.Clear();
                    txt_Contrasena.Focus();

                    return;
                }

                MessageBox.Show(
                    $"Bienvenido/a {resultado.Nombre} {resultado.Apellido}\n" +
                    $"Rol: {resultado.Rol}",
                    "Acceso correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Abrir menú principal
                Main menuPrincipal = new Main();

                // Ocultar login
                this.Hide();

                // Cuando se cierre Main, cerrar también el login
                menuPrincipal.FormClosed += (s, args) => this.Close();

                menuPrincipal.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo realizar el inicio de sesión.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btn_Ingresar.Enabled = true;
            }
        }
    }
}