using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nk_Colletion_New
{
    public partial class Form_mantenimiento : Form
    {
        public Form_mantenimiento()
        {
            InitializeComponent();
        }

        private void Form_mantenimiento_Load(object sender, EventArgs e)
        {

        }

        private void btn_Crear_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();

            guardar.Title = "Guardar copia de seguridad";
            guardar.Filter = "Archivo de respaldo PostgreSQL (*.backup)|*.backup";
            guardar.FileName = "NK STYLE POINT" +
                DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") +
                ".backup";

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                string pgDump =
                    @"C:\Program Files\PostgreSQL\18\bin\pg_dump.exe";

                string host = "localhost";
                string port = "5432";
                string database = "NK_STYLE_POINT";
                string username = "postgres";
                string password = "131007";

                ProcessStartInfo proceso = new ProcessStartInfo();

                proceso.FileName = pgDump;

                proceso.Arguments =
                    $"-h {host} " +
                    $"-p {port} " +
                    $"-U {username} " +
                    $"-F c " +
                    $"-f \"{guardar.FileName}\" " +
                    $"\"{database}\"";

                proceso.UseShellExecute = false;
                proceso.CreateNoWindow = true;
                proceso.RedirectStandardError = true;

                proceso.EnvironmentVariables["PGPASSWORD"] = password;

                using (Process ejecutar = Process.Start(proceso))
                {
                    string error = ejecutar.StandardError.ReadToEnd();

                    ejecutar.WaitForExit();

                    if (ejecutar.ExitCode == 0)
                    {
                        MessageBox.Show(
                            "La copia de seguridad se creó correctamente.",
                            "Respaldo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo crear la copia de seguridad.\n\n" + error,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void btn_Restaurar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();

            abrir.Title = "Seleccionar copia de seguridad";
            abrir.Filter =
                "Archivo de respaldo PostgreSQL (*.backup)|*.backup";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                DialogResult confirmar = MessageBox.Show(
                    "¿Está seguro de que desea restaurar la base de datos?\n\n" +
                    "Los datos actuales pueden ser reemplazados.",
                    "Confirmar restauración",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmar != DialogResult.Yes)
                {
                    return;
                }

                string pgRestore =
                    @"C:\Program Files\PostgreSQL\18\bin\pg_restore.exe";

                string host = "localhost";
                string port = "5432";
                string database = "NK_STYLE_POINT";
                string username = "postgres";
                string password = "131007";

                ProcessStartInfo proceso = new ProcessStartInfo();

                proceso.FileName = pgRestore;


                proceso.Arguments =
                $"-h \"{host}\" " +
                $"-p {port} " +
                $"-U \"{username}\" " +
                $"-d \"{database}\" " +
                $"--clean " +
                $"--if-exists " +
                $"\"{abrir.FileName}\"";

                proceso.UseShellExecute = false;
                proceso.CreateNoWindow = true;
                proceso.RedirectStandardError = true;

                proceso.EnvironmentVariables["PGPASSWORD"] = password;

                using (Process ejecutar = Process.Start(proceso))
                {
                    string error = ejecutar.StandardError.ReadToEnd();

                    ejecutar.WaitForExit();

                    if (ejecutar.ExitCode == 0)
                    {
                        MessageBox.Show(
                            "La base de datos se restauró correctamente.",
                            "Restauración",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            "No se pudo restaurar la base de datos.\n\n" + error,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
