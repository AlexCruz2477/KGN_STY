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
using static System.Windows.Forms.DataFormats;

namespace Nk_Colletion_New
{
    public partial class Formapertura : Form
    {
     
        public Formapertura()
        {
            InitializeComponent();
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // ==============================
            // 1. VALIDAR SALDO INICIAL
            // ==============================

            decimal saldoInicial;

            if (!decimal.TryParse(txtsaldoinicial.Text, out saldoInicial))
            {
                MessageBox.Show("Ingrese un saldo inicial válido.");
                txtsaldoinicial.Focus();
                return;
            }

            if (saldoInicial < 0)
            {
                MessageBox.Show("El saldo inicial no puede ser negativo.");
                txtsaldoinicial.Focus();
                return;
            }


            // ==============================
            // 2. VALIDAR TIPO DE CAMBIO
            // ==============================

            decimal cambioDolar;

            if (!decimal.TryParse(txtvalordolar.Text, out cambioDolar))
            {
                MessageBox.Show("Ingrese un tipo de cambio válido.");
                txtvalordolar.Focus();
                return;
            }

            if (cambioDolar <= 0)
            {
                MessageBox.Show("El tipo de cambio debe ser mayor que 0.");
                txtvalordolar.Focus();
                return;
            }


            // ==============================
            // 3. CONEXIÓN
            // ==============================
            Conexion_BD conexionBD = new Conexion_BD();
            using (NpgsqlConnection conexion = conexionBD.ObtenerConexion())
            {
                conexion.Open();
                // ==============================
                try
                {
                    // Validar que los campos no estén vacíos
                    if (string.IsNullOrWhiteSpace(txtsaldoinicial.Text))
                    {
                        MessageBox.Show("Ingrese el saldo inicial.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtvalordolar.Text))
                    {
                        MessageBox.Show("Ingrese el valor del dólar.");
                        return;
                    }

                    // Convertir el valor del dólar a decimal
                    decimal valorDolar = Convert.ToDecimal(txtvalordolar.Text);

                    string sql = @"INSERT INTO caja
                       (saldo_inicial, cambio_dolar)
                       VALUES (@saldo, @dolar)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conexion))
                    {
                        // saldo_inicial es VARCHAR
                        cmd.Parameters.AddWithValue("@saldo", txtsaldoinicial.Text);

                        // cambio_dolar es NUMERIC
                        cmd.Parameters.AddWithValue("@dolar", valorDolar);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Apertura registrada correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar la apertura: " + ex.Message);
                }
                Main form = new Main();
                form.Show();
            } 
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

        private void Formapertura_Load_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btn_regresar_Click(object sender, EventArgs e)
        {
            
        }
    }
}
