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
    public partial class form_Listado_Producto : Form
    {
        public form_Listado_Producto()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            form_Productos subproductos = new form_Productos();
            subproductos.ShowDialog();
        }

        private void btn_volver_Click(object sender, EventArgs e)
        {
            // Close this listing and return to main products form
            this.Close();
        }

        private void Form_productos_Load(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Clear filter controls
            try
            {
                // Controls were removed to leave the form blank; nothing to clear.
                // Keep method to avoid referenced event handler removal elsewhere.
                return;
            }
            catch { }
        }
    }
}
