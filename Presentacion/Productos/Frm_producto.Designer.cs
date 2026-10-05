namespace Nk_Colletion_New.Presentacion.Productos
{
    partial class Frm_producto
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlEncabezado = new Panel();
            btnVerTodos = new Button();
            btnColores = new Button();
            btnTallas = new Button();
            btnMarcas = new Button();
            btnCategorias = new Button();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            pnlRegistro = new Panel();
            btnGuardarProducto = new Button();
            btnLimpiar = new Button();
            pnlVariante = new Panel();
            btnAgregarVariante = new Button();
            nudPrecioVenta = new NumericUpDown();
            nudPrecioCompra = new NumericUpDown();
            nudStockMinimo = new NumericUpDown();
            nudStock = new NumericUpDown();
            cmbColor = new ComboBox();
            cmbTalla = new ComboBox();
            lblPrecioVenta = new Label();
            lblPrecioCompra = new Label();
            lblStockMinimo = new Label();
            lblStock = new Label();
            lblColor = new Label();
            lblTalla = new Label();
            lblVariante = new Label();
            cmbMarca = new ComboBox();
            lblMarca = new Label();
            cmbCategoria = new ComboBox();
            lblCategoria = new Label();
            txtDescripcion = new TextBox();
            lblDescripcion = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            lblRegistrarAyuda = new Label();
            lblRegistrar = new Label();
            pnlResumen = new Panel();
            btnAgregarVariantesExistente = new Button();
            txtBuscarResumen = new TextBox();
            cmbOrdenResumen = new ComboBox();
            lblOrdenResumen = new Label();
            lblResumenTotal = new Label();
            lblResumen = new Label();
            dgvResumen = new DataGridView();
            colIdProducto = new DataGridViewTextBoxColumn();
            colProducto = new DataGridViewTextBoxColumn();
            colMarcaResumen = new DataGridViewTextBoxColumn();
            colCategoriaResumen = new DataGridViewTextBoxColumn();
            colVariantesResumen = new DataGridViewTextBoxColumn();
            colStockResumen = new DataGridViewTextBoxColumn();
            colPrecioResumen = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            colEstadoResumen = new DataGridViewTextBoxColumn();
            pnlEncabezado.SuspendLayout();
            pnlRegistro.SuspendLayout();
            pnlVariante.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrecioVenta).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecioCompra).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).BeginInit();
            pnlResumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResumen).BeginInit();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlEncabezado.Controls.Add(btnVerTodos);
            pnlEncabezado.Controls.Add(btnColores);
            pnlEncabezado.Controls.Add(btnTallas);
            pnlEncabezado.Controls.Add(btnMarcas);
            pnlEncabezado.Controls.Add(btnCategorias);
            pnlEncabezado.Controls.Add(lblSubtitulo);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(1556, 105);
            pnlEncabezado.TabIndex = 2;
            // 
            // btnVerTodos
            // 
            btnVerTodos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerTodos.Location = new Point(1307, 34);
            btnVerTodos.Name = "btnVerTodos";
            btnVerTodos.Size = new Size(237, 45);
            btnVerTodos.TabIndex = 0;
            btnVerTodos.Text = "Ver todos los productos";
            btnVerTodos.Click += btnVerTodos_Click;
            // 
            // btnColores
            // 
            btnColores.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnColores.Location = new Point(1169, 34);
            btnColores.Name = "btnColores";
            btnColores.Size = new Size(132, 45);
            btnColores.TabIndex = 1;
            btnColores.Text = "Nuevo color";
            btnColores.Click += btnColores_Click;
            // 
            // btnTallas
            // 
            btnTallas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTallas.Location = new Point(1037, 34);
            btnTallas.Name = "btnTallas";
            btnTallas.Size = new Size(126, 45);
            btnTallas.TabIndex = 2;
            btnTallas.Text = "Nueva talla";
            btnTallas.Click += btnTallas_Click;
            // 
            // btnMarcas
            // 
            btnMarcas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMarcas.Location = new Point(873, 34);
            btnMarcas.Name = "btnMarcas";
            btnMarcas.Size = new Size(154, 45);
            btnMarcas.TabIndex = 3;
            btnMarcas.Text = "Nueva marca";
            btnMarcas.Click += btnMarcas_Click;
            // 
            // btnCategorias
            // 
            btnCategorias.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCategorias.Location = new Point(689, 34);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.Size = new Size(178, 45);
            btnCategorias.TabIndex = 4;
            btnCategorias.Text = "Nueva categoría";
            btnCategorias.Click += btnCategorias_Click;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(38, 65);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(550, 23);
            lblSubtitulo.TabIndex = 5;
            lblSubtitulo.Text = "Registra productos y consulta la vista general agrupada por ID.";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(33, 17);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(203, 49);
            lblTitulo.TabIndex = 6;
            lblTitulo.Text = "Productos";
            // 
            // pnlRegistro
            // 
            pnlRegistro.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlRegistro.Controls.Add(btnGuardarProducto);
            pnlRegistro.Controls.Add(btnLimpiar);
            pnlRegistro.Controls.Add(pnlVariante);
            pnlRegistro.Controls.Add(cmbMarca);
            pnlRegistro.Controls.Add(lblMarca);
            pnlRegistro.Controls.Add(cmbCategoria);
            pnlRegistro.Controls.Add(lblCategoria);
            pnlRegistro.Controls.Add(txtDescripcion);
            pnlRegistro.Controls.Add(lblDescripcion);
            pnlRegistro.Controls.Add(txtNombre);
            pnlRegistro.Controls.Add(lblNombre);
            pnlRegistro.Controls.Add(lblRegistrarAyuda);
            pnlRegistro.Controls.Add(lblRegistrar);
            pnlRegistro.Location = new Point(28, 110);
            pnlRegistro.Name = "pnlRegistro";
            pnlRegistro.Size = new Size(1500, 355);
            pnlRegistro.TabIndex = 1;
            // 
            // btnGuardarProducto
            // 
            btnGuardarProducto.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardarProducto.Location = new Point(1274, 297);
            btnGuardarProducto.Name = "btnGuardarProducto";
            btnGuardarProducto.Size = new Size(185, 42);
            btnGuardarProducto.TabIndex = 0;
            btnGuardarProducto.Text = "Guardar producto";
            btnGuardarProducto.Click += btnGuardarProducto_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLimpiar.Location = new Point(1110, 297);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(145, 42);
            btnLimpiar.TabIndex = 1;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // pnlVariante
            // 
            pnlVariante.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlVariante.Controls.Add(btnAgregarVariante);
            pnlVariante.Controls.Add(nudPrecioVenta);
            pnlVariante.Controls.Add(nudPrecioCompra);
            pnlVariante.Controls.Add(nudStockMinimo);
            pnlVariante.Controls.Add(nudStock);
            pnlVariante.Controls.Add(cmbColor);
            pnlVariante.Controls.Add(cmbTalla);
            pnlVariante.Controls.Add(lblPrecioVenta);
            pnlVariante.Controls.Add(lblPrecioCompra);
            pnlVariante.Controls.Add(lblStockMinimo);
            pnlVariante.Controls.Add(lblStock);
            pnlVariante.Controls.Add(lblColor);
            pnlVariante.Controls.Add(lblTalla);
            pnlVariante.Controls.Add(lblVariante);
            pnlVariante.Location = new Point(31, 164);
            pnlVariante.Name = "pnlVariante";
            pnlVariante.Size = new Size(1428, 122);
            pnlVariante.TabIndex = 2;
            // 
            // btnAgregarVariante
            // 
            btnAgregarVariante.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAgregarVariante.Location = new Point(1232, 60);
            btnAgregarVariante.Name = "btnAgregarVariante";
            btnAgregarVariante.Size = new Size(174, 43);
            btnAgregarVariante.TabIndex = 0;
            btnAgregarVariante.Text = "+ Agregar variante";
            btnAgregarVariante.Visible = false;
            btnAgregarVariante.Click += btnAgregarVariante_Click;
            // 
            // nudPrecioVenta
            // 
            nudPrecioVenta.BorderStyle = BorderStyle.FixedSingle;
            nudPrecioVenta.DecimalPlaces = 2;
            nudPrecioVenta.Location = new Point(1070, 66);
            nudPrecioVenta.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudPrecioVenta.Name = "nudPrecioVenta";
            nudPrecioVenta.Size = new Size(142, 30);
            nudPrecioVenta.TabIndex = 1;
            nudPrecioVenta.ThousandsSeparator = true;
            // 
            // nudPrecioCompra
            // 
            nudPrecioCompra.BorderStyle = BorderStyle.FixedSingle;
            nudPrecioCompra.DecimalPlaces = 2;
            nudPrecioCompra.Location = new Point(904, 66);
            nudPrecioCompra.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudPrecioCompra.Name = "nudPrecioCompra";
            nudPrecioCompra.Size = new Size(142, 30);
            nudPrecioCompra.TabIndex = 2;
            nudPrecioCompra.ThousandsSeparator = true;
            // 
            // nudStockMinimo
            // 
            nudStockMinimo.BorderStyle = BorderStyle.FixedSingle;
            nudStockMinimo.Location = new Point(766, 66);
            nudStockMinimo.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudStockMinimo.Name = "nudStockMinimo";
            nudStockMinimo.Size = new Size(116, 30);
            nudStockMinimo.TabIndex = 3;
            // 
            // nudStock
            // 
            nudStock.BorderStyle = BorderStyle.FixedSingle;
            nudStock.Location = new Point(645, 66);
            nudStock.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(99, 30);
            nudStock.TabIndex = 4;
            // 
            // cmbColor
            // 
            cmbColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColor.ItemHeight = 30;
            cmbColor.Location = new Point(316, 59);
            cmbColor.Name = "cmbColor";
            cmbColor.Size = new Size(210, 36);
            cmbColor.TabIndex = 5;
            // 
            // cmbTalla
            // 
            cmbTalla.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTalla.ItemHeight = 30;
            cmbTalla.Location = new Point(24, 59);
            cmbTalla.Name = "cmbTalla";
            cmbTalla.Size = new Size(270, 36);
            cmbTalla.TabIndex = 6;
            // 
            // lblPrecioVenta
            // 
            lblPrecioVenta.AutoSize = true;
            lblPrecioVenta.Location = new Point(1070, 41);
            lblPrecioVenta.Name = "lblPrecioVenta";
            lblPrecioVenta.Size = new Size(99, 21);
            lblPrecioVenta.TabIndex = 7;
            lblPrecioVenta.Text = "Precio venta";
            // 
            // lblPrecioCompra
            // 
            lblPrecioCompra.AutoSize = true;
            lblPrecioCompra.Location = new Point(904, 41);
            lblPrecioCompra.Name = "lblPrecioCompra";
            lblPrecioCompra.Size = new Size(113, 21);
            lblPrecioCompra.TabIndex = 8;
            lblPrecioCompra.Text = "Precio compra";
            // 
            // lblStockMinimo
            // 
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Location = new Point(766, 41);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(112, 21);
            lblStockMinimo.TabIndex = 9;
            lblStockMinimo.Text = "Stock mínimo";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(645, 41);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(51, 21);
            lblStock.TabIndex = 10;
            lblStock.Text = "Stock";
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Location = new Point(316, 35);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(51, 21);
            lblColor.TabIndex = 11;
            lblColor.Text = "Color";
            // 
            // lblTalla
            // 
            lblTalla.AutoSize = true;
            lblTalla.Location = new Point(24, 35);
            lblTalla.Name = "lblTalla";
            lblTalla.Size = new Size(47, 21);
            lblTalla.TabIndex = 12;
            lblTalla.Text = "Talla";
            // 
            // lblVariante
            // 
            lblVariante.AutoSize = true;
            lblVariante.Location = new Point(20, 8);
            lblVariante.Name = "lblVariante";
            lblVariante.Size = new Size(262, 23);
            lblVariante.TabIndex = 13;
            lblVariante.Text = "Variante inicial del producto";
            // 
            // cmbMarca
            // 
            cmbMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarca.ItemHeight = 30;
            cmbMarca.Location = new Point(1120, 110);
            cmbMarca.Name = "cmbMarca";
            cmbMarca.Size = new Size(339, 36);
            cmbMarca.TabIndex = 3;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Location = new Point(1120, 85);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(60, 22);
            lblMarca.TabIndex = 4;
            lblMarca.Text = "Marca";
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.ItemHeight = 30;
            cmbCategoria.Location = new Point(780, 110);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(318, 36);
            cmbCategoria.TabIndex = 5;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(780, 85);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(86, 22);
            lblCategoria.TabIndex = 6;
            lblCategoria.Text = "Categoría";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(391, 110);
            txtDescripcion.Margin = new Padding(4, 5, 4, 5);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "Descripción opcional";
            txtDescripcion.Size = new Size(367, 36);
            txtDescripcion.TabIndex = 7;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(391, 85);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(105, 22);
            lblDescripcion.TabIndex = 8;
            lblDescripcion.Text = "Descripción";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(31, 110);
            txtNombre.Margin = new Padding(4, 5, 4, 5);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Ej. Camisa deportiva Nike";
            txtNombre.Size = new Size(338, 36);
            txtNombre.TabIndex = 9;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(31, 85);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(176, 22);
            lblNombre.TabIndex = 10;
            lblNombre.Text = "Nombre del producto";
            // 
            // lblRegistrarAyuda
            // 
            lblRegistrarAyuda.AutoSize = true;
            lblRegistrarAyuda.Location = new Point(31, 49);
            lblRegistrarAyuda.Name = "lblRegistrarAyuda";
            lblRegistrarAyuda.Size = new Size(624, 22);
            lblRegistrarAyuda.TabIndex = 11;
            lblRegistrarAyuda.Text = "Completa los datos del producto y define su primera combinación de talla y color.";
            // 
            // lblRegistrar
            // 
            lblRegistrar.AutoSize = true;
            lblRegistrar.Location = new Point(26, 17);
            lblRegistrar.Name = "lblRegistrar";
            lblRegistrar.Size = new Size(216, 28);
            lblRegistrar.TabIndex = 12;
            lblRegistrar.Text = "Registrar producto";
            // 
            // pnlResumen
            // 
            pnlResumen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlResumen.Controls.Add(btnAgregarVariantesExistente);
            pnlResumen.Controls.Add(txtBuscarResumen);
            pnlResumen.Controls.Add(cmbOrdenResumen);
            pnlResumen.Controls.Add(lblOrdenResumen);
            pnlResumen.Controls.Add(lblResumenTotal);
            pnlResumen.Controls.Add(lblResumen);
            pnlResumen.Controls.Add(dgvResumen);
            pnlResumen.Location = new Point(28, 478);
            pnlResumen.Name = "pnlResumen";
            pnlResumen.Size = new Size(1500, 363);
            pnlResumen.TabIndex = 0;
            // 
            // btnAgregarVariantesExistente
            // 
            btnAgregarVariantesExistente.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAgregarVariantesExistente.Enabled = false;
            btnAgregarVariantesExistente.Location = new Point(617, 23);
            btnAgregarVariantesExistente.Name = "btnAgregarVariantesExistente";
            btnAgregarVariantesExistente.Size = new Size(223, 42);
            btnAgregarVariantesExistente.TabIndex = 0;
            btnAgregarVariantesExistente.Text = "+ Agregar variantes";
            btnAgregarVariantesExistente.Click += btnAgregarVariantesExistente_Click;
            // 
            // txtBuscarResumen
            // 
            txtBuscarResumen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscarResumen.Location = new Point(1043, 22);
            txtBuscarResumen.Margin = new Padding(4, 5, 4, 5);
            txtBuscarResumen.Name = "txtBuscarResumen";
            txtBuscarResumen.PlaceholderText = "Buscar producto...";
            txtBuscarResumen.Size = new Size(417, 42);
            txtBuscarResumen.TabIndex = 1;
            txtBuscarResumen.TextChanged += txtBuscarResumen_TextChanged;
            // 
            // cmbOrdenResumen
            // 
            cmbOrdenResumen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbOrdenResumen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrdenResumen.ItemHeight = 28;
            cmbOrdenResumen.Location = new Point(860, 30);
            cmbOrdenResumen.Name = "cmbOrdenResumen";
            cmbOrdenResumen.Size = new Size(160, 34);
            cmbOrdenResumen.TabIndex = 2;
            cmbOrdenResumen.SelectedIndexChanged += cmbOrdenResumen_SelectedIndexChanged;
            // 
            // lblOrdenResumen
            // 
            lblOrdenResumen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOrdenResumen.AutoSize = true;
            lblOrdenResumen.Location = new Point(860, 8);
            lblOrdenResumen.Name = "lblOrdenResumen";
            lblOrdenResumen.Size = new Size(54, 19);
            lblOrdenResumen.TabIndex = 3;
            lblOrdenResumen.Text = "Orden";
            // 
            // lblResumenTotal
            // 
            lblResumenTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblResumenTotal.AutoSize = true;
            lblResumenTotal.Location = new Point(31, 329);
            lblResumenTotal.Name = "lblResumenTotal";
            lblResumenTotal.Size = new Size(341, 22);
            lblResumenTotal.TabIndex = 4;
            lblResumenTotal.Text = "Mostrando 0 producto(s) agrupados por ID";
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.Location = new Point(26, 16);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(306, 28);
            lblResumen.TabIndex = 6;
            lblResumen.Text = "Vista general de productos";
            // 
            // dgvResumen
            // 
            dgvResumen.AllowUserToAddRows = false;
            dgvResumen.AllowUserToDeleteRows = false;
            dgvResumen.AllowUserToResizeRows = false;
            dgvResumen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResumen.ColumnHeadersHeight = 40;
            dgvResumen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvResumen.Columns.AddRange(new DataGridViewColumn[] { colIdProducto, colProducto, colMarcaResumen, colCategoriaResumen, colVariantesResumen, colStockResumen, colPrecioResumen, Column1, colEstadoResumen });
            dgvResumen.GridColor = Color.FromArgb(239, 226, 229);
            dgvResumen.Location = new Point(31, 82);
            dgvResumen.MultiSelect = false;
            dgvResumen.Name = "dgvResumen";
            dgvResumen.ReadOnly = true;
            dgvResumen.RowHeadersVisible = false;
            dgvResumen.RowHeadersWidth = 62;
            dgvResumen.RowTemplate.Height = 36;
            dgvResumen.Size = new Size(1429, 232);
            dgvResumen.TabIndex = 7;
            dgvResumen.CellDoubleClick += dgvResumen_CellDoubleClick;
            dgvResumen.SelectionChanged += dgvResumen_SelectionChanged;
            // 
            // colIdProducto
            // 
            colIdProducto.HeaderText = "ID";
            colIdProducto.MinimumWidth = 8;
            colIdProducto.Name = "colIdProducto";
            colIdProducto.ReadOnly = true;
            // 
            // colProducto
            // 
            colProducto.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colProducto.FillWeight = 140F;
            colProducto.HeaderText = "Producto";
            colProducto.MinimumWidth = 8;
            colProducto.Name = "colProducto";
            colProducto.ReadOnly = true;
            // 
            // colMarcaResumen
            // 
            colMarcaResumen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colMarcaResumen.HeaderText = "Marca";
            colMarcaResumen.MinimumWidth = 8;
            colMarcaResumen.Name = "colMarcaResumen";
            colMarcaResumen.ReadOnly = true;
            // 
            // colCategoriaResumen
            // 
            colCategoriaResumen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCategoriaResumen.HeaderText = "Categoría";
            colCategoriaResumen.MinimumWidth = 8;
            colCategoriaResumen.Name = "colCategoriaResumen";
            colCategoriaResumen.ReadOnly = true;
            // 
            // colVariantesResumen
            // 
            colVariantesResumen.HeaderText = "Variantes";
            colVariantesResumen.MinimumWidth = 8;
            colVariantesResumen.Name = "colVariantesResumen";
            colVariantesResumen.ReadOnly = true;
            // 
            // colStockResumen
            // 
            colStockResumen.HeaderText = "Stock total";
            colStockResumen.MinimumWidth = 8;
            colStockResumen.Name = "colStockResumen";
            colStockResumen.ReadOnly = true;
            // 
            // colPrecioResumen
            // 
            colPrecioResumen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colPrecioResumen.HeaderText = "Precio venta";
            colPrecioResumen.MinimumWidth = 8;
            colPrecioResumen.Name = "colPrecioResumen";
            colPrecioResumen.ReadOnly = true;
            // 
            // Column1
            // 
            Column1.HeaderText = "Precio Compra";
            Column1.MinimumWidth = 8;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // colEstadoResumen
            // 
            colEstadoResumen.HeaderText = "Estado";
            colEstadoResumen.MinimumWidth = 8;
            colEstadoResumen.Name = "colEstadoResumen";
            colEstadoResumen.ReadOnly = true;
            // 
            // Frm_producto
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 241, 242);
            ClientSize = new Size(1556, 867);
            Controls.Add(pnlResumen);
            Controls.Add(pnlRegistro);
            Controls.Add(pnlEncabezado);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Frm_producto";
            Text = "Productos";
            Load += Frm_producto_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlRegistro.ResumeLayout(false);
            pnlRegistro.PerformLayout();
            pnlVariante.ResumeLayout(false);
            pnlVariante.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrecioVenta).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudPrecioCompra).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStock).EndInit();
            pnlResumen.ResumeLayout(false);
            pnlResumen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResumen).EndInit();
            ResumeLayout(false);
        }


        #endregion

        private Panel pnlEncabezado;
        private Button btnVerTodos;
        private Button btnMarcas;
        private Button btnCategorias;
        private Label lblSubtitulo;
        private Label lblTitulo;
        private Panel pnlRegistro;
        private Button btnGuardarProducto;
        private Button btnLimpiar;
        private Panel pnlVariante;
        private Button btnColores;
        private Button btnTallas;
        private Button btnAgregarVariante;
        private NumericUpDown nudPrecioVenta;
        private NumericUpDown nudPrecioCompra;
        private NumericUpDown nudStockMinimo;
        private NumericUpDown nudStock;
        private ComboBox cmbColor;
        private ComboBox cmbTalla;
        private Label lblPrecioVenta;
        private Label lblPrecioCompra;
        private Label lblStockMinimo;
        private Label lblStock;
        private Label lblColor;
        private Label lblTalla;
        private Label lblVariante;
        private ComboBox cmbMarca;
        private Label lblMarca;
        private ComboBox cmbCategoria;
        private Label lblCategoria;
        private TextBox txtDescripcion;
        private Label lblDescripcion;
        private TextBox txtNombre;
        private Label lblNombre;
        private Label lblRegistrarAyuda;
        private Label lblRegistrar;
        private Panel pnlResumen;
        private Button btnAgregarVariantesExistente;
        private TextBox txtBuscarResumen;
        private ComboBox cmbOrdenResumen;
        private Label lblOrdenResumen;
        private Label lblResumenTotal;
        private Label lblResumen;
        private DataGridView dgvResumen;
        private DataGridViewTextBoxColumn colIdProducto;
        private DataGridViewTextBoxColumn colProducto;
        private DataGridViewTextBoxColumn colMarcaResumen;
        private DataGridViewTextBoxColumn colCategoriaResumen;
        private DataGridViewTextBoxColumn colVariantesResumen;
        private DataGridViewTextBoxColumn colStockResumen;
        private DataGridViewTextBoxColumn colPrecioResumen;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn colEstadoResumen;
    }
}