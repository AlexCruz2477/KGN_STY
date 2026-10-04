using Guna.UI2.WinForms;
using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Catalogos;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Nk_Colletion_New
{


    public partial class Form_usuarios : Form
    {
        private readonly DbContextOptions<NkCollectionContext> _options;
        private readonly Usuario_Service _usuarioService;

        public Form_usuarios(DbContextOptions<NkCollectionContext> options)
        {
            InitializeComponent();

            _options = options;
            _usuarioService = new Usuario_Service(_options);
        }



        private async void Form_usuarios_Load(object sender, EventArgs e)
        {
            await CargarRolesAsync();

            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Inactivo");

            // Los usuarios nuevos se muestran activos por defecto.
            cmbEstado.SelectedIndex = 0;

            dtpFecha.Value = DateTime.Now;
        }

        private bool ValidarFormulario()
        {
            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un rol.");
                cmbRol.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtcedula.Text))
            {
                MessageBox.Show("Ingrese la cédula.");
                txtcedula.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtnombre.Text))
            {
                MessageBox.Show("Ingrese el nombre.");
                txtnombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtapellido.Text))
            {
                MessageBox.Show("Ingrese el apellido.");
                txtapellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtusuario.Text))
            {
                MessageBox.Show("Ingrese el nombre de usuario.");
                txtusuario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtcontrasena.Text))
            {
                MessageBox.Show("Ingrese una contraseña.");
                txtcontrasena.Focus();
                return false;
            }

            if (txtcontrasena.Text.Length < 6)
            {
                MessageBox.Show(
                    "La contraseña debe tener al menos 6 caracteres.");
                txtcontrasena.Focus();
                return false;
            }

            // Validaciones adicionales usando helpers
            if (!Nk_Colletion_New.Helpers.FormValidators.IsValidCedula(txtcedula.Text))
            {
                MessageBox.Show("La cédula debe contener sólo dígitos (6-15).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcedula.Focus();
                return false;
            }

            if (!Nk_Colletion_New.Helpers.FormValidators.IsValidName(txtnombre.Text))
            {
                MessageBox.Show("Ingrese un nombre válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnombre.Focus();
                return false;
            }

            if (!Nk_Colletion_New.Helpers.FormValidators.IsValidName(txtapellido.Text))
            {
                MessageBox.Show("Ingrese un apellido válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtapellido.Focus();
                return false;
            }

            if (!Nk_Colletion_New.Helpers.FormValidators.IsValidUsername(txtusuario.Text))
            {
                MessageBox.Show("El nombre de usuario sólo puede contener letras, números, guion bajo o punto (4-30).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtusuario.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(guna2TextBox1.Text) && !Nk_Colletion_New.Helpers.FormValidators.IsValidEmail(guna2TextBox1.Text))
            {
                MessageBox.Show("Ingrese un correo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox1.Focus();
                return false;
            }

            return true;
        }


        private async Task CargarRolesAsync()
        {
            try
            {
                var roles = await _usuarioService.ListarRolesAsync();

                cmbRol.DataSource = roles;
                cmbRol.DisplayMember = "Nombre";
                cmbRol.ValueMember = "IdRol";
                cmbRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los roles.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void cmb_rol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                // Evita doble clic y posibles registros duplicados.
                btnGuardar.Enabled = false;

                int idRol = Convert.ToInt32(cmbRol.SelectedValue);

                string cedula = txtcedula.Text.Trim();
                string nombre = txtnombre.Text.Trim();
                string apellido = txtapellido.Text.Trim();
                string nombreUsuario = txtusuario.Text.Trim();
                string contrasena = txtcontrasena.Text;

                string? correo = string.IsNullOrWhiteSpace(guna2TextBox1.Text)
                    ? null
                    : guna2TextBox1.Text.Trim().ToLower();
                await _usuarioService.GuardarAsync(
                    idRol,
                    cedula,
                    nombre,
                    apellido,
                    nombreUsuario,
                    contrasena,
                    correo);

                MessageBox.Show(
                    "El usuario se registró correctamente.",
                    "Usuario registrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Indica a FrmUsuariosPrincipal que debe refrescar el DGV.
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "No se pudo guardar el usuario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                btnGuardar.Enabled = true;
            }

        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

}
