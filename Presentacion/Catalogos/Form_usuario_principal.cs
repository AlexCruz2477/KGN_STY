using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Catalogos;
using Nk_Colletion_New.Presentacion.Catalogos;
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
        private readonly DbContextOptions<NkCollectionContext> _options;


        // Parameterless constructor must remain available for the WinForms designer.
        // At runtime prefer the globally configured AppConfig.DbOptions when available.
        public Form_usuario_principal()
            : this(Nk_Colletion_New.Datos.AppConfig.DbOptions ?? new DbContextOptionsBuilder<NkCollectionContext>().Options)
        {
        }

        public Form_usuario_principal(
            DbContextOptions<NkCollectionContext> options)
        {
            InitializeComponent();

            _options = options;
            _usuarioService = new Usuario_Service(_options);
        }

        private async void Form_usuario_principal_Load(object sender, EventArgs e)
        {
            await CargarUsuariosAsync();
        }



        private async Task CargarUsuariosAsync()
        {
            try
            {
                var usuarios = await _usuarioService.ListarAsync();

                // Mostrar sólo las columnas relevantes para edición.
                dgvUsuario.AutoGenerateColumns = false;
                dgvUsuario.DataSource = null;
                dgvUsuario.Columns.Clear();

                // Id (oculto)
                var cId = new DataGridViewTextBoxColumn
                {
                    Name = "IdUsuario",
                    HeaderText = "Id",
                    DataPropertyName = "IdUsuario",
                    Visible = false
                };
                dgvUsuario.Columns.Add(cId);

                // Cédula
                dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Cedula",
                    HeaderText = "Cédula",
                    DataPropertyName = "Cedula"
                });

                // Nombre
                dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Nombre",
                    HeaderText = "Nombre",
                    DataPropertyName = "Nombre"
                });

                // Apellido
                dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Apellido",
                    HeaderText = "Apellido",
                    DataPropertyName = "Apellido"
                });

                // Usuario
                dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Usuario",
                    HeaderText = "Usuario",
                    DataPropertyName = "Usuario1"
                });

                // Correo
                dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Correo",
                    HeaderText = "Correo",
                    DataPropertyName = "Correo"
                });

                // Rol (nombre desde la navegación) y Estado
                dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Rol",
                    HeaderText = "Rol",
                    DataPropertyName = "Rol"
                });

                dgvUsuario.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Estado",
                    HeaderText = "Estado",
                    DataPropertyName = "Estado"
                });

                // Proyectar a un tipo plano para evitar problemas con propiedades de navegación
                var lista = usuarios.Select(u => new
                {
                    u.IdUsuario,
                    u.Cedula,
                    u.Nombre,
                    u.Apellido,
                    Usuario1 = u.Usuario1,
                    u.Correo,
                    Rol = u.IdRolNavigation?.Nombre,
                    Estado = u.Estado ? "Activo" : "Inactivo"
                }).ToList();

                dgvUsuario.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudieron cargar los usuarios.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }



        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Form_usuarios usuarios = new Form_usuarios(_options);
            usuarios.ShowDialog();
        }

        private void guna2Button5_Click_1(object sender, EventArgs e)
        {

        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private async void guna2Button6_Click(object sender, EventArgs e)
        {

            try
            {
                if (dgvUsuario.CurrentRow == null)
                {
                    MessageBox.Show(
                        "Seleccione un usuario del listado.",
                        "Editar usuario",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // Obtener id desde la fila actual (la fuente es una lista anónima proyectada).
                var cell = dgvUsuario.CurrentRow.Cells["IdUsuario"]?.Value;
                if (cell == null)
                {
                    MessageBox.Show(
                        "No se pudo obtener el usuario seleccionado.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                int idUsuario;
                try
                {
                    idUsuario = Convert.ToInt32(cell);
                }
                catch
                {
                    MessageBox.Show(
                        "Id de usuario inválido.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                Form_usuarioD form = new Form_usuarioD(_options, idUsuario);

                // Mostrar el formulario una sola vez y comprobar el resultado.
                var result = form.ShowDialog();

                // El formulario devuelve OK solamente si Guardar terminó correctamente.
                if (result == DialogResult.OK)
                {
                    // Refrescamos la lista después de guardar.
                    await CargarUsuariosAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error al abrir edición",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }


           
        }

        private void guna2Button5_Click_2(object sender, EventArgs e)
        {
            Form_usuarios form = new Form_usuarios(_options);
            form.ShowDialog();
        }

        private async void guna2Button3_Click(object sender, EventArgs e)
        {
            await CargarUsuariosAsync();
        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = CBbuscar.Text.Trim();

                var usuarios = await _usuarioService.BuscarAsync(texto);

                // Proyectar igual que en CargarUsuariosAsync para mantener columnas y tipos.
                var lista = usuarios.Select(u => new
                {
                    u.IdUsuario,
                    u.Cedula,
                    u.Nombre,
                    u.Apellido,
                    Usuario1 = u.Usuario1,
                    u.Correo,
                    Rol = u.IdRolNavigation?.Nombre,
                    Estado = u.Estado ? "Activo" : "Inactivo"
                }).ToList();

                dgvUsuario.DataSource = null;
                dgvUsuario.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error en búsqueda",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            dgvUsuario.DataSource = null;

        }
    }
}
