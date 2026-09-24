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
using System.Net.Mail;
using Nk_Colletion_New.Presentacion.Autenticacion;

namespace Nk_Colletion_New
{
    public partial class Form_recuperacion : Form
    {
        private readonly LoginServicio? _loginServicio;

        private readonly AutenticacionUsuario? _autenticacionUsuario;
        public Form_recuperacion()
        {
            InitializeComponent();
        }
        public Form_recuperacion(
            LoginServicio loginServicio,
            AutenticacionUsuario? autenticacionUsuario) :this()
        {
            _loginServicio = loginServicio;
            _autenticacionUsuario = autenticacionUsuario;
        }
        private void Form_recuperacion_Load(object sender, EventArgs e)
        {

        }

        private void btncontinuar_Click(object sender, EventArgs e)
        {
            string correo = txt_correo.Text.Trim();

            if (!System.Net.Mail.MailAddress.TryCreate(correo, out var direccion) ||
                !string.Equals(direccion.Address, correo, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Ingrese un correo electrónico válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_correo.Focus();
                return;
            }

            if (_loginServicio is null)
            {
                MessageBox.Show("El servicio de recuperación no está configurado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btn_continuar.Enabled = false;
            try
            {
                bool enviado = _loginServicio.EnviarCodigoRecuperacion(correo);
                if (!enviado)
                {
                    MessageBox.Show("No se pudo enviar el código. Verifique que el correo esté registrado y la configuración de Zoho.", "Recuperación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("El código de recuperación fue enviado a su correo.", "Recuperación", MessageBoxButtons.OK, MessageBoxIcon.Information);
               // new Form_restablecer(correo, _loginServicio, _autenticacionUsuario).Show();
                Hide();
            }
            finally
            {
                btn_continuar.Enabled = true;
            }
        }


    }
}
