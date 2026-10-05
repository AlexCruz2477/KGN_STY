using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Servicios.Productos;

namespace Nk_Colletion_New.Presentacion.Productos
{
    public enum TipoCatalogoRapido
    {
        Categoria,
        Marca,
        Talla,
        Color
    }

    public partial class Frm_catalogo_rapido : Form
    {
        private readonly TipoCatalogoRapido _tipo;
        private readonly Form? _anterior;

        private readonly Categoria_Service _categoriaService = new(AppConfig.DbOptions!);

        private readonly Marca_Service _marcaService = new(AppConfig.DbOptions!);

        private readonly Talla_Service _tallaService = new(AppConfig.DbOptions!);

        private readonly Color_Service _colorService = new(AppConfig.DbOptions!);
        private int _idSeleccionado;

        public Frm_catalogo_rapido(TipoCatalogoRapido tipo, Form? anterior = null)
        {
            InitializeComponent();
            
            _tipo = tipo;
            _anterior = anterior;
        }

        private async void Frm_catalogo_rapido_Load(object sender, EventArgs e)
        {
            ConfigurarPantalla();
            await CargarListadoAsync();
        }

        private string NombreCatalogo => _tipo switch
        {
            TipoCatalogoRapido.Categoria => "categoría",
            TipoCatalogoRapido.Marca => "marca",
            TipoCatalogoRapido.Talla => "talla",
            TipoCatalogoRapido.Color => "color",
            _ => "registro"
        };

        private void ConfigurarPantalla()
        {
            string nombre = NombreCatalogo;
            string plural = _tipo switch
            {
                TipoCatalogoRapido.Categoria => "categorías",
                TipoCatalogoRapido.Marca => "marcas",
                TipoCatalogoRapido.Talla => "tallas",
                TipoCatalogoRapido.Color => "colores",
                _ => "registros"
            };
            lblTitulo.Text = $"Gestión de {plural}";
            lblSubtitulo.Text = $"Guarde, edite o desactive {plural} sin salir del módulo de productos.";
            lblNombre.Text = $"Nombre de {nombre}";
            lblFormulario.Text = $"Datos de la {nombre}";
            Text = $"Gestión de {nombre}s - NK Collection";
            bool esCategoria = _tipo == TipoCatalogoRapido.Categoria;
            lblDescripcion.Visible = esCategoria;
            txtDescripcion.Visible = esCategoria;
            colDescripcion.Visible = esCategoria;
        }

        private async Task CargarListadoAsync(string? busqueda = null)
        {
            dgvCatalogo.Rows.Clear();
            switch (_tipo)
            {
                case TipoCatalogoRapido.Categoria:
                    foreach (var item in string.IsNullOrWhiteSpace(busqueda) ? await _categoriaService.ListarAsync() : await _categoriaService.BuscarAsync(busqueda))
                    {
                        int fila = dgvCatalogo.Rows.Add(
                            item.IdCategoria,
                            item.NombreCategoria,
                            item.Descripcion ?? string.Empty,
                            item.Estado != false ? "Activo" : "Inactivo"
                        );
                        dgvCatalogo.Rows[fila].Tag = item.IdCategoria;
                    }

                    break;
                case TipoCatalogoRapido.Marca:
                    foreach (var item in string.IsNullOrWhiteSpace(busqueda) ? await _marcaService.ListarAsync() : await _marcaService.BuscarAsync(busqueda))
                    {
                        int fila = dgvCatalogo.Rows.Add(item.IdMarca, item.Nombre, "", item.Estado != false ? "Activo" : "Inactivo");
                        dgvCatalogo.Rows[fila].Tag = item.IdMarca;
                    }

                    break;
                case TipoCatalogoRapido.Talla:
                    foreach (var item in string.IsNullOrWhiteSpace(busqueda) ? await _tallaService.ListarAsync() : await _tallaService.BuscarAsync(busqueda))
                    {
                        int fila = dgvCatalogo.Rows.Add(item.IdTalla, item.NombreTalla, "", item.Estado != false ? "Activo" : "Inactivo");
                        dgvCatalogo.Rows[fila].Tag = item.IdTalla;
                    }

                    break;
                case TipoCatalogoRapido.Color:
                    foreach (var item in string.IsNullOrWhiteSpace(busqueda) ? await _colorService.ListarAsync() : await _colorService.BuscarAsync(busqueda))
                    {
                        int fila = dgvCatalogo.Rows.Add(item.IdColor, item.NombreColor, "", item.Estado != false ? "Activo" : "Inactivo");
                        dgvCatalogo.Rows[fila].Tag = item.IdColor;
                    }

                    break;
            }

            lblTotal.Text = $"Registros: {dgvCatalogo.Rows.Count}";
            LimpiarSeleccion();
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                btnGuardar.Enabled = false;
                switch (_tipo)
                {
                    case TipoCatalogoRapido.Categoria:
                        await _categoriaService.GuardarAsync(txtNombre.Text, txtDescripcion.Text);
                        break;
                    case TipoCatalogoRapido.Marca:
                        await _marcaService.GuardarAsync(txtNombre.Text);
                        break;
                    case TipoCatalogoRapido.Talla:
                        await _tallaService.GuardarAsync(txtNombre.Text);
                        break;
                    case TipoCatalogoRapido.Color:
                        await _colorService.GuardarAsync(txtNombre.Text);
                        break;
                }

                MessageBox.Show("Registro guardado correctamente.", "Catálogo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await CargarListadoAsync(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Catálogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuardar.Enabled = true;
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Seleccione un registro para editar.",
                    "Catálogo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            try
            {
                switch (_tipo)
                {
                    case TipoCatalogoRapido.Categoria:
                        await _categoriaService.EditarAsync(_idSeleccionado, txtNombre.Text, txtDescripcion.Text);
                        break;
                    case TipoCatalogoRapido.Marca:
                        await _marcaService.EditarAsync(_idSeleccionado, txtNombre.Text);
                        break;
                    case TipoCatalogoRapido.Talla:
                        await _tallaService.EditarAsync(_idSeleccionado, txtNombre.Text);
                        break;
                    case TipoCatalogoRapido.Color:
                        await _colorService.EditarAsync(_idSeleccionado, txtNombre.Text);
                        break;
                }

                MessageBox.Show(
                    "Registro actualizado correctamente.",
                    "Catálogo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                await CargarListadoAsync(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Catálogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnBorrar_Click(object sender, EventArgs e)
        {
            if (_idSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Seleccione un registro para borrar.",
                    "Catálogo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            var respuesta = MessageBox.Show(
                "El registro se desactivará para conservar la relación con productos existentes. ¿Desea continuar?",
                "Borrar registro",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            try
            {
                switch (_tipo)
                {
                    case TipoCatalogoRapido.Categoria:
                        await _categoriaService.DesactivarAsync(_idSeleccionado);
                        break;
                    case TipoCatalogoRapido.Marca:
                        await _marcaService.DesactivarAsync(_idSeleccionado);
                        break;
                    case TipoCatalogoRapido.Talla:
                        await _tallaService.DesactivarAsync(_idSeleccionado);
                        break;
                    case TipoCatalogoRapido.Color:
                        await _colorService.DesactivarAsync(_idSeleccionado);
                        break;
                }

                MessageBox.Show(
                    "Registro desactivado correctamente.",
                    "Catálogo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                await CargarListadoAsync(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Catálogo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCatalogo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCatalogo.CurrentRow?.Tag is not int id)
            {
                return;
            }

            _idSeleccionado = id;
            txtNombre.Text = dgvCatalogo.CurrentRow.Cells["colNombre"].Value?.ToString() ?? string.Empty;
            txtDescripcion.Text = dgvCatalogo.CurrentRow.Cells["colDescripcion"].Value?.ToString() ?? string.Empty;
            lblSeleccion.Text = $"Seleccionado: ID {_idSeleccionado}";
            btnEditar.Enabled = true;
            btnBorrar.Enabled = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarSeleccion();
            txtNombre.Focus();
        }

        private void LimpiarSeleccion()
        {
            _idSeleccionado = 0;
            txtNombre.Clear();
            txtDescripcion.Clear();
            lblSeleccion.Text = "Nuevo registro";
            btnEditar.Enabled = false;
            btnBorrar.Enabled = false;
            dgvCatalogo.ClearSelection();
            dgvCatalogo.CurrentCell = null;
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            await CargarListadoAsync(txtBuscar.Text);
        }

        private async void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            await CargarListadoAsync(txtBuscar.Text);
        }

        private async void btnVolver_Click(object sender, EventArgs e)
        {
            if (_anterior is Frm_producto producto)
            {
                await producto.RefrescarCatalogosAsync();
            }

            if (_anterior != null)
            {
                NavegacionPanel.Volver(this, _anterior);
                return;
            }

            Close();
        }
    }
}
