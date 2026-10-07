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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            pnlEncabezado = new Panel();
            btnVerTodos = new Button();
            btnColores = new Button();
            btnTallas = new Button();
            btnMarcas = new Button();
            btnCategorias = new Button();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            pnlRegistro = new Panel();
            btnAgregarVariante = new Button();
            btnGuardarProducto = new Button();
            btnLimpiar = new Button();
            pnlVariante = new Panel();
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
            btnAgregarVariantesExistente = new Button();
            pnlResumen = new Panel();
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
            pnlEncabezado.BackColor = Color.FromArgb(232, 221, 202);
            pnlEncabezado.Controls.Add(btnVerTodos);
            pnlEncabezado.Controls.Add(btnColores);
            pnlEncabezado.Controls.Add(btnTallas);
            pnlEncabezado.Controls.Add(btnMarcas);
            pnlEncabezado.Controls.Add(btnCategorias);
            pnlEncabezado.Controls.Add(lblSubtitulo);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(1556, 112);
            pnlEncabezado.TabIndex = 2;
            // 
            // btnVerTodos
            // 
            btnVerTodos.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVerTodos.Cursor = Cursors.Hand;
            btnVerTodos.FlatAppearance.BorderColor = Color.Black;
            btnVerTodos.BackColor = Color.FromArgb(100, 28, 45);
            btnVerTodos.ForeColor = Color.White;
            btnVerTodos.FlatAppearance.MouseOverBackColor = Color.FromArgb(125, 35, 56);
            btnVerTodos.FlatStyle = FlatStyle.Flat;
            
            btnVerTodos.Location = new Point(1314, 18);
            btnVerTodos.Name = "btnVerTodos";
            btnVerTodos.Size = new Size(230, 40);
            btnVerTodos.TabIndex = 0;
            btnVerTodos.Text = "Ver todos los productos";
            btnVerTodos.UseVisualStyleBackColor = false;
            btnVerTodos.Click += btnVerTodos_Click;
            // 
            // btnColores
            // 
            btnColores.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnColores.Cursor = Cursors.Hand;
            btnColores.FlatAppearance.BorderColor = Color.Black;
            btnColores.BackColor = Color.FromArgb(100, 28, 45);
            btnColores.ForeColor = Color.White;
            btnColores.FlatAppearance.MouseOverBackColor = Color.FromArgb(125, 35, 56);
            btnColores.FlatStyle = FlatStyle.Flat;
            
            btnColores.Location = new Point(1172, 18);
            btnColores.Name = "btnColores";
            btnColores.Size = new Size(134, 40);
            btnColores.TabIndex = 1;
            btnColores.Text = "Nuevo color";
            btnColores.UseVisualStyleBackColor = false;
            btnColores.Click += btnColores_Click;
            // 
            // btnTallas
            // 
            btnTallas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTallas.Cursor = Cursors.Hand;
            btnTallas.FlatAppearance.BorderColor = Color.Black;
            btnTallas.BackColor = Color.FromArgb(100, 28, 45);
            btnTallas.ForeColor = Color.White;
            btnTallas.FlatAppearance.MouseOverBackColor = Color.FromArgb(125, 35, 56);
            btnTallas.FlatStyle = FlatStyle.Flat;
            
            btnTallas.Location = new Point(1042, 18);
            btnTallas.Name = "btnTallas";
            btnTallas.Size = new Size(122, 40);
            btnTallas.TabIndex = 2;
            btnTallas.Text = "Nueva talla";
            btnTallas.UseVisualStyleBackColor = false;
            btnTallas.Click += btnTallas_Click;
            // 
            // btnMarcas
            // 
            btnMarcas.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMarcas.Cursor = Cursors.Hand;
            btnMarcas.FlatAppearance.BorderColor = Color.Black;
            btnMarcas.BackColor = Color.FromArgb(100, 28, 45);
            btnMarcas.ForeColor = Color.White;
            btnMarcas.FlatAppearance.MouseOverBackColor = Color.FromArgb(125, 35, 56);
            btnMarcas.FlatStyle = FlatStyle.Flat;
            
            btnMarcas.Location = new Point(884, 18);
            btnMarcas.Name = "btnMarcas";
            btnMarcas.Size = new Size(148, 40);
            btnMarcas.TabIndex = 3;
            btnMarcas.Text = "Nueva marca";
            btnMarcas.UseVisualStyleBackColor = false;
            btnMarcas.Click += btnMarcas_Click;
            // 
            // btnCategorias
            // 
            btnCategorias.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCategorias.Cursor = Cursors.Hand;
            btnCategorias.FlatAppearance.BorderColor = Color.Black;
            btnCategorias.BackColor = Color.FromArgb(100, 28, 45);
            btnCategorias.ForeColor = Color.White;
            btnCategorias.FlatAppearance.MouseOverBackColor = Color.FromArgb(125, 35, 56);
            btnCategorias.FlatStyle = FlatStyle.Flat;
            
            btnCategorias.Location = new Point(704, 18);
            btnCategorias.Name = "btnCategorias";
            btnCategorias.Size = new Size(178, 40);
            btnCategorias.TabIndex = 4;
            btnCategorias.Text = "Nueva categoría";
            btnCategorias.UseVisualStyleBackColor = false;
            btnCategorias.Click += btnCategorias_Click;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(38, 65);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(510, 25);
            lblSubtitulo.TabIndex = 5;
            lblSubtitulo.Text = "Registra productos y consulta la vista general agrupada por ID.";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(33, 17);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(93, 25);
            lblTitulo.TabIndex = 6;
            lblTitulo.Text = "Productos";
            // 
            // pnlRegistro
            // 
            pnlRegistro.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlRegistro.BackColor = Color.FromArgb(232, 221, 202);
            pnlRegistro.Controls.Add(btnAgregarVariante);
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
            pnlRegistro.Location = new Point(28, 124);
            pnlRegistro.Name = "pnlRegistro";
            pnlRegistro.Size = new Size(1500, 346);
            pnlRegistro.TabIndex = 1;
            // 
            // btnAgregarVariante
            // 
            btnAgregarVariante.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAgregarVariante.Cursor = Cursors.Hand;
            btnAgregarVariante.FlatAppearance.BorderSize = 1;
            btnAgregarVariante.FlatAppearance.MouseDownBackColor = Color.FromArgb(232, 221, 202);
            btnAgregarVariante.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 221, 202);
            btnAgregarVariante.FlatStyle = FlatStyle.Flat;
            btnAgregarVariante.ForeColor = Color.FromArgb(41, 41, 41);
            btnAgregarVariante.Location = new Point(904, 296);
            btnAgregarVariante.Name = "btnAgregarVariante";
            btnAgregarVariante.Size = new Size(174, 43);
            btnAgregarVariante.TabIndex = 0;
            btnAgregarVariante.Text = "+ Agregar variante";
            btnAgregarVariante.UseVisualStyleBackColor = false;
            btnAgregarVariante.Visible = false;
            btnAgregarVariante.Click += btnAgregarVariante_Click;
            // 
            // btnGuardarProducto
            // 
            btnGuardarProducto.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGuardarProducto.Cursor = Cursors.Hand;
            btnGuardarProducto.FlatAppearance.BorderSize = 1;
            btnGuardarProducto.FlatAppearance.MouseDownBackColor = Color.FromArgb(232, 221, 202);
            btnGuardarProducto.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 221, 202);
            btnGuardarProducto.FlatStyle = FlatStyle.Flat;
            btnGuardarProducto.ForeColor = Color.FromArgb(41, 41, 41);
            btnGuardarProducto.Location = new Point(1274, 296);
            btnGuardarProducto.Name = "btnGuardarProducto";
            btnGuardarProducto.Size = new Size(185, 42);
            btnGuardarProducto.TabIndex = 0;
            btnGuardarProducto.Text = "Guardar producto";
            btnGuardarProducto.UseVisualStyleBackColor = false;
            btnGuardarProducto.Click += btnGuardarProducto_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatAppearance.BorderColor = Color.Black;
            btnLimpiar.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 221, 202);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.ForeColor = Color.FromArgb(41, 41, 41);
            btnLimpiar.Location = new Point(1110, 296);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(145, 42);
            btnLimpiar.TabIndex = 1;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // pnlVariante
            // 
            pnlVariante.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlVariante.BackColor = Color.FromArgb(232, 221, 202);
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
            // nudPrecioVenta
            // 
            nudPrecioVenta.BackColor = Color.FromArgb(232, 221, 202);
            nudPrecioVenta.BorderStyle = BorderStyle.FixedSingle;
            nudPrecioVenta.DecimalPlaces = 2;
            nudPrecioVenta.ForeColor = Color.FromArgb(41, 41, 41);
            nudPrecioVenta.Location = new Point(939, 66);
            nudPrecioVenta.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudPrecioVenta.Name = "nudPrecioVenta";
            nudPrecioVenta.Size = new Size(142, 31);
            nudPrecioVenta.TabIndex = 1;
            nudPrecioVenta.ThousandsSeparator = true;
            // 
            // nudPrecioCompra
            // 
            nudPrecioCompra.BackColor = Color.FromArgb(232, 221, 202);
            nudPrecioCompra.BorderStyle = BorderStyle.FixedSingle;
            nudPrecioCompra.DecimalPlaces = 2;
            nudPrecioCompra.ForeColor = Color.FromArgb(41, 41, 41);
            nudPrecioCompra.Location = new Point(1096, 66);
            nudPrecioCompra.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            nudPrecioCompra.Name = "nudPrecioCompra";
            nudPrecioCompra.Size = new Size(142, 31);
            nudPrecioCompra.TabIndex = 2;
            nudPrecioCompra.ThousandsSeparator = true;
            // 
            // nudStockMinimo
            // 
            nudStockMinimo.BackColor = Color.FromArgb(232, 221, 202);
            nudStockMinimo.BorderStyle = BorderStyle.FixedSingle;
            nudStockMinimo.ForeColor = Color.FromArgb(41, 41, 41);
            nudStockMinimo.Location = new Point(766, 66);
            nudStockMinimo.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudStockMinimo.Name = "nudStockMinimo";
            nudStockMinimo.Size = new Size(116, 31);
            nudStockMinimo.TabIndex = 3;
            // 
            // nudStock
            // 
            nudStock.BackColor = Color.FromArgb(232, 221, 202);
            nudStock.BorderStyle = BorderStyle.FixedSingle;
            nudStock.ForeColor = Color.FromArgb(41, 41, 41);
            nudStock.Location = new Point(645, 66);
            nudStock.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudStock.Name = "nudStock";
            nudStock.Size = new Size(99, 31);
            nudStock.TabIndex = 4;
            // 
            // cmbColor
            // 
            cmbColor.BackColor = Color.FromArgb(232, 221, 202);
            cmbColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColor.FlatStyle = FlatStyle.Flat;
            cmbColor.ForeColor = Color.FromArgb(41, 41, 41);
            cmbColor.ItemHeight = 25;
            cmbColor.Location = new Point(316, 59);
            cmbColor.Name = "cmbColor";
            cmbColor.Size = new Size(210, 33);
            cmbColor.TabIndex = 5;
            // 
            // cmbTalla
            // 
            cmbTalla.BackColor = Color.FromArgb(232, 221, 202);
            cmbTalla.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTalla.FlatStyle = FlatStyle.Flat;
            cmbTalla.ForeColor = Color.FromArgb(41, 41, 41);
            cmbTalla.ItemHeight = 25;
            cmbTalla.Location = new Point(24, 59);
            cmbTalla.Name = "cmbTalla";
            cmbTalla.Size = new Size(270, 33);
            cmbTalla.TabIndex = 6;
            // 
            // lblPrecioVenta
            // 
            lblPrecioVenta.AutoSize = true;
            lblPrecioVenta.Location = new Point(939, 41);
            lblPrecioVenta.Name = "lblPrecioVenta";
            lblPrecioVenta.Size = new Size(108, 25);
            lblPrecioVenta.TabIndex = 7;
            lblPrecioVenta.Text = "Precio venta";
            // 
            // lblPrecioCompra
            // 
            lblPrecioCompra.AutoSize = true;
            lblPrecioCompra.Location = new Point(1096, 41);
            lblPrecioCompra.Name = "lblPrecioCompra";
            lblPrecioCompra.Size = new Size(126, 25);
            lblPrecioCompra.TabIndex = 14;
            lblPrecioCompra.Text = "Precio compra";
            // 
            // lblStockMinimo
            // 
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Location = new Point(766, 41);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(121, 25);
            lblStockMinimo.TabIndex = 9;
            lblStockMinimo.Text = "Stock mínimo";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(645, 41);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(55, 25);
            lblStock.TabIndex = 10;
            lblStock.Text = "Stock";
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Location = new Point(316, 35);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(55, 25);
            lblColor.TabIndex = 11;
            lblColor.Text = "Color";
            // 
            // lblTalla
            // 
            lblTalla.AutoSize = true;
            lblTalla.Location = new Point(24, 35);
            lblTalla.Name = "lblTalla";
            lblTalla.Size = new Size(45, 25);
            lblTalla.TabIndex = 12;
            lblTalla.Text = "Talla";
            // 
            // lblVariante
            // 
            lblVariante.AutoSize = true;
            lblVariante.Location = new Point(20, 8);
            lblVariante.Name = "lblVariante";
            lblVariante.Size = new Size(231, 25);
            lblVariante.TabIndex = 13;
            lblVariante.Text = "Variante inicial del producto";
            // 
            // cmbMarca
            // 
            cmbMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarca.ItemHeight = 25;
            cmbMarca.Location = new Point(1120, 110);
            cmbMarca.Name = "cmbMarca";
            cmbMarca.Size = new Size(339, 33);
            cmbMarca.TabIndex = 3;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Location = new Point(1120, 85);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(60, 25);
            lblMarca.TabIndex = 4;
            lblMarca.Text = "Marca";
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.ItemHeight = 25;
            cmbCategoria.Location = new Point(780, 110);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(318, 33);
            cmbCategoria.TabIndex = 5;
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Location = new Point(780, 85);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(88, 25);
            lblCategoria.TabIndex = 6;
            lblCategoria.Text = "Categoría";
            // 
            // txtDescripcion
            // 
            txtDescripcion.BackColor = Color.FromArgb(232, 221, 202);
            txtDescripcion.BorderStyle = BorderStyle.FixedSingle;
            txtDescripcion.ForeColor = Color.FromArgb(41, 41, 41);
            txtDescripcion.Location = new Point(391, 110);
            txtDescripcion.Margin = new Padding(4, 5, 4, 5);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "Descripción opcional";
            txtDescripcion.Size = new Size(367, 31);
            txtDescripcion.TabIndex = 7;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(391, 85);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(104, 25);
            lblDescripcion.TabIndex = 8;
            lblDescripcion.Text = "Descripción";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(232, 221, 202);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.ForeColor = Color.FromArgb(41, 41, 41);
            txtNombre.Location = new Point(31, 110);
            txtNombre.Margin = new Padding(4, 5, 4, 5);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Ej. Camisa deportiva Nike";
            txtNombre.Size = new Size(338, 31);
            txtNombre.TabIndex = 9;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(31, 85);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(186, 25);
            lblNombre.TabIndex = 10;
            lblNombre.Text = "Nombre del producto";
            // 
            // lblRegistrarAyuda
            // 
            lblRegistrarAyuda.AutoSize = true;
            lblRegistrarAyuda.Location = new Point(31, 49);
            lblRegistrarAyuda.Name = "lblRegistrarAyuda";
            lblRegistrarAyuda.Size = new Size(664, 25);
            lblRegistrarAyuda.TabIndex = 11;
            lblRegistrarAyuda.Text = "Completa los datos del producto y define su primera combinación de talla y color.";
            // 
            // lblRegistrar
            // 
            lblRegistrar.AutoSize = true;
            lblRegistrar.Location = new Point(26, 17);
            lblRegistrar.Name = "lblRegistrar";
            lblRegistrar.Size = new Size(160, 25);
            lblRegistrar.TabIndex = 12;
            lblRegistrar.Text = "Registrar producto";
            // 
            // btnAgregarVariantesExistente
            // 
            btnAgregarVariantesExistente.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAgregarVariantesExistente.Cursor = Cursors.Hand;
            btnAgregarVariantesExistente.Enabled = false;
            btnAgregarVariantesExistente.FlatAppearance.BorderSize = 1;
            btnAgregarVariantesExistente.FlatStyle = FlatStyle.Flat;
            btnAgregarVariantesExistente.ForeColor = Color.FromArgb(41, 41, 41);
            btnAgregarVariantesExistente.Location = new Point(1221, 19);
            btnAgregarVariantesExistente.Name = "btnAgregarVariantesExistente";
            btnAgregarVariantesExistente.Size = new Size(210, 42);
            btnAgregarVariantesExistente.TabIndex = 8;
            btnAgregarVariantesExistente.Text = "+ Variantes al producto";
            btnAgregarVariantesExistente.UseVisualStyleBackColor = false;
            btnAgregarVariantesExistente.Click += btnAgregarVariantesExistente_Click;
            // 
            // pnlResumen
            // 
            pnlResumen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlResumen.BackColor = Color.FromArgb(232, 221, 202);
            pnlResumen.Controls.Add(txtBuscarResumen);
            pnlResumen.Controls.Add(cmbOrdenResumen);
            pnlResumen.Controls.Add(lblOrdenResumen);
            pnlResumen.Controls.Add(lblResumenTotal);
            pnlResumen.Controls.Add(lblResumen);
            pnlResumen.Controls.Add(dgvResumen);
            pnlResumen.Controls.Add(btnAgregarVariantesExistente);
            pnlResumen.Location = new Point(28, 484);
            pnlResumen.Name = "pnlResumen";
            pnlResumen.Size = new Size(1500, 357);
            pnlResumen.TabIndex = 0;
            // 
            // txtBuscarResumen
            // 
            txtBuscarResumen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscarResumen.BackColor = Color.FromArgb(232, 221, 202);
            txtBuscarResumen.BorderStyle = BorderStyle.FixedSingle;
            txtBuscarResumen.ForeColor = Color.FromArgb(41, 41, 41);
            txtBuscarResumen.Location = new Point(782, 26);
            txtBuscarResumen.Margin = new Padding(4, 5, 4, 5);
            txtBuscarResumen.Name = "txtBuscarResumen";
            txtBuscarResumen.PlaceholderText = "Buscar producto...";
            txtBuscarResumen.Size = new Size(417, 31);
            txtBuscarResumen.TabIndex = 1;
            txtBuscarResumen.TextChanged += txtBuscarResumen_TextChanged;
            // 
            // cmbOrdenResumen
            // 
            cmbOrdenResumen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbOrdenResumen.BackColor = Color.FromArgb(232, 221, 202);
            cmbOrdenResumen.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrdenResumen.FlatStyle = FlatStyle.Flat;
            cmbOrdenResumen.ForeColor = Color.FromArgb(41, 41, 41);
            cmbOrdenResumen.ItemHeight = 25;
            cmbOrdenResumen.Location = new Point(615, 25);
            cmbOrdenResumen.Name = "cmbOrdenResumen";
            cmbOrdenResumen.Size = new Size(160, 33);
            cmbOrdenResumen.TabIndex = 2;
            cmbOrdenResumen.SelectedIndexChanged += cmbOrdenResumen_SelectedIndexChanged;
            // 
            // lblOrdenResumen
            // 
            lblOrdenResumen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblOrdenResumen.AutoSize = true;
            lblOrdenResumen.Location = new Point(615, 3);
            lblOrdenResumen.Name = "lblOrdenResumen";
            lblOrdenResumen.Size = new Size(62, 25);
            lblOrdenResumen.TabIndex = 3;
            lblOrdenResumen.Text = "Orden";
            // 
            // lblResumenTotal
            // 
            lblResumenTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblResumenTotal.AutoSize = true;
            lblResumenTotal.Location = new Point(31, 329);
            lblResumenTotal.Name = "lblResumenTotal";
            lblResumenTotal.Size = new Size(359, 25);
            lblResumenTotal.TabIndex = 4;
            lblResumenTotal.Text = "Mostrando 0 producto(s) agrupados por ID";
            // 
            // lblResumen
            // 
            lblResumen.AutoSize = true;
            lblResumen.Location = new Point(26, 16);
            lblResumen.Name = "lblResumen";
            lblResumen.Size = new Size(225, 25);
            lblResumen.TabIndex = 6;
            lblResumen.Text = "Vista general de productos";
            // 
            // dgvResumen
            // 
            dgvResumen.AllowUserToAddRows = false;
            dgvResumen.AllowUserToDeleteRows = false;
            dgvResumen.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(232, 221, 202);
            dgvResumen.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvResumen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResumen.BackgroundColor = Color.FromArgb(232, 221, 202);
            dgvResumen.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(41, 41, 41);
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle5.SelectionForeColor = Color.FromArgb(250, 249, 246);
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvResumen.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvResumen.ColumnHeadersHeight = 40;
            dgvResumen.Columns.AddRange(new DataGridViewColumn[] { colIdProducto, colProducto, colMarcaResumen, colCategoriaResumen, colVariantesResumen, colStockResumen, colPrecioResumen, colEstadoResumen });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(41, 41, 41);
            dataGridViewCellStyle6.Padding = new Padding(7, 3, 7, 3);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle6.SelectionForeColor = Color.FromArgb(250, 249, 246);
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvResumen.DefaultCellStyle = dataGridViewCellStyle6;
            dgvResumen.EnableHeadersVisualStyles = false;
            dgvResumen.GridColor = Color.FromArgb(229, 227, 223);
            dgvResumen.Location = new Point(31, 82);
            dgvResumen.MultiSelect = false;
            dgvResumen.Name = "dgvResumen";
            dgvResumen.ReadOnly = true;
            dgvResumen.RowHeadersVisible = false;
            dgvResumen.RowHeadersWidth = 62;
            dgvResumen.RowTemplate.Height = 36;
            dgvResumen.Size = new Size(1429, 240);
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
            colIdProducto.Width = 150;
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
            colVariantesResumen.Width = 150;
            // 
            // colStockResumen
            // 
            colStockResumen.HeaderText = "Stock total";
            colStockResumen.MinimumWidth = 8;
            colStockResumen.Name = "colStockResumen";
            colStockResumen.ReadOnly = true;
            colStockResumen.Width = 150;
            // 
            // colPrecioResumen
            // 
            colPrecioResumen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colPrecioResumen.HeaderText = "Precio venta";
            colPrecioResumen.MinimumWidth = 8;
            colPrecioResumen.Name = "colPrecioResumen";
            colPrecioResumen.ReadOnly = true;
            // 
            // colEstadoResumen
            // 
            colEstadoResumen.HeaderText = "Estado";
            colEstadoResumen.MinimumWidth = 8;
            colEstadoResumen.Name = "colEstadoResumen";
            colEstadoResumen.ReadOnly = true;
            colEstadoResumen.Width = 150;
            // 
            // Frm_producto
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(1556, 867);
            Controls.Add(pnlResumen);
            Controls.Add(pnlRegistro);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(41, 41, 41);
            MinimumSize = new Size(1100, 700);
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
            nudPrecioCompra.BackColor = Color.FromArgb(232, 221, 202);
            nudPrecioCompra.ForeColor = Color.FromArgb(41, 41, 41);
            nudPrecioVenta.BackColor = Color.FromArgb(232, 221, 202);
            nudPrecioVenta.ForeColor = Color.FromArgb(41, 41, 41);
            nudStockMinimo.BackColor = Color.FromArgb(232, 221, 202);
            nudStockMinimo.ForeColor = Color.FromArgb(41, 41, 41);
            nudStock.BackColor = Color.FromArgb(232, 221, 202);
            nudStock.ForeColor = Color.FromArgb(41, 41, 41);
            cmbColor.BackColor = Color.FromArgb(232, 221, 202);
            cmbColor.ForeColor = Color.FromArgb(41, 41, 41);
            cmbTalla.BackColor = Color.FromArgb(232, 221, 202);
            cmbTalla.ForeColor = Color.FromArgb(41, 41, 41);
            cmbMarca.BackColor = Color.FromArgb(232, 221, 202);
            cmbMarca.ForeColor = Color.FromArgb(41, 41, 41);
            cmbCategoria.BackColor = Color.FromArgb(232, 221, 202);
            cmbCategoria.ForeColor = Color.FromArgb(41, 41, 41);
            txtDescripcion.BackColor = Color.FromArgb(232, 221, 202);
            txtDescripcion.ForeColor = Color.FromArgb(41, 41, 41);
            txtNombre.BackColor = Color.FromArgb(232, 221, 202);
            txtNombre.ForeColor = Color.FromArgb(41, 41, 41);
            txtBuscarResumen.BackColor = Color.FromArgb(232, 221, 202);
            txtBuscarResumen.ForeColor = Color.FromArgb(41, 41, 41);
            cmbOrdenResumen.BackColor = Color.FromArgb(232, 221, 202);
            cmbOrdenResumen.ForeColor = Color.FromArgb(41, 41, 41);
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
        private Button btnAgregarVariantesExistente;
        private NumericUpDown nudPrecioCompra;
        private NumericUpDown nudPrecioVenta;
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
        private DataGridViewTextBoxColumn colEstadoResumen;
    }
}


