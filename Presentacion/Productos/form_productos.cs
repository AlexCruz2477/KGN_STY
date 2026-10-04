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
    public partial class form_Productos : Form
    {
        public form_Productos()
        {
            InitializeComponent();
        }

        private void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            // Abrir formulario de nueva categoría (si existe)
            MessageBox.Show("Abrir: Nueva categoría");
        }

        private void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Abrir: Nueva marca");
        }

        private void btnNuevaTalla_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Abrir: Nueva talla");
        }

        private void btnNuevoColor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Abrir: Nuevo color");
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ver todos los productos");
        }

        private void btn_volver_Click(object sender, EventArgs e)
        {
            // Intent: abrir el formulario principal de productos o navegar atrás.
            // Si existe un formulario padre se puede cerrar este.
            this.Close();
        }
        private void form_Productos_Load(object sender, EventArgs e)
        {
            // Form left intentionally blank. UI controls were removed from Designer.
            // Keep this handler to satisfy Designer event subscription.
            return;
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

                // Controls removed from Designer; nothing to clear here.
                return;
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

        private void grpRegistrar_Enter(object sender, EventArgs e)
        {

        }

        private void form_Productos_Load_1(object sender, EventArgs e)
        {

        }
    }
}

    
    

