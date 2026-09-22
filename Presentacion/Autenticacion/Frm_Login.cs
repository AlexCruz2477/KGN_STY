using System;
using System.Windows.Forms;
using Nk_Colletion_New.Negocios;
using Nk_Colletion_New.Negocios.Autenticacion;

namespace Nk_Colletion_New.Presentacion.Autenticacion
{
    public partial class Frm_Login : Form
    {
        private readonly LoginServicio? _loginServicio;
        private readonly AutenticacionUsuario? _autenticacionUsuario;

        // Constructor para el diseñador
        public Frm_Login()
        {
            InitializeComponent();
        }

        // Constructor usado al ejecutar el programa
        public Frm_Login(AutenticacionUsuario autenticacionUsuario, LoginServicio loginServicio) : this()
        {
            _autenticacionUsuario = autenticacionUsuario;
            _loginServicio = loginServicio;
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
            if (_autenticacionUsuario == null)
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

                var usuarioEncontrado = _autenticacionUsuario.ValidarCredenciales(
                    usuario,
                    contrasena
                );

                if (usuarioEncontrado == null)
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
                    $"Bienvenido/a {usuarioEncontrado.Nombre} {usuarioEncontrado.Apellido}",
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_recuperacion Form = new Form_recuperacion();
            Form.Show();
        }
    }
}