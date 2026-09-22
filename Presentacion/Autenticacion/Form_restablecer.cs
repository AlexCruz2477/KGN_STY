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
using Nk_Colletion_New.Negocios.Autenticacion;

namespace Nk_Colletion_New.Presentacion.Autenticacion
{
    public partial class Form_restablecer : Form
    {
        private readonly LoginServicio? _loginServicio;
        private readonly AutenticacionUsuario? _autenticacionUsuario;

        public Form_restablecer()
        {
            InitializeComponent();
        }

        // Constructor que acepta correo y codigo (para prellenar el formulario)
        public Form_restablecer(string correo, string codigo) : this()
        {
            // Si los controles ya están inicializados, prellenar el campo de código
            if (textBox2 != null)
                textBox2.Text = codigo;
        }

    }
}
