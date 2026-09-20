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
    public partial class form_subproductos : Form
    {
        public form_subproductos()
        {
            InitializeComponent();
        }
        private void form_subproductos_Load(object sender, EventArgs e)
        {
            // Establecer la fecha actual en el DateTimePicker
            dtpFecha.Value = DateTime.Now;

            //Metodos
           

            //Mostrar la fecha actual en el label
            lblFecha.Text = "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy");

            // Configurar el ComboBox de búsqueda
            cmbbuscarpor.Items.Clear();

            cmbbuscarpor.Items.Add("Código");
            cmbbuscarpor.Items.Add("Tipo de producto");
            cmbbuscarpor.Items.Add("Categoría");
            cmbbuscarpor.Items.Add("Marca");
            cmbbuscarpor.Items.Add("Color");
            cmbbuscarpor.Items.Add("Talla");

            cmbbuscarpor.SelectedIndex = 0;

        }




        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
            "¿Está segura/o de que desea cancelar? Se perderán los datos ingresados.",
            "Confirmar cancelación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {

                txtproducto.Clear();
                cmbcategoria.SelectedIndex = -1;
                cmbmarca.SelectedIndex = -1;
                cmb_color.SelectedIndex = -1;
                cmb_talla.SelectedIndex = -1;
                txtprecioventa.Clear();
                txtcodigo.Clear();
                txtstockmin.Clear();
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {

        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {

        }


        private void BuscarProductos(string campo, string texto)
        {


        }
    }
}

    
    

