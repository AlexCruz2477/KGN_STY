using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Catalogos;
using Nk_Colletion_New.Datos.Modelos;
using System;
using System.Text.RegularExpressions;
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
    public partial class Subcliente : Form
    {
        private readonly DbContextOptions<NkCollectionContext> _options;
        private readonly Cliente_Service _clienteService;
        private readonly int _idCliente;

        // Designer constructor
        public Subcliente()
            : this(Nk_Colletion_New.Datos.AppConfig.DbOptions ?? new DbContextOptionsBuilder<NkCollectionContext>().Options, 0)
        {
        }

        // Runtime constructor (idCliente = 0 -> nuevo)
        public Subcliente(DbContextOptions<NkCollectionContext> options, int idCliente = 0)
        {
            InitializeComponent();

            _options = options;
            _clienteService = new Cliente_Service(_options);
            _idCliente = idCliente;
            // Eventos: el diseñador puede vincularlos, pero aseguramos handlers
        }

        private void Subcliente_Load(object sender, EventArgs e)
        {
            // Inicializar estado
            guna2ComboBox1.Items.Clear();
            guna2ComboBox1.Items.Add("Activo");
            guna2ComboBox1.Items.Add("Inactivo");
            guna2ComboBox1.SelectedIndex = 0;

            // Modo alta: inicializar valores por defecto
            guna2ComboBox1.SelectedIndex = 0;
            dtpFecha.Value = DateTime.Now;
            txtnombre.Clear();
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox5.Clear();
            guna2TextBox6.Clear();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Guardar cliente
            _ = GuardarClienteAsync();
        }



        private bool ValidarFormulario()
        {
            // Cédula obligatorio al crear cliente
            if (string.IsNullOrWhiteSpace(guna2TextBox6.Text))
            {
                MessageBox.Show("Ingrese la cédula.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                guna2TextBox6.Focus();
                return false;
            }

            // Nombre completo (puede contener apellido). Si sólo se escribe un nombre
            // intentamos obtener apellido desde un control llamado "txtapellido" si existe.
            var fullName = txtnombre.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Ingrese el nombre del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnombre.Focus();
                return false;
            }

            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            // El apellido debe estar en el control txtapellido
            if (string.IsNullOrWhiteSpace(txtapellido.Text))
            {
                MessageBox.Show("Ingrese el apellido del cliente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtapellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(guna2TextBox3.Text))
            {
                // Teléfono opcional en algunos flujos; no obligatorio. Comentado según necesidad.
                // MessageBox.Show("Ingrese el teléfono.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // guna2TextBox3.Focus();
                // return false;
            }

            if (!string.IsNullOrWhiteSpace(guna2TextBox2.Text))
            {
                // Validación básica de correo
                var email = guna2TextBox2.Text.Trim();
                var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
                if (!emailRegex.IsMatch(email))
                {
                    MessageBox.Show("Ingrese un correo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    guna2TextBox2.Focus();
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                yield return c;
                foreach (var child in GetAllControls(c))
                    yield return child;
            }
        }


        private async Task GuardarClienteAsync()
        {
            try
            {
                if (!ValidarFormulario())
                    return;

                guna2Button5.Enabled = false;

                string? cedula = string.IsNullOrWhiteSpace(guna2TextBox6.Text) ? null : guna2TextBox6.Text.Trim();

                // Tomar nombre y apellido de sus controles explícitos
                string nombre = txtnombre.Text.Trim();
                string apellido = txtapellido.Text.Trim();

                string? telefono = string.IsNullOrWhiteSpace(guna2TextBox3.Text) ? null : guna2TextBox3.Text.Trim();
                string? correo = string.IsNullOrWhiteSpace(guna2TextBox2.Text) ? null : guna2TextBox2.Text.Trim().ToLower();
                string? direccion = string.IsNullOrWhiteSpace(guna2TextBox5.Text) ? null : guna2TextBox5.Text.Trim();

                // Llamada a servicio para crear cliente (modo alta)
                await _clienteService.GuardarAsync(
                    cedula,
                    nombre,
                    apellido,
                    telefono,
                    correo,
                    direccion);

                MessageBox.Show(
                    "El cliente se registró correctamente.",
                    "Cliente registrado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al guardar cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                guna2Button5.Enabled = true;
            }
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
              DialogResult = DialogResult.Cancel;
                        Close();
        }
    }
}
