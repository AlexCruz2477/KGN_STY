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
    //modulo catalogo clientes  
    public partial class Form_clientes : Form
    {
        public Form_clientes()
        {
            InitializeComponent();
        }

        private void Form_clientes_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Subcliente form = new Subcliente();
            form.Show();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            SBSubcliente form = new SBSubcliente();
            form.Show();
        }
    }
}
