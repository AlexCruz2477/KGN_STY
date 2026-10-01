using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Catalogos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nk_Colletion_New.Presentacion.Catalogos
{
    public partial class Form_usuarioD : Form
    {
        private readonly DbContextOptions<NkCollectionContext> _options;
        private readonly Usuario_Service _usuarioService;
        // Identificador del usuario que se seleccionó en el DGV.
        private readonly int _idUsuario;

        public Form_usuarioD(
            DbContextOptions<NkCollectionContext> options,
            int idUsuario)
        {
            InitializeComponent();

            _options = options;
            _usuarioService = new Usuario_Service(_options);
            _idUsuario = idUsuario;
        }

        private async Task CargarRolesAsync()
        {
            var roles = await _usuarioService.ListarRolesAsync();

            cmbRol.DataSource = roles;
            cmbRol.DisplayMember = "Nombre";
            cmbRol.ValueMember = "IdRol";
        }

        private bool ValidarFormulario()
        {
            if (_idUsuario <= 0)
            {
                MessageBox.Show("El usuario seleccionado no es válido.");
                return false;
            }

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

            // Contraseña opcional al editar.
            // Vacía = no cambiar la contraseña actual.
            if (!string.IsNullOrWhiteSpace(txtcontrasena.Text) &&
                txtcontrasena.Text.Length < 6)
            {
                MessageBox.Show(
                    "La nueva contraseña debe tener al menos 6 caracteres.");
                txtcontrasena.Focus();
                return false;
            }

            return true;
        }



        private async Task CargarUsuarioAsync()
        {
            try
            {
                var usuario =
                    await _usuarioService.ObtenerPorIdAsync(_idUsuario);

                if (usuario == null)
                {
                    MessageBox.Show(
                        "El usuario seleccionado ya no existe.",
                        "Usuario no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                txtcedula.Text = usuario.Cedula;
                txtnombre.Text = usuario.Nombre;
                txtapellido.Text = usuario.Apellido;
                txtusuario.Text = usuario.Usuario1;
                txtapellido.Text = usuario.Correo ?? string.Empty;

                cmbRol.SelectedValue = usuario.IdRol;

                cmbestado.Items.Clear();
                cmbestado.Items.Add("Activo");
                cmbestado.Items.Add("Inactivo");

                cmbestado.SelectedIndex =
                    usuario.Estado ? 0 : 1;

                // Nunca se carga la contraseña almacenada.
                // El campo vacío significa "mantener contraseña actual".
                txtcontrasena.Clear();

                dtpFecha.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los datos.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private async void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                btn_guardar.Enabled = false;

                int idRol = Convert.ToInt32(cmbRol.SelectedValue);

                string cedula = txtcedula.Text.Trim();
                string nombre = txtnombre.Text.Trim();
                string apellido = txtapellido.Text.Trim();
                string nombreUsuario = txtusuario.Text.Trim();

                string? correo = string.IsNullOrWhiteSpace(txtcorreo.Text)
                    ? null
                    : txtcorreo.Text.Trim();
                string? nuevaContrasena =
                    string.IsNullOrWhiteSpace(txtcontrasena.Text)
                        ? null
                        : txtcontrasena.Text;
                bool estado = cmbestado.SelectedIndex == 0;

                // Se actualiza el usuario seleccionado.
                await _usuarioService.EditarAsync(
                    _idUsuario,
                    idRol,
                    cedula,
                    nombre,
                    apellido,
                    nombreUsuario,
                    correo,
                    estado,
                    nuevaContrasena);

                MessageBox.Show(
                    "Los datos del usuario fueron actualizados correctamente.",
                    "Usuario actualizado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Regresa a la pantalla principal con resultado OK.
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "No se pudo actualizar el usuario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                btn_guardar.Enabled = true;
            }

        }

        private void txt_usuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void Form_usuarioD_Load(object sender, EventArgs e)
        {
            await CargarRolesAsync();
            await CargarUsuarioAsync();

        }

        private void cmb_estado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
