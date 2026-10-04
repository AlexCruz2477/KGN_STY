using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Catalogos;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New
{
    public partial class SBSubcliente : Form
    {
        private readonly DbContextOptions<NkCollectionContext> _options;
        private readonly Cliente_Service _clienteService;
        private readonly int _idCliente;

        // Constructor parameterless for designer
        public SBSubcliente()
            : this(Nk_Colletion_New.Datos.AppConfig.DbOptions ?? new DbContextOptionsBuilder<NkCollectionContext>().Options, 0)
        {
        }

        // Constructor for runtime usage. idCliente = 0 -> nuevo cliente
        public SBSubcliente(DbContextOptions<NkCollectionContext> options, int idCliente = 0)
        {
            InitializeComponent();

            _options = options;
            _clienteService = new Cliente_Service(_options);
            _idCliente = idCliente;


        }

        private void SBSubcliente_Load(object sender, EventArgs e)
        {
            // Inicializar estado
            CBestado.Items.Clear();
            CBestado.Items.Add("Activo");
            CBestado.Items.Add("Inactivo");

            if (_idCliente <= 0)
            {
                CBestado.SelectedIndex = 0; // activo por defecto
                dtpFecha.Value = DateTime.Now;
                return;
            }

            // Cargar datos del cliente a editar
            _ = CargarClienteAsync();
        }

        private bool ValidarFormulario()
        {
            if (_idCliente <= 0)
            {
                MessageBox.Show("El cliente seleccionado no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // Si hay correo, validar formato
            if (!string.IsNullOrWhiteSpace(txtcorreo.Text) && !Nk_Colletion_New.Helpers.FormValidators.IsValidEmail(txtcorreo.Text))
            {
                MessageBox.Show("Ingrese un correo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcorreo.Focus();
                return false;
            }

            return true;
        }

        private async Task CargarClienteAsync()
        {
            try
            {
                var cliente = await _clienteService.ObtenerPorIdAsync(_idCliente);

                if (cliente == null)
                {
                    MessageBox.Show("El cliente seleccionado ya no existe.", "Cliente no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                txtnombre.Text = cliente.Nombre;
                txtapellido.Text = cliente.Apellido;
                txtcedula.Text = cliente.Cedula ?? string.Empty;
                txttelefono.Text = cliente.Telefono ?? string.Empty;
                txtcorreo.Text = cliente.Correo ?? string.Empty;
                txtdireccion.Text = cliente.Direccion ?? string.Empty;

                CBestado.SelectedIndex = (cliente.Estado.HasValue && cliente.Estado.Value) ? 0 : 1;
                dtpFecha.Value = cliente.FechaRegistro ?? DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los datos.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario()) return;

                btnguardar.Enabled = false;

                // Sólo edición: requerimos un cliente válido (_idCliente > 0)
                string? cedula = string.IsNullOrWhiteSpace(txtcedula.Text) ? null : txtcedula.Text.Trim();
                string nombre = txtnombre.Text.Trim();
                string apellido = txtapellido.Text.Trim();
                string? telefono = string.IsNullOrWhiteSpace(txttelefono.Text) ? null : txttelefono.Text.Trim();
                string? correo = string.IsNullOrWhiteSpace(txtcorreo.Text) ? null : txtcorreo.Text.Trim();
                string? direccion = string.IsNullOrWhiteSpace(txtdireccion.Text) ? null : txtdireccion.Text.Trim();
                bool? estado = CBestado.SelectedIndex == 0 ? true : false;

                // Forzar edición path
                await _clienteService.EditarAsync(_idCliente, cedula, nombre, apellido, telefono, correo, direccion, estado);

                MessageBox.Show("Los datos del cliente fueron actualizados correctamente.", "Cliente actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnguardar.Enabled = true;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }


    }
}
