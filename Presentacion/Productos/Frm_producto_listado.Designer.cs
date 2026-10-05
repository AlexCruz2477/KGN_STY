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
            cmbEstado = new ComboBox();
            cmbColor = new ComboBox();
            cmbTalla = new ComboBox();
            cmbMarca = new ComboBox();
            cmbCategoria = new ComboBox();
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
            pnlEncabezado.Controls.Add(btnVolver);
            pnlEncabezado.Controls.Add(lblSubtitulo);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Dock = DockStyle.Top;
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(1556, 112);
            // 
            // btnVolver
            // 
            btnVolver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnVolver.Location = new Point(1328, 34);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(190, 45);
            btnVolver.Text = "Volver a productos";
            btnVolver.Click += btnVolver_Click;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(38, 69);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(571, 23);
            lblSubtitulo.Text = "Inventario desglosado: una fila representa una variante del producto.";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(33, 20);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(512, 47);
            lblTitulo.Text = "Listado completo de productos";
            // 
            // pnlContenido
            // 
            pnlContenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlContenido.Controls.Add(btnLimpiar);
            pnlContenido.Controls.Add(lblOrden);
            pnlContenido.Controls.Add(lblEstado);
            pnlContenido.Controls.Add(lblColor);
            pnlContenido.Controls.Add(lblTalla);
            pnlContenido.Controls.Add(lblMarca);
            pnlContenido.Controls.Add(lblCategoria);
            pnlContenido.Controls.Add(lblBuscar);
            pnlContenido.Controls.Add(cmbOrden);
            pnlContenido.Controls.Add(cmbEstado);
            pnlContenido.Controls.Add(cmbColor);
            pnlContenido.Controls.Add(cmbTalla);
            pnlContenido.Controls.Add(cmbMarca);
            pnlContenido.Controls.Add(cmbCategoria);
            pnlContenido.Controls.Add(txtBuscar);
            pnlContenido.Controls.Add(lblResultados);
            pnlContenido.Controls.Add(lblInventarioAyuda);
            pnlContenido.Controls.Add(lblInventario);
            pnlContenido.Controls.Add(dgvProductos);
            pnlContenido.Location = new Point(28, 120);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(1500, 720);
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Location = new Point(1336, 111);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(124, 40);
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // etiquetas filtros
            // 
            lblBuscar.AutoSize = true; lblBuscar.Font = new Font("Book Antiqua", 8F, FontStyle.Bold); lblBuscar.ForeColor = Color.FromArgb(63, 65, 64); lblBuscar.Location = new Point(31, 86); lblBuscar.Name = "lblBuscar"; lblBuscar.Text = "Buscar";
            lblCategoria.AutoSize = true; lblCategoria.Font = new Font("Book Antiqua", 8F, FontStyle.Bold); lblCategoria.ForeColor = Color.FromArgb(63, 65, 64); lblCategoria.Location = new Point(327, 86); lblCategoria.Name = "lblCategoria"; lblCategoria.Text = "Categoría";
            lblMarca.AutoSize = true; lblMarca.Font = new Font("Book Antiqua", 8F, FontStyle.Bold); lblMarca.ForeColor = Color.FromArgb(63, 65, 64); lblMarca.Location = new Point(497, 86); lblMarca.Name = "lblMarca"; lblMarca.Text = "Marca";
            lblTalla.AutoSize = true; lblTalla.Font = new Font("Book Antiqua", 8F, FontStyle.Bold); lblTalla.ForeColor = Color.FromArgb(63, 65, 64); lblTalla.Location = new Point(667, 86); lblTalla.Name = "lblTalla"; lblTalla.Text = "Talla";
            lblColor.AutoSize = true; lblColor.Font = new Font("Book Antiqua", 8F, FontStyle.Bold); lblColor.ForeColor = Color.FromArgb(63, 65, 64); lblColor.Location = new Point(812, 86); lblColor.Name = "lblColor"; lblColor.Text = "Color";
            lblEstado.AutoSize = true; lblEstado.Font = new Font("Book Antiqua", 8F, FontStyle.Bold); lblEstado.ForeColor = Color.FromArgb(63, 65, 64); lblEstado.Location = new Point(957, 86); lblEstado.Name = "lblEstado"; lblEstado.Text = "Estado";
            lblOrden.AutoSize = true; lblOrden.Font = new Font("Book Antiqua", 8F, FontStyle.Bold); lblOrden.ForeColor = Color.FromArgb(63, 65, 64); lblOrden.Location = new Point(1092, 86); lblOrden.Name = "lblOrden"; lblOrden.Text = "Orden";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(31, 111);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Producto, código, marca, categoría, talla o color";
            txtBuscar.Size = new Size(280, 40);
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.ItemHeight = 28;
            cmbCategoria.Location = new Point(327, 111);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(155, 34);
            cmbCategoria.SelectedIndexChanged += filtro_SelectedIndexChanged;
            // 
            // cmbMarca
            // 
            cmbMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMarca.ItemHeight = 28;
            cmbMarca.Location = new Point(497, 111);
            cmbMarca.Name = "cmbMarca";
            cmbMarca.Size = new Size(155, 34);
            cmbMarca.SelectedIndexChanged += filtro_SelectedIndexChanged;
            // 
            // cmbTalla
            // 
            cmbTalla.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTalla.ItemHeight = 28;
            cmbTalla.Location = new Point(667, 111);
            cmbTalla.Name = "cmbTalla";
            cmbTalla.Size = new Size(130, 34);
            cmbTalla.SelectedIndexChanged += filtro_SelectedIndexChanged;
            // 
            // cmbColor
            // 
            cmbColor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbColor.ItemHeight = 28;
            cmbColor.Location = new Point(812, 111);
            cmbColor.Name = "cmbColor";
            cmbColor.Size = new Size(130, 34);
            cmbColor.SelectedIndexChanged += filtro_SelectedIndexChanged;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.ItemHeight = 28;
            cmbEstado.Location = new Point(957, 111);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(120, 34);
            cmbEstado.SelectedIndexChanged += filtro_SelectedIndexChanged;
            // 
            // cmbOrden
            // 
            cmbOrden.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbOrden.ItemHeight = 28;
            cmbOrden.Location = new Point(1092, 111);
            cmbOrden.Name = "cmbOrden";
            cmbOrden.Size = new Size(130, 34);
            cmbOrden.SelectedIndexChanged += cmbOrden_SelectedIndexChanged;
            // 
            // lblResultados
            // 
            lblResultados.AutoSize = true;
            lblResultados.Location = new Point(31, 166);
            lblResultados.Name = "lblResultados";
            lblResultados.Size = new Size(177, 20);
            lblResultados.Text = "0 variantes encontradas";
            // 
            // lblInventarioAyuda
            // 
            lblInventarioAyuda.AutoSize = true;
            lblInventarioAyuda.Location = new Point(31, 53);
            lblInventarioAyuda.Name = "lblInventarioAyuda";
            lblInventarioAyuda.Size = new Size(947, 21);
            lblInventarioAyuda.Text = "Los filtros se aplican automáticamente al escribir o seleccionar una opción. Cada fila muestra una variante distinta.";
            // 
            // lblInventario
            // 
            lblInventario.AutoSize = true;
            lblInventario.Location = new Point(28, 21);
            lblInventario.Name = "lblInventario";
            lblInventario.Size = new Size(250, 30);
            lblInventario.Text = "Inventario detallado";
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.AllowUserToResizeRows = false;
            dgvProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.ColumnHeadersHeight = 44;
            dgvProductos.Columns.AddRange(new DataGridViewColumn[] { colIdProducto, colIdVariante, colCodigo, colProducto, colMarca, colCategoria, colTalla, colColor, colStock, colStockMinimo, colPrecioCompra, colPrecioVenta, colEstado });
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.GridColor = Color.FromArgb(238, 228, 230);
            dgvProductos.Location = new Point(31, 197);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.RowTemplate.Height = 38;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(1429, 493);
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            // columnas
            colIdProducto.HeaderText = "ID producto"; colIdProducto.Name = "colIdProducto"; colIdProducto.ReadOnly = true; colIdProducto.Width = 85;
            colIdVariante.HeaderText = "ID variante"; colIdVariante.Name = "colIdVariante"; colIdVariante.ReadOnly = true; colIdVariante.Width = 85;
            colCodigo.HeaderText = "Código"; colCodigo.Name = "colCodigo"; colCodigo.ReadOnly = true; colCodigo.Width = 170;
            colProducto.HeaderText = "Producto"; colProducto.Name = "colProducto"; colProducto.ReadOnly = true; colProducto.Width = 190;
            colMarca.HeaderText = "Marca"; colMarca.Name = "colMarca"; colMarca.ReadOnly = true; colMarca.Width = 130;
            colCategoria.HeaderText = "Categoría"; colCategoria.Name = "colCategoria"; colCategoria.ReadOnly = true; colCategoria.Width = 150;
            colTalla.HeaderText = "Talla"; colTalla.Name = "colTalla"; colTalla.ReadOnly = true; colTalla.Width = 90;
            colColor.HeaderText = "Color"; colColor.Name = "colColor"; colColor.ReadOnly = true; colColor.Width = 110;
            colStock.HeaderText = "Stock"; colStock.Name = "colStock"; colStock.ReadOnly = true; colStock.Width = 80;
            colStockMinimo.HeaderText = "Stock mínimo"; colStockMinimo.Name = "colStockMinimo"; colStockMinimo.ReadOnly = true; colStockMinimo.Width = 100;
            colPrecioCompra.HeaderText = "Precio compra"; colPrecioCompra.Name = "colPrecioCompra"; colPrecioCompra.ReadOnly = true; colPrecioCompra.Width = 120;
            colPrecioVenta.HeaderText = "Precio venta"; colPrecioVenta.Name = "colPrecioVenta"; colPrecioVenta.ReadOnly = true; colPrecioVenta.Width = 120;
            colEstado.HeaderText = "Estado"; colEstado.Name = "colEstado"; colEstado.ReadOnly = true; colEstado.Width = 95;
            // 
            // Frm_producto_listado
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 241, 242);
            BackColor = Color.FromArgb(245, 241, 232);
            ClientSize = new Size(1556, 867);
            Controls.Add(pnlContenido);
            Controls.Add(pnlEncabezado);
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
        private ComboBox cmbEstado;
        private ComboBox cmbColor;
        private ComboBox cmbTalla;
        private ComboBox cmbMarca;
        private ComboBox cmbCategoria;
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