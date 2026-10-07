using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Metodos_Ordenamiento;
using Nk_Colletion_New.Negocios.Servicios.Productos;

namespace Nk_Colletion_New.Presentacion.Productos
{
    public partial class Frm_producto_listado : Form
    {

        private readonly Producto_Service _productoService = new(AppConfig.DbOptions!);

        private readonly Form _anterior;

        private readonly ArbolBinarioBusqueda<ProductoVariante> _arbolProductos = new(x => x.IdProductoNavigation.NombreProducto);

        private readonly System.Windows.Forms.Timer _timerBusqueda = new()
        {
            Interval = 250
        };
        private bool _cargandoFiltros;

        public Frm_producto_listado(Form anterior)
        {
            InitializeComponent();
            _anterior = anterior;
            _timerBusqueda.Tick += (_, _) =>
            {
                _timerBusqueda.Stop();
                AplicarFiltrosYBusqueda();
            };
        }

        private async void Frm_producto_listado_Load(object sender, EventArgs e)
        {
            await CargarArbolAsync();
        }

        //cargar inventario

        private async Task CargarArbolAsync()
        {
            try
            {
                var variantes = await _productoService.ListarVariantesAsync();
                _arbolProductos.Reconstruir(variantes);
                AplicarFiltrosYBusqueda();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo cargar el inventario.\n\n{ex.Message}",
                    "Listado de productos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //buscar y filtrar

        private void AplicarFiltrosYBusqueda()
        {
            string categoria = txtCategoria.Text.Trim();
            string marca = txtMarca.Text.Trim();
            string talla = txtTalla.Text.Trim();
            string color = txtColor.Text.Trim();
            string estado = txtEstado.Text.Trim();
            bool ascendente = cmbOrden.SelectedIndex != 1;
            var variantes = _arbolProductos.BuscarYFiltrar(
                txtBuscar.Text,
                variante => new[] { variante.IdProductoNavigation.NombreProducto, variante.Codigo, variante.IdProductoNavigation.IdMarcaNavigation?.Nombre, variante.IdProductoNavigation.IdCategoriaNavigation?.NombreCategoria, variante.IdTallaNavigation?.NombreTalla, variante.IdColorNavigation?.NombreColor, variante.IdProducto.ToString(), variante.IdVariante.ToString() },
                variante => (string.IsNullOrWhiteSpace(categoria) || variante.IdProductoNavigation.IdCategoriaNavigation?.NombreCategoria.Contains(categoria, StringComparison.OrdinalIgnoreCase) == true) &&
                (string.IsNullOrWhiteSpace(marca) || variante.IdProductoNavigation.IdMarcaNavigation?.Nombre.Contains(marca, StringComparison.OrdinalIgnoreCase) == true) &&
                (string.IsNullOrWhiteSpace(talla) || variante.IdTallaNavigation?.NombreTalla.Contains(talla, StringComparison.OrdinalIgnoreCase) == true) &&
                (string.IsNullOrWhiteSpace(color) || variante.IdColorNavigation?.NombreColor.Contains(color, StringComparison.OrdinalIgnoreCase) == true) &&
                (string.IsNullOrWhiteSpace(estado) || (string.Equals(estado, "activo", StringComparison.OrdinalIgnoreCase) && variante.Estado != false) || (string.Equals(estado, "inactivo", StringComparison.OrdinalIgnoreCase) && variante.Estado == false)),
                ascendente
            );
            dgvProductos.Rows.Clear();
            foreach (var variante in variantes)
            {
                dgvProductos.Rows.Add(
                    variante.IdProducto,
                    variante.IdVariante,
                    variante.Codigo,
                    variante.IdProductoNavigation.NombreProducto,
                    variante.IdProductoNavigation.IdMarcaNavigation?.Nombre ?? "Sin marca",
                    variante.IdProductoNavigation.IdCategoriaNavigation?.NombreCategoria ?? "Sin categoría",
                    variante.IdTallaNavigation?.NombreTalla ?? "Sin talla",
                    variante.IdColorNavigation?.NombreColor ?? "Sin color",
                    variante.StockActual,
                    variante.StockMinimo,
                    $"C$ {variante.PrecioCompra:N2}",
                    $"C$ {variante.PrecioVenta:N2}",
                    variante.Estado != false ? "Activo" : "Inactivo"
                );
            }

            dgvProductos.ClearSelection();
            dgvProductos.CurrentCell = null;
            lblResultados.Text = variantes.Count == 1 ? "1 variante encontrada" : $"{variantes.Count} variantes encontradas";
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (_cargandoFiltros)
            {
                return;
            }

            _timerBusqueda.Stop();
            _timerBusqueda.Start();
        }

        private void filtro_TextChanged(object sender, EventArgs e)
        {
            if (_cargandoFiltros)
            {
                return;
            }

            _timerBusqueda.Stop();
            _timerBusqueda.Start();
        }

        private void cmbOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoFiltros || !IsHandleCreated)
            {
                return;
            }

            AplicarFiltrosYBusqueda();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            _cargandoFiltros = true;
            try
            {
                txtBuscar.Clear();
                txtCategoria.Clear();
                txtMarca.Clear();
                txtTalla.Clear();
                txtColor.Clear();
                txtEstado.Clear();
                cmbOrden.SelectedIndex = 0;
            }
            finally
            {
                _cargandoFiltros = false;
            }

            AplicarFiltrosYBusqueda();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            NavegacionPanel.Volver(this, _anterior);
        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
