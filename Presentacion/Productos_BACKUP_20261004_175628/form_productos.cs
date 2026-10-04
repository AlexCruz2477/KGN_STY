using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Metodos_Ordenamiento;
using Nk_Colletion_New.Negocios.Servicios.Productos;
using Nk_Colletion_New.Presentacion.Productos;

namespace Nk_Colletion_New
{
    public partial class form_Productos : Form
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

        public form_Productos()
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
            }

            // Configure order combo (guna2ComboBox3 in Designer)
            guna2ComboBox3.Items.Clear();
            guna2ComboBox3.Items.Add("A - Z");
            guna2ComboBox3.Items.Add("Z - A");
            guna2ComboBox3.SelectedIndex = 0;

            // wire runtime events not set in Designer
            guna2TextBox2.TextChanged += (_, _) =>
            {
                _timerBusqueda.Stop();
                _timerBusqueda.Start();
            };

            guna2ComboBox3.SelectedIndexChanged += (_, _) =>
            {
                if (IsHandleCreated)
                    AplicarBusquedaResumen();
            };

            dgvProductos.SelectionChanged += (_, _) => button1.Enabled = dgvProductos.SelectedRows.Count > 0;
            dgvProductos.CellDoubleClick += async (_, e) =>
            {
                if (e.RowIndex < 0) return;
                int idProducto = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells[0].Value);
                await ActivarModoAgregarVariantesAsync(idProducto);
            };

            btnAgregarVariante.Click += btnAgregarVariante_Click;
            btnGuardarProducto.Click += btnGuardarProducto_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnNuevaCategoria.Click += btnCategorias_Click;
            btnNuevaMarca.Click += btnMarcas_Click;
            btnNuevaTalla.Click += btnTallas_Click;
            btnNuevoColor.Click += btnColores_Click;
            btnVerTodos.Click += btnVerTodos_Click;
            button1.Click += async (_, _) =>
            {
                if (dgvProductos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un producto.", "Productos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int idProducto = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells[0].Value);
                await ActivarModoAgregarVariantesAsync(idProducto);
            };

            _timerBusqueda.Tick += (_, _) =>
            {
                _timerBusqueda.Stop();
                AplicarBusquedaResumen();
            };

            ActivarModoNuevoProducto(false);
        }

        private async void form_Productos_Load_1(object sender, EventArgs e)
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

        private static void ConfigurarCombo(System.Windows.Forms.ComboBox combo, object datos, string displayMember, string valueMember)
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

        private static int? ObtenerId(System.Windows.Forms.ComboBox combo)
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
            bool ascendente = guna2ComboBox3.SelectedIndex != 1;

            var productos = _arbolProductos.BuscarYFiltrar(
                guna2TextBox2.Text,
                producto => new[]
                {
                    producto.NombreProducto,
                    producto.IdMarcaNavigation?.Nombre,
                    producto.IdCategoriaNavigation?.NombreCategoria,
                    producto.IdProducto.ToString()
                },
                ascendente: ascendente
            );

            dgvProductos.Rows.Clear();

            foreach (var producto in productos)
            {
                var variantes = producto.ProductoVariantes?.ToList() ?? new List<ProductoVariante>();
                int cantidadVariantes = variantes.Count;

                int stockTotal = variantes.Sum(v => v.StockActual);

                decimal precioMinimo = cantidadVariantes == 0
                    ? 0
                    : variantes.Min(v => v.PrecioVenta);

                decimal precioMaximo = cantidadVariantes == 0
                    ? 0
                    : variantes.Max(v => v.PrecioVenta);

                string precio = cantidadVariantes == 0
                    ? "C$ 0.00"
                    : precioMinimo == precioMaximo
                        ? $"C${precioMinimo:N2}"
                        : $"C${precioMinimo:N2} - {precioMaximo:N2}";

                dgvProductos.Rows.Add(
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

            dgvProductos.ClearSelection();
            dgvProductos.CurrentCell = null;
            button1.Enabled = false;
            lblCount.Text = $"Mostrando {productos.Count} producto(s) agrupados por ID";
        }

        //validar variante

        private bool ValidarVarianteFormulario()
        {
            if (ObtenerId(cmbTalla) is null)
            {
                MessageBox.Show("Seleccione una talla.", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbTalla.Focus();
                return false;
            }

            if (ObtenerId(cmbColor) is null)
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
                    Convert.ToInt32(numStock.Value),
                    Convert.ToInt32(numStockMin.Value),
                    numPrecioCompra.Value,
                    numPrecioVenta.Value
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
                    txtNombreProducto.Text,
                    txtDescripcion.Text,
                    ObtenerId(cmbCategoria),
                    ObtenerId(cmbMarca),
                    ObtenerId(cmbTalla),
                    ObtenerId(cmbColor),
                    Convert.ToInt32(numStock.Value),
                    Convert.ToInt32(numStockMin.Value),
                    numPrecioCompra.Value,
                    numPrecioVenta.Value
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

            txtNombreProducto.ReadOnly = false;
            txtDescripcion.ReadOnly = false;

            cmbCategoria.Enabled = true;
            cmbMarca.Enabled = true;

            lblRegistrar.Text = "Registrar producto";
            lblRegistrarAyuda.Text = "Completa los datos del producto y define su primera combinación de talla y color.";
            lblVariante.Text = "Variante inicial del producto";

            btnGuardarProducto.Visible = true;
            btnGuardarProducto.Enabled = true;

            btnAgregarVariante.Visible = false;
            btnAgregarVariante.Enabled = false;

            btnLimpiar.Text = "Limpiar";

            if (!limpiarCampos)
            {
                return;
            }

            txtNombreProducto.Clear();
            txtDescripcion.Clear();
            cmbCategoria.SelectedIndex = -1;
            cmbMarca.SelectedIndex = -1;

            LimpiarCamposVariante();

            txtNombreProducto.Focus();
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
                    MessageBox.Show(
                        "El producto está inactivo.",
                        "Productos",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                _idProductoParaVariantes = producto.IdProducto;

                txtNombreProducto.Text = producto.NombreProducto;
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

                txtNombreProducto.ReadOnly = true;
                txtDescripcion.ReadOnly = true;

                cmbCategoria.Enabled = false;
                cmbMarca.Enabled = false;

                lblRegistrar.Text = $"Agregar variantes al producto #{producto.IdProducto}";
                lblRegistrarAyuda.Text = $"Producto: {producto.NombreProducto}. Selecciona talla, color, stock y precios.";

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
            NavegacionPanel.Abrir(
                this,
                new Frm_catalogo_rapido(AppConfig.DbOptions!, Presentacion.Productos.Frm_catalogo_rapido.TipoCatalogoRapido.Categoria, this)
            );
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            NavegacionPanel.Abrir(
                this,
                new Frm_catalogo_rapido(AppConfig.DbOptions!, Presentacion.Productos.Frm_catalogo_rapido.TipoCatalogoRapido.Marca, this)
            );
        }

        private void btnTallas_Click(object sender, EventArgs e)
        {
            NavegacionPanel.Abrir(
                this,
                new Frm_catalogo_rapido(AppConfig.DbOptions!, Presentacion.Productos.Frm_catalogo_rapido.TipoCatalogoRapido.Talla, this)
            );
        }

        private void btnColores_Click(object sender, EventArgs e)
        {
            NavegacionPanel.Abrir(
                this,
                new Frm_catalogo_rapido(AppConfig.DbOptions!, Presentacion.Productos.Frm_catalogo_rapido.TipoCatalogoRapido.Color, this)
            );
        }

        private void btnVerTodos_Click(object sender, EventArgs e)
        {
            NavegacionPanel.Abrir(
                this,
                new Frm_producto_listado(this)
            );
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
                MessageBox.Show(
                    "Seleccione un producto.",
                    "Productos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

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

        private void pnlEncabezado_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dgvResumen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}

    

