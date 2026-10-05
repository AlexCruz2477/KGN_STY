using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Metodos_Ordenamiento;
using Nk_Colletion_New.Negocios.Servicios.Productos;

namespace Nk_Colletion_New.Presentacion.Productos
{
    public partial class Frm_producto_listado : Form
    {

        private readonly Producto_Service _productoService = new(AppConfig.DbOptions!);

        private readonly Categoria_Service _categoriaService = new(AppConfig.DbOptions!);

        private readonly Marca_Service _marcaService = new(AppConfig.DbOptions!);

        private readonly Talla_Service _tallaService = new(AppConfig.DbOptions!);

        private readonly Color_Service _colorService = new(AppConfig.DbOptions!);
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
            // El estilo dinámico solo se aplica al ejecutar. En el Designer se usan
            // las propiedades ya guardadas en InitializeComponent para que sea editable.
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
}

            _anterior = anterior;
            cmbOrden.Items.Clear();
            cmbOrden.Items.AddRange(new object[] { "A - Z", "Z - A" });
            cmbOrden.SelectedIndex = 0;
            _timerBusqueda.Tick += (_, _) =>
            {
                _timerBusqueda.Stop();
                AplicarFiltrosYBusqueda();
            };
        }

        private async void Frm_producto_listado_Load(object sender, EventArgs e)
        {
            await CargarFiltrosAsync();
            await CargarArbolAsync();
        }

        //cargar filtros

        private async Task CargarFiltrosAsync()
        {
            _cargandoFiltros = true;
            try
            {
                var categorias = (await _categoriaService.ListarAsync()).Where(x => x.Estado != false).ToList();
                var marcas = (await _marcaService.ListarAsync()).Where(x => x.Estado != false).ToList();
                var tallas = (await _tallaService.ListarAsync()).Where(x => x.Estado != false).ToList();
                var colores = (await _colorService.ListarAsync()).Where(x => x.Estado != false).ToList();
                ConfigurarCombo(cmbCategoria, categorias, "NombreCategoria", "IdCategoria");
                ConfigurarCombo(cmbMarca, marcas, "Nombre", "IdMarca");
                ConfigurarCombo(cmbTalla, tallas, "NombreTalla", "IdTalla");
                ConfigurarCombo(cmbColor, colores, "NombreColor", "IdColor");
                cmbEstado.Items.Clear();
                cmbEstado.Items.AddRange(new object[] { "Activo", "Inactivo" });
                cmbEstado.SelectedIndex = -1;
            }
            finally
            {
                _cargandoFiltros = false;
            }
        }

        private static void ConfigurarCombo(ComboBox combo, object datos, string displayMember, string valueMember)
        {
            combo.DataSource = null;
            combo.DisplayMember = displayMember;
            combo.ValueMember = valueMember;
            combo.DataSource = datos;
            combo.SelectedIndex = -1;
        }

        private static int? ObtenerId(ComboBox combo)
        {
            if (combo.SelectedIndex < 0 || combo.SelectedValue == null)
            {
                return null;
            }

            return Convert.ToInt32(combo.SelectedValue);
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
            int? idCategoria = ObtenerId(cmbCategoria);
            int? idMarca = ObtenerId(cmbMarca);
            int? idTalla = ObtenerId(cmbTalla);
            int? idColor = ObtenerId(cmbColor);
            bool? estado = cmbEstado.SelectedIndex switch
            {
                0 => true,
                1 => false,
                _ => null
            };
            bool ascendente = cmbOrden.SelectedIndex != 1;
            var variantes = _arbolProductos.BuscarYFiltrar(
                txtBuscar.Text,
                variante => new[] { variante.IdProductoNavigation.NombreProducto, variante.Codigo, variante.IdProductoNavigation.IdMarcaNavigation?.Nombre, variante.IdProductoNavigation.IdCategoriaNavigation?.NombreCategoria, variante.IdTallaNavigation?.NombreTalla, variante.IdColorNavigation?.NombreColor, variante.IdProducto.ToString(), variante.IdVariante.ToString() },
                variante => (!idCategoria.HasValue ||
                variante.IdProductoNavigation.IdCategoria == idCategoria.Value) &&
                (!idMarca.HasValue ||
                variante.IdProductoNavigation.IdMarca == idMarca.Value) &&
                (!idTalla.HasValue ||
                variante.IdTalla == idTalla.Value) &&
                (!idColor.HasValue ||
                variante.IdColor == idColor.Value) &&
                (!estado.HasValue ||
                (variante.Estado != false) == estado.Value),
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

        private void filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoFiltros)
            {
                return;
            }

            AplicarFiltrosYBusqueda();
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
                cmbCategoria.SelectedIndex = -1;
                cmbMarca.SelectedIndex = -1;
                cmbTalla.SelectedIndex = -1;
                cmbColor.SelectedIndex = -1;
                cmbEstado.SelectedIndex = -1;
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
    }
}
