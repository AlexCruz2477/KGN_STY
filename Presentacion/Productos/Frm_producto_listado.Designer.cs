namespace Nk_Colletion_New.Presentacion.Productos
{
    partial class Frm_producto_listado
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pnlEncabezado = new Panel();
            btnVolver = new Button();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            pnlContenido = new Panel();
            btnLimpiar = new Button();
            lblOrden = new Label();
            lblEstado = new Label();
            lblColor = new Label();
            lblTalla = new Label();
            lblMarca = new Label();
            lblCategoria = new Label();
            lblBuscar = new Label();
            cmbOrden = new ComboBox();
            cmbOrden.Items.AddRange(new object[] { "A - Z", "Z - A" });
            cmbOrden.SelectedIndex = 0;
            txtEstado = new TextBox();
            txtColor = new TextBox();
            txtTalla = new TextBox();
            txtMarca = new TextBox();
            txtCategoria = new TextBox();
            txtBuscar = new TextBox();
            lblResultados = new Label();
            lblInventarioAyuda = new Label();
            lblInventario = new Label();
            dgvProductos = new DataGridView();
            colIdProducto = new DataGridViewTextBoxColumn();
            colIdVariante = new DataGridViewTextBoxColumn();
            colCodigo = new DataGridViewTextBoxColumn();
            colProducto = new DataGridViewTextBoxColumn();
            colMarca = new DataGridViewTextBoxColumn();
            colCategoria = new DataGridViewTextBoxColumn();
            colTalla = new DataGridViewTextBoxColumn();
            colColor = new DataGridViewTextBoxColumn();
            colStock = new DataGridViewTextBoxColumn();
            colStockMinimo = new DataGridViewTextBoxColumn();
            colPrecioCompra = new DataGridViewTextBoxColumn();
            colPrecioVenta = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            pnlEncabezado.SuspendLayout();
            pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.BackColor = Color.FromArgb(232, 221, 202);
            pnlEncabezado.Controls.Add(btnVolver);
            pnlEncabezado.Controls.Add(lblSubtitulo);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(1556, 112);
            pnlEncabezado.TabIndex = 1;
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatAppearance.BorderSize = 1;
            btnVolver.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 221, 202);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnVolver.ForeColor = Color.FromArgb(41, 41, 41);
            btnVolver.Location = new Point(1328, 34);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(190, 45);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver a productos";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.ForeColor = Color.FromArgb(41, 41, 41);
            lblSubtitulo.Location = new Point(38, 69);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(592, 25);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Inventario desglosado: una fila representa una variante del producto.";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(41, 41, 41);
            lblTitulo.Location = new Point(33, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(272, 25);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Listado completo de productos";
            // 
            // pnlContenido
            // 
            pnlContenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenido.BackColor = Color.FromArgb(232, 221, 202);
            pnlContenido.Controls.Add(btnLimpiar);
            pnlContenido.Controls.Add(lblOrden);
            pnlContenido.Controls.Add(lblEstado);
            pnlContenido.Controls.Add(lblColor);
            pnlContenido.Controls.Add(lblTalla);
            pnlContenido.Controls.Add(lblMarca);
            pnlContenido.Controls.Add(lblCategoria);
            pnlContenido.Controls.Add(lblBuscar);
            pnlContenido.Controls.Add(cmbOrden);
            pnlContenido.Controls.Add(txtEstado);
            pnlContenido.Controls.Add(txtColor);
            pnlContenido.Controls.Add(txtTalla);
            pnlContenido.Controls.Add(txtMarca);
            pnlContenido.Controls.Add(txtCategoria);
            pnlContenido.Controls.Add(txtBuscar);
            pnlContenido.Controls.Add(lblResultados);
            pnlContenido.Controls.Add(lblInventarioAyuda);
            pnlContenido.Controls.Add(lblInventario);
            pnlContenido.Controls.Add(dgvProductos);
            pnlContenido.Location = new Point(28, 128);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(1500, 710);
            pnlContenido.TabIndex = 0;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Cursor = Cursors.Hand;
            btnLimpiar.FlatAppearance.BorderColor = Color.Black;
            btnLimpiar.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 221, 202);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.FromArgb(41, 41, 41);
            btnLimpiar.Location = new Point(1336, 111);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(124, 40);
            btnLimpiar.TabIndex = 0;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = false;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // lblOrden
            // 
            lblOrden.AutoSize = true;
            lblOrden.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblOrden.ForeColor = Color.FromArgb(41, 41, 41);
            lblOrden.ForeColor = Color.FromArgb(41, 41, 41);
            lblOrden.Location = new Point(1092, 86);
            lblOrden.Name = "lblOrden";
            lblOrden.Size = new Size(65, 25);
            lblOrden.TabIndex = 1;
            lblOrden.Text = "Orden";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblEstado.ForeColor = Color.FromArgb(41, 41, 41);
            lblEstado.ForeColor = Color.FromArgb(41, 41, 41);
            lblEstado.Location = new Point(957, 86);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(67, 25);
            lblEstado.TabIndex = 2;
            lblEstado.Text = "Estado";
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblColor.ForeColor = Color.FromArgb(41, 41, 41);
            lblColor.ForeColor = Color.FromArgb(41, 41, 41);
            lblColor.Location = new Point(812, 86);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(57, 25);
            lblColor.TabIndex = 3;
            lblColor.Text = "Color";
            // 
            // lblTalla
            // 
            lblTalla.AutoSize = true;
            lblTalla.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblTalla.ForeColor = Color.FromArgb(41, 41, 41);
            lblTalla.ForeColor = Color.FromArgb(41, 41, 41);
            lblTalla.Location = new Point(667, 86);
            lblTalla.Name = "lblTalla";
            lblTalla.Size = new Size(48, 25);
            lblTalla.TabIndex = 4;
            lblTalla.Text = "Talla";
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblMarca.ForeColor = Color.FromArgb(41, 41, 41);
            lblMarca.ForeColor = Color.FromArgb(41, 41, 41);
            lblMarca.Location = new Point(497, 86);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(62, 25);
            lblMarca.TabIndex = 5;
            lblMarca.Text = "Marca";
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblCategoria.ForeColor = Color.FromArgb(41, 41, 41);
            lblCategoria.ForeColor = Color.FromArgb(41, 41, 41);
            lblCategoria.Location = new Point(327, 86);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(92, 25);
            lblCategoria.TabIndex = 6;
            lblCategoria.Text = "Categoría";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblBuscar.ForeColor = Color.FromArgb(41, 41, 41);
            lblBuscar.ForeColor = Color.FromArgb(41, 41, 41);
            lblBuscar.Location = new Point(31, 86);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(66, 25);
            lblBuscar.TabIndex = 7;
            lblBuscar.Text = "Buscar";
            // 
            // cmbOrden
            // 
            cmbOrden.BackColor = Color.FromArgb(232, 221, 202);
            cmbOrden.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrden.FlatStyle = FlatStyle.Flat;
            cmbOrden.Font = new Font("Segoe UI", 10F);
            cmbOrden.ForeColor = Color.FromArgb(41, 41, 41);
            cmbOrden.ItemHeight = 28;
            cmbOrden.Location = new Point(1092, 111);
            cmbOrden.Name = "cmbOrden";
            cmbOrden.Size = new Size(130, 36);
            cmbOrden.TabIndex = 8;
            cmbOrden.SelectedIndexChanged += cmbOrden_SelectedIndexChanged;
            // 
            // txtEstado
            // 
            txtEstado.BackColor = Color.FromArgb(232, 221, 202);
            txtEstado.BorderStyle = BorderStyle.FixedSingle;
            txtEstado.Font = new Font("Segoe UI", 10F);
            txtEstado.ForeColor = Color.FromArgb(41, 41, 41);
            txtEstado.Location = new Point(957, 111);
            txtEstado.Name = "txtEstado";
            txtEstado.PlaceholderText = "Todos";
            txtEstado.Size = new Size(120, 34);
            txtEstado.TabIndex = 9;
            txtEstado.TextChanged += filtro_TextChanged;
            // 
            // txtColor
            // 
            txtColor.BackColor = Color.FromArgb(232, 221, 202);
            txtColor.BorderStyle = BorderStyle.FixedSingle;
            txtColor.Font = new Font("Segoe UI", 10F);
            txtColor.ForeColor = Color.FromArgb(41, 41, 41);
            txtColor.Location = new Point(812, 111);
            txtColor.Name = "txtColor";
            txtColor.PlaceholderText = "Todos";
            txtColor.Size = new Size(130, 34);
            txtColor.TabIndex = 10;
            txtColor.TextChanged += filtro_TextChanged;
            // 
            // txtTalla
            // 
            txtTalla.BackColor = Color.FromArgb(232, 221, 202);
            txtTalla.BorderStyle = BorderStyle.FixedSingle;
            txtTalla.Font = new Font("Segoe UI", 10F);
            txtTalla.ForeColor = Color.FromArgb(41, 41, 41);
            txtTalla.Location = new Point(667, 111);
            txtTalla.Name = "txtTalla";
            txtTalla.PlaceholderText = "Todas";
            txtTalla.Size = new Size(130, 34);
            txtTalla.TabIndex = 11;
            txtTalla.TextChanged += filtro_TextChanged;
            // 
            // txtMarca
            // 
            txtMarca.BackColor = Color.FromArgb(232, 221, 202);
            txtMarca.BorderStyle = BorderStyle.FixedSingle;
            txtMarca.Font = new Font("Segoe UI", 10F);
            txtMarca.ForeColor = Color.FromArgb(41, 41, 41);
            txtMarca.Location = new Point(497, 111);
            txtMarca.Name = "txtMarca";
            txtMarca.PlaceholderText = "Todas";
            txtMarca.Size = new Size(155, 34);
            txtMarca.TabIndex = 12;
            txtMarca.TextChanged += filtro_TextChanged;
            // 
            // txtCategoria
            // 
            txtCategoria.BackColor = Color.FromArgb(232, 221, 202);
            txtCategoria.BorderStyle = BorderStyle.FixedSingle;
            txtCategoria.Font = new Font("Segoe UI", 10F);
            txtCategoria.ForeColor = Color.FromArgb(41, 41, 41);
            txtCategoria.Location = new Point(327, 111);
            txtCategoria.Name = "txtCategoria";
            txtCategoria.PlaceholderText = "Todas";
            txtCategoria.Size = new Size(155, 34);
            txtCategoria.TabIndex = 13;
            txtCategoria.TextChanged += filtro_TextChanged;
            // 
            // txtBuscar
            // 
            txtBuscar.BackColor = Color.FromArgb(232, 221, 202);
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.ForeColor = Color.FromArgb(41, 41, 41);
            txtBuscar.Location = new Point(31, 111);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Producto, código, marca, categoría, talla o color";
            txtBuscar.Size = new Size(280, 34);
            txtBuscar.TabIndex = 14;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // lblResultados
            // 
            lblResultados.AutoSize = true;
            lblResultados.Location = new Point(31, 166);
            lblResultados.Name = "lblResultados";
            lblResultados.Size = new Size(212, 25);
            lblResultados.TabIndex = 15;
            lblResultados.Text = "0 variantes encontradas";
            // 
            // lblInventarioAyuda
            // 
            lblInventarioAyuda.AutoSize = true;
            lblInventarioAyuda.Location = new Point(31, 53);
            lblInventarioAyuda.Name = "lblInventarioAyuda";
            lblInventarioAyuda.Size = new Size(974, 25);
            lblInventarioAyuda.TabIndex = 16;
            lblInventarioAyuda.Text = "Los filtros se aplican automáticamente al escribir o seleccionar una opción. Cada fila muestra una variante distinta.";
            // 
            // lblInventario
            // 
            lblInventario.AutoSize = true;
            lblInventario.Location = new Point(28, 21);
            lblInventario.Name = "lblInventario";
            lblInventario.Size = new Size(181, 25);
            lblInventario.TabIndex = 17;
            lblInventario.Text = "Inventario detallado";
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(232, 221, 202);
            dgvProductos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProductos.BackgroundColor = Color.FromArgb(232, 221, 202);
            dgvProductos.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(41, 41, 41);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(250, 249, 246);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvProductos.ColumnHeadersHeight = 44;
            dgvProductos.Columns.AddRange(new DataGridViewColumn[] { colIdProducto, colIdVariante, colCodigo, colProducto, colMarca, colCategoria, colTalla, colColor, colStock, colStockMinimo, colPrecioCompra, colPrecioVenta, colEstado });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(41, 41, 41);
            dataGridViewCellStyle3.Padding = new Padding(6, 3, 6, 3);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(250, 249, 246);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvProductos.DefaultCellStyle = dataGridViewCellStyle3;
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.GridColor = Color.FromArgb(229, 227, 223);
            dgvProductos.Location = new Point(31, 197);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.RowHeadersWidth = 62;
            dgvProductos.RowTemplate.Height = 38;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(1497, 493);
            dgvProductos.TabIndex = 18;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
            // 
            // colIdProducto
            // 
            colIdProducto.HeaderText = "ID producto";
            colIdProducto.MinimumWidth = 8;
            colIdProducto.Name = "colIdProducto";
            colIdProducto.ReadOnly = true;
            colIdProducto.Width = 85;
            // 
            // colIdVariante
            // 
            colIdVariante.HeaderText = "ID variante";
            colIdVariante.MinimumWidth = 8;
            colIdVariante.Name = "colIdVariante";
            colIdVariante.ReadOnly = true;
            colIdVariante.Width = 85;
            // 
            // colCodigo
            // 
            colCodigo.HeaderText = "Código";
            colCodigo.MinimumWidth = 8;
            colCodigo.Name = "colCodigo";
            colCodigo.ReadOnly = true;
            colCodigo.Width = 170;
            // 
            // colProducto
            // 
            colProducto.HeaderText = "Producto";
            colProducto.MinimumWidth = 8;
            colProducto.Name = "colProducto";
            colProducto.ReadOnly = true;
            colProducto.Width = 190;
            // 
            // colMarca
            // 
            colMarca.HeaderText = "Marca";
            colMarca.MinimumWidth = 8;
            colMarca.Name = "colMarca";
            colMarca.ReadOnly = true;
            colMarca.Width = 130;
            // 
            // colCategoria
            // 
            colCategoria.HeaderText = "Categoría";
            colCategoria.MinimumWidth = 8;
            colCategoria.Name = "colCategoria";
            colCategoria.ReadOnly = true;
            colCategoria.Width = 150;
            // 
            // colTalla
            // 
            colTalla.HeaderText = "Talla";
            colTalla.MinimumWidth = 8;
            colTalla.Name = "colTalla";
            colTalla.ReadOnly = true;
            colTalla.Width = 90;
            // 
            // colColor
            // 
            colColor.HeaderText = "Color";
            colColor.MinimumWidth = 8;
            colColor.Name = "colColor";
            colColor.ReadOnly = true;
            colColor.Width = 110;
            // 
            // colStock
            // 
            colStock.HeaderText = "Stock";
            colStock.MinimumWidth = 8;
            colStock.Name = "colStock";
            colStock.ReadOnly = true;
            colStock.Width = 80;
            // 
            // colStockMinimo
            // 
            colStockMinimo.HeaderText = "Stock mínimo";
            colStockMinimo.MinimumWidth = 8;
            colStockMinimo.Name = "colStockMinimo";
            colStockMinimo.ReadOnly = true;
            // 
            // colPrecioCompra
            // 
            colPrecioCompra.HeaderText = "Precio compra";
            colPrecioCompra.MinimumWidth = 8;
            colPrecioCompra.Name = "colPrecioCompra";
            colPrecioCompra.ReadOnly = true;
            colPrecioCompra.Width = 120;
            // 
            // colPrecioVenta
            // 
            colPrecioVenta.HeaderText = "Precio venta";
            colPrecioVenta.MinimumWidth = 8;
            colPrecioVenta.Name = "colPrecioVenta";
            colPrecioVenta.ReadOnly = true;
            colPrecioVenta.Width = 120;
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 8;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            colEstado.Width = 95;
            // 
            // Frm_producto_listado
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(1556, 867);
            Controls.Add(pnlContenido);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.FromArgb(41, 41, 41);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Frm_producto_listado";
            Text = "Listado completo de productos";
            Load += Frm_producto_listado_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlContenido.ResumeLayout(false);
            pnlContenido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            }


        #endregion

        private Panel pnlEncabezado;
        private Button btnVolver;
        private Label lblSubtitulo;
        private Label lblTitulo;
        private Panel pnlContenido;
        private Button btnLimpiar;
        private Label lblOrden;
        private Label lblEstado;
        private Label lblColor;
        private Label lblTalla;
        private Label lblMarca;
        private Label lblCategoria;
        private Label lblBuscar;
        private ComboBox cmbOrden;
        private TextBox txtEstado;
        private TextBox txtColor;
        private TextBox txtTalla;
        private TextBox txtMarca;
        private TextBox txtCategoria;
        private TextBox txtBuscar;
        private Label lblResultados;
        private Label lblInventarioAyuda;
        private Label lblInventario;
        private DataGridView dgvProductos;
        private DataGridViewTextBoxColumn colIdProducto;
        private DataGridViewTextBoxColumn colIdVariante;
        private DataGridViewTextBoxColumn colCodigo;
        private DataGridViewTextBoxColumn colProducto;
        private DataGridViewTextBoxColumn colMarca;
        private DataGridViewTextBoxColumn colCategoria;
        private DataGridViewTextBoxColumn colTalla;
        private DataGridViewTextBoxColumn colColor;
        private DataGridViewTextBoxColumn colStock;
        private DataGridViewTextBoxColumn colStockMinimo;
        private DataGridViewTextBoxColumn colPrecioCompra;
        private DataGridViewTextBoxColumn colPrecioVenta;
        private DataGridViewTextBoxColumn colEstado;
    }
}


