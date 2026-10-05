using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Metodos_Ordenamiento;
using Nk_Colletion_New.Negocios.Servicios.Productos;

namespace Nk_Colletion_New.Presentacion.Productos
{
    public partial class Frm_producto : Form
    {

        private readonly Producto_Service _productoService = new(AppConfig.DbOptions!);

        private readonly Categoria_Service _categoriaService = new(AppConfig.DbOptions!);

        private readonly Marca_Service _marcaService = new(AppConfig.DbOptions!);

        private readonly Talla_Service _tallaService = new(AppConfig.DbOptions!);

        private readonly Color_Service _colorService = new(AppConfig.DbOptions!);

        private readonly ArbolBinarioBusqueda<Producto> _arbolProductos = new(x => x.NombreProducto);

        private readonly System.Windows.Forms.Timer _timerBusqueda = new()
        {
            Interval = 250
        };
        private int? _idProductoParaVariantes;

        public Frm_producto()
        {
            InitializeComponent();
            // El estilo dinámico solo se aplica al ejecutar. En el Designer se usan
            // las propiedades ya guardadas en InitializeComponent para que sea editable.
            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
}

            cmbOrdenResumen.Items.Clear();
            cmbOrdenResumen.Items.AddRange(new object[] { "A - Z", "Z - A" });
            cmbOrdenResumen.SelectedIndex = 0;
            _timerBusqueda.Tick += (_, _) =>
            {
                _timerBusqueda.Stop();
                AplicarBusquedaResumen();
            };
            ActivarModoNuevoProducto(false);
        }

        private async void Frm_producto_Load(object sender, EventArgs e)
        {
            await RefrescarCatalogosAsync();
            await RecargarResumenAsync();
        }

        //cargar catálogos

        internal async Task RefrescarCatalogosAsync()
        {
            var categorias = (await _categoriaService.ListarAsync()).Where(x => x.Estado != false).ToList();
            var marcas = (await _marcaService.ListarAsync()).Where(x => x.Estado != false).ToList();
            var tallas = (await _tallaService.ListarAsync()).Where(x => x.Estado != false).ToList();
            var colores = (await _colorService.ListarAsync()).Where(x => x.Estado != false).ToList();
            ConfigurarCombo(cmbCategoria, categorias, "NombreCategoria", "IdCategoria");
            ConfigurarCombo(cmbMarca, marcas, "Nombre", "IdMarca");
            ConfigurarCombo(cmbTalla, tallas, "NombreTalla", "IdTalla");
            ConfigurarCombo(cmbColor, colores, "NombreColor", "IdColor");
        }

        private static void ConfigurarCombo(ComboBox combo, object datos, string displayMember, string valueMember)
        {
            object? valorAnterior = combo.SelectedValue;
            combo.DataSource = null;
            combo.DisplayMember = displayMember;
            combo.ValueMember = valueMember;
            combo.DataSource = datos;
            combo.SelectedIndex = -1;
            if (valorAnterior == null)
            {
                return;
            }

            try
            {
                combo.SelectedValue = valorAnterior;
            }
            catch
            {
                combo.SelectedIndex = -1;
            }
        }

        private static int? ObtenerId(ComboBox combo)
        {
            if (combo.SelectedIndex < 0 || combo.SelectedValue == null)
            {
                return null;
            }

            return Convert.ToInt32(combo.SelectedValue);
        }

        //cargar productos

        private async Task RecargarResumenAsync()
        {
            try
            {
                var productos = await _productoService.ListarProductosAsync();
                _arbolProductos.Reconstruir(productos);
                AplicarBusquedaResumen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo cargar el listado de productos.\n\n{ex.Message}",
                    "Productos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //buscar productos

        private void AplicarBusquedaResumen()
        {
            bool ascendente = cmbOrdenResumen.SelectedIndex != 1;
            var productos = _arbolProductos.BuscarYFiltrar(
                txtBuscarResumen.Text,
                producto => new[] { producto.NombreProducto, producto.IdMarcaNavigation?.Nombre, producto.IdCategoriaNavigation?.NombreCategoria, producto.IdProducto.ToString() },
                ascendente: ascendente
            );
            dgvResumen.Rows.Clear();
            foreach (var producto in productos)
            {
                var variantes = producto.ProductoVariantes.ToList();
                int cantidadVariantes = variantes.Count;
                int stockTotal = variantes.Sum(v => v.StockActual);
                decimal precioMinimo = cantidadVariantes == 0 ? 0 : variantes.Min(v => v.PrecioVenta);
                decimal precioMaximo = cantidadVariantes == 0 ? 0 : variantes.Max(v => v.PrecioVenta);
                string precio = cantidadVariantes == 0 ? "C$ 0.00" : precioMinimo == precioMaximo ? $"C$ {precioMinimo:N2}" : $"C$ {precioMinimo:N2} - {precioMaximo:N2}";
                dgvResumen.Rows.Add(
                    producto.IdProducto,
                    producto.NombreProducto,
                    producto.IdMarcaNavigation?.Nombre ?? "Sin marca",
                    producto.IdCategoriaNavigation?.NombreCategoria ?? "Sin categoría",
                    cantidadVariantes,
                    stockTotal,
                    precio,
                    producto.Estado != false ? "Activo" : "Inactivo"
                );
            }

            dgvResumen.ClearSelection();
            dgvResumen.CurrentCell = null;
            btnAgregarVariantesExistente.Enabled = false;
            lblResumenTotal.Text = $"Mostrando {productos.Count} producto(s) agrupados por ID";
        }

        //validar variante

        private bool ValidarVarianteFormulario()
        {
            if (ObtenerId(cmbTalla)is null)
            {
                MessageBox.Show("Seleccione una talla.", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbTalla.Focus();
                return false;
            }

            if (ObtenerId(cmbColor)is null)
            {
                MessageBox.Show("Seleccione un color.", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbColor.Focus();
                return false;
            }

            return true;
        }

        //agregar variante

        private async void btnAgregarVariante_Click(object sender, EventArgs e)
        {
            if (_idProductoParaVariantes is null)
            {
                MessageBox.Show(
                    "Primero guarda el producto con su primera variante.",
                    "Producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            if (!ValidarVarianteFormulario())
            {
                return;
            }

            try
            {
                btnAgregarVariante.Enabled = false;
                int idVariante = await _productoService.AgregarVarianteAsync(
                    _idProductoParaVariantes.Value,
                    ObtenerId(cmbTalla),
                    ObtenerId(cmbColor),
                    Convert.ToInt32(nudStock.Value),
                    Convert.ToInt32(nudStockMinimo.Value),
                    nudPrecioCompra.Value,
                    nudPrecioVenta.Value
                );
                MessageBox.Show(
                    $"Variante #{idVariante} agregada correctamente.",
                    "Producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                LimpiarCamposVariante();
                await RecargarResumenAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAgregarVariante.Enabled = true;
            }
        }

        //guardar producto

        private async void btnGuardarProducto_Click(object sender, EventArgs e)
        {
            if (!ValidarVarianteFormulario())
            {
                return;
            }

            try
            {
                btnGuardarProducto.Enabled = false;
                int idProducto = await _productoService.GuardarAsync(
                    txtNombre.Text,
                    txtDescripcion.Text,
                    ObtenerId(cmbCategoria),
                    ObtenerId(cmbMarca),
                    ObtenerId(cmbTalla),
                    ObtenerId(cmbColor),
                    Convert.ToInt32(nudStock.Value),
                    Convert.ToInt32(nudStockMinimo.Value),
                    nudPrecioCompra.Value,
                    nudPrecioVenta.Value
                );
                await RecargarResumenAsync();
                await ActivarModoAgregarVariantesAsync(idProducto);
                MessageBox.Show(
                    $"Producto #{idProducto} guardado correctamente. Ahora puedes agregar más variantes.",
                    "Producto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGuardarProducto.Enabled = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ActivarModoNuevoProducto(true);
        }

        //nuevo producto

        private void ActivarModoNuevoProducto(bool limpiarCampos)
        {
            _idProductoParaVariantes = null;
            txtNombre.ReadOnly = false;
            txtDescripcion.ReadOnly = false;
            cmbCategoria.Enabled = true;
            cmbMarca.Enabled = true;
            lblRegistrar.Text = "Registrar producto";
            lblRegistrarAyuda.Text = "Completa los datos del producto y define su primera combinación de talla y color.";
            lblVariante.Text = "Variante inicial del producto";
            btnGuardarProducto.Visible = true;
            btnGuardarProducto.Enabled = true;
            btnAgregarVariante.Visible = false;
            btnLimpiar.Text = "Limpiar";
            if (!limpiarCampos)
            {
                return;
            }

            txtNombre.Clear();
            txtDescripcion.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;
            LimpiarCamposVariante();
            txtNombre.Focus();
        }

        //modo agregar variantes

        private async Task ActivarModoAgregarVariantesAsync(int idProducto)
        {
            try
            {
                var producto = await _productoService.ObtenerPorIdAsync(idProducto);
                if (producto is null)
                {
                    MessageBox.Show(
                        "No se encontró el producto seleccionado.",
                        "Productos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (producto.Estado == false)
                {
                    MessageBox.Show("El producto está inactivo.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _idProductoParaVariantes = producto.IdProducto;
                txtNombre.Text = producto.NombreProducto;
                txtDescripcion.Text = producto.Descripcion ?? string.Empty;
                if (producto.IdCategoria.HasValue)
                {
                    cmbCategoria.SelectedValue = producto.IdCategoria.Value;
                }
                else
                {
                    cmbCategoria.SelectedIndex = -1;
                }

                if (producto.IdMarca.HasValue)
                {
                    cmbMarca.SelectedValue = producto.IdMarca.Value;
                }
                else
                {
                    cmbMarca.SelectedIndex = -1;
                }

                txtNombre.ReadOnly = true;
                txtDescripcion.ReadOnly = true;
                cmbCategoria.Enabled = false;
                cmbMarca.Enabled = false;
                lblRegistrar.Text = $"Agregar variantes al producto #{producto.IdProducto}";
                lblRegistrarAyuda.Text = $"{producto.NombreProducto}: selecciona talla, color, stock y precios.";
                lblVariante.Text = "Nueva variante del producto";
                btnGuardarProducto.Visible = false;
                btnAgregarVariante.Visible = true;
                btnAgregarVariante.Enabled = true;
                btnLimpiar.Text = "Nuevo producto";
                LimpiarCamposVariante();
                cmbTalla.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Productos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCamposVariante()
        {
            cmbTalla.SelectedIndex = -1;
            cmbColor.SelectedIndex = -1;
            nudStock.Value = 0;
            nudStockMinimo.Value = 0;
            nudPrecioCompra.Value = 0;
            nudPrecioVenta.Value = 0;
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            NavegacionPanel.Abrir(this, new Frm_catalogo_rapido(TipoCatalogoRapido.Categoria, this));
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            NavegacionPanel.Abrir(this, new Frm_catalogo_rapido(TipoCatalogoRapido.Marca, this));
        }

        private void btnTallas_Click(object sender, EventArgs e)
        {
            NavegacionPanel.Abrir(this, new Frm_catalogo_rapido(TipoCatalogoRapido.Talla, this));
        }

        private void btnColores_Click(object sender, EventArgs e)
        {
            NavegacionPanel.Abrir(this, new Frm_catalogo_rapido(TipoCatalogoRapido.Color, this));
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            NavegacionPanel.Abrir(this, new Frm_producto_listado(this));
        }

        private void txtBuscarResumen_TextChanged(object sender, EventArgs e)
        {
            _timerBusqueda.Stop();
            _timerBusqueda.Start();
        }

        private void cmbOrdenResumen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated)
            {
                AplicarBusquedaResumen();
            }
        }

        private void dgvResumen_SelectionChanged(object sender, EventArgs e)
        {
            btnAgregarVariantesExistente.Enabled = dgvResumen.SelectedRows.Count > 0;
        }

        private async void btnAgregarVariantesExistente_Click(object sender, EventArgs e)
        {
            if (dgvResumen.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idProducto = Convert.ToInt32(dgvResumen.SelectedRows[0].Cells[0].Value);
            await ActivarModoAgregarVariantesAsync(idProducto);
        }

        private async void dgvResumen_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            int idProducto = Convert.ToInt32(dgvResumen.Rows[e.RowIndex].Cells[0].Value);
            await ActivarModoAgregarVariantesAsync(idProducto);
        }
    }
}
