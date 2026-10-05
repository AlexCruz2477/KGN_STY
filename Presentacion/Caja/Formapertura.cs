using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Servicios.Caja;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Nk_Colletion_New
{
    public partial class Formapertura : Form
    {
        private readonly int _idUsuario;
        private readonly AperturaCaja_Service? _servicio;

        public Formapertura() : this(0)
        {
        }

        public Formapertura(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            if (AppConfig.DbOptions is not null) _servicio = new AperturaCaja_Service(AppConfig.DbOptions);
            txtvalordolar.ReadOnly = true;
            txtvalordolar.Text = "No configurado";
            label4.Text = "Tasa de cambio informativa:";
        }
      
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Separator1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            if (_servicio is null || _idUsuario <= 0) { MessageBox.Show("No se identificó el usuario de la sesión."); return; }
            if (!decimal.TryParse(txtsaldoinicial.Text, out decimal monto) || monto < 0)
            {
                MessageBox.Show("Ingrese un fondo inicial válido, igual o mayor que cero.", "Apertura de caja", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int idApertura = await _servicio.AbrirAsync(_idUsuario, monto);
                MessageBox.Show($"Caja abierta correctamente. Apertura #{idApertura}.", "Apertura de caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_aperturar.Enabled = false;
                txtsaldoinicial.ReadOnly = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "No se pudo abrir la caja", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        
            
    

      


        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Formapertura_Load(object sender, EventArgs e)
        {
           
        }

        private async void Formapertura_Load_1(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            lblUsuario.Text = $"Usuario #{_idUsuario}";
            if (_servicio is null || _idUsuario <= 0) return;
            try
            {
                var (caja, apertura) = await _servicio.ObtenerEstadoAsync(_idUsuario);
                lblUsuario.Text = $"Caja {caja.NumeroCaja} · Usuario #{_idUsuario}";
                if (apertura is not null)
                {
                    txtsaldoinicial.Text = (apertura.MontoApertura ?? 0).ToString("0.00");
                    txtsaldoinicial.ReadOnly = true;
                    btn_aperturar.Enabled = false;
                    guna2HtmlLabel3.Text = $"Caja abierta desde {apertura.FechaApertura:dd/MM/yyyy HH:mm}.";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Apertura de caja", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btn_regresar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
