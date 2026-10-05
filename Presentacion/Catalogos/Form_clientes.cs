using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nk_Colletion_New
{
    // modulo catalogo clientes
    public partial class Form_clientes : Form
    {
        private readonly DbContextOptions<NkCollectionContext> _options;
        private readonly Cliente_Service _clienteService;

        public Form_clientes()
            : this(Nk_Colletion_New.Datos.AppConfig.DbOptions ?? new DbContextOptionsBuilder<NkCollectionContext>().Options)
        {
        }

        public Form_clientes(DbContextOptions<NkCollectionContext> options)
        {
            InitializeComponent();

            _options = options;
            _clienteService = new Cliente_Service(_options);

            // Eventos
            Load += Form_clientes_Load;
            // El botón de añadir está nombrado 'btnañadir' en el diseñador
            btnañadir.Click += guna2Button5_Click; // Añadir
            guna2Button6.Click += guna2Button6_Click; // Editar
            guna2Button2.Click += guna2Button2_Click; // Buscar
            guna2Button3.Click += guna2Button3_Click; // Mostrar todo
            guna2Button4.Click += guna2Button4_Click; // Limpiar
        }

        private async void Form_clientes_Load(object sender, EventArgs e)
        {
            await CargarClientesAsync();
        }

        private async Task CargarClientesAsync()
        {
            try
            {
                var clientes = await _clienteService.ListarAsync();

                guna2DataGridView1.AutoGenerateColumns = false;
                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.Columns.Clear();

                // Id oculto
                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "IdCliente",
                    HeaderText = "Id",
                    DataPropertyName = "IdCliente",
                    Visible = false
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Nombre",
                    HeaderText = "Nombre",
                    DataPropertyName = "Nombre"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Apellido",
                    HeaderText = "Apellido",
                    DataPropertyName = "Apellido"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Telefono",
                    HeaderText = "Teléfono",
                    DataPropertyName = "Telefono"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Cedula",
                    HeaderText = "Cédula",
                    DataPropertyName = "Cedula"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Direccion",
                    HeaderText = "Dirección",
                    DataPropertyName = "Direccion"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Correo",
                    HeaderText = "Correo",
                    DataPropertyName = "Correo"
                });

                guna2DataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Estado",
                    HeaderText = "Estado",
                    DataPropertyName = "Estado"
                });

                var lista = clientes.Select(c => new
                {
                    c.IdCliente,
                    c.Nombre,
                    c.Apellido,
                    Telefono = c.Telefono ?? string.Empty,
                    Cedula = c.Cedula ?? string.Empty,
                    Direccion = c.Direccion ?? string.Empty,
                    Correo = c.Correo ?? string.Empty,
                    Estado = (c.Estado.HasValue && c.Estado.Value) ? "Activo" : "Inactivo"
                }).ToList();

                guna2DataGridView1.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los clientes.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void guna2Button5_Click(object sender, EventArgs e)
        {
            // Redirigir al formulario Subcliente (vista simple de cliente)
            // Usar el constructor que recibe las opciones para asegurar que se usa
            // la versión actualizada del formulario que depende de Cliente_Service.
            var form = new Subcliente(_options, 0);
            form.ShowDialog();

            // Después de cerrar, recargar la lista por si se añadieron clientes desde allí.
            await CargarClientesAsync();
        }

        private async void guna2Button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (guna2DataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un cliente del listado.", "Editar cliente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var idCell = guna2DataGridView1.CurrentRow.Cells["IdCliente"]?.Value;
                if (idCell == null)
                {
                    MessageBox.Show("No se pudo obtener el cliente seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int idCliente = Convert.ToInt32(idCell);

                var form = new SBSubcliente(_options, idCliente);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await CargarClientesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al abrir edición", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtbuscar.Text.Trim();

                var clientes = await _clienteService.BuscarAsync(texto);

                var lista = clientes.Select(c => new
                {
                    c.IdCliente,
                    c.Nombre,
                    c.Apellido,
                    Telefono = c.Telefono ?? string.Empty,
                    Cedula = c.Cedula ?? string.Empty,
                    Direccion = c.Direccion ?? string.Empty,
                    Correo = c.Correo ?? string.Empty,
                    Estado = (c.Estado.HasValue && c.Estado.Value) ? "Activo" : "Inactivo"
                }).ToList();

                guna2DataGridView1.DataSource = null;
                guna2DataGridView1.DataSource = lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void guna2Button3_Click(object sender, EventArgs e)
        {
            await CargarClientesAsync();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.DataSource = null;
            guna2DataGridView1.Columns.Clear();
        }
    }
}
