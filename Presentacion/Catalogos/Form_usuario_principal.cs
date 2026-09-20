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

namespace Nk_Colletion_New
{
    public partial class Form_usuario_principal : Form
    {
        private readonly Usuario_Service _usuarioService;
        public Form_usuario_principal()
        {
            InitializeComponent();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Form_usuarios usuarios = new Form_usuarios();
            usuarios.ShowDialog();
        }

        private void guna2Button5_Click_1(object sender, EventArgs e)
        {

        }

        private void Form_usuario_principal_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
