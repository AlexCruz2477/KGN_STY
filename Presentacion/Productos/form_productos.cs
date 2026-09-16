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
    public partial class Form_productos : Form
    {
        public Form_productos()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            form_subproductos subproductos = new form_subproductos();
            subproductos.ShowDialog();
        }

        private void Form_productos_Load(object sender, EventArgs e)
        {

        }
    }
}
