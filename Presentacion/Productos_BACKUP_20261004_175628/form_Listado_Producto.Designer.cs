namespace Nk_Colletion_New
{
    partial class form_Listado_Producto
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblTitle = new Label();
            lblSubtitle = new Label();
            btn_volver = new Button();
            txtBuscar = new TextBox();
            labelBuscar = new Label();
            cmbCategoria = new ComboBox();
            labelCategoria = new Label();
            cmbMarca = new ComboBox();
            labelMarca = new Label();
            cmbTalla = new ComboBox();
            labelTalla = new Label();
            cmbColor = new ComboBox();
            labelColor = new Label();
            cmbEstado = new ComboBox();
            labelEstado = new Label();
            btnLimpiar = new Button();
            lblCount = new Label();
            dgvListado = new DataGridView();
            comboBox1 = new ComboBox();
            label1 = new Label();
            guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)dgvListado).BeginInit();
            guna2GroupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(608, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Listado completo de productos";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.Location = new Point(26, 63);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(556, 25);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Inventario desglosado: una fila representa una variante del producto.";
            // 
            // btn_volver
            // 
            btn_volver.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_volver.Location = new Point(1023, 26);
            btn_volver.Name = "btn_volver";
            btn_volver.Size = new Size(188, 36);
            btn_volver.TabIndex = 2;
            btn_volver.Text = "Volver a productos";
            btn_volver.UseVisualStyleBackColor = true;
            btn_volver.Click += btn_volver_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Location = new Point(12, 101);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Producto, código, marca, catego";
            txtBuscar.Size = new Size(405, 31);
            txtBuscar.TabIndex = 3;
            // 
            // labelBuscar
            // 
            labelBuscar.AutoSize = true;
            labelBuscar.Font = new Font("Segoe UI", 9F);
            labelBuscar.Location = new Point(12, 73);
            labelBuscar.Name = "labelBuscar";
            labelBuscar.Size = new Size(63, 25);
            labelBuscar.TabIndex = 3;
            labelBuscar.Text = "Buscar";
            // 
            // cmbCategoria
            // 
            cmbCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbCategoria.Location = new Point(438, 99);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(120, 33);
            cmbCategoria.TabIndex = 4;
            // 
            // labelCategoria
            // 
            labelCategoria.AutoSize = true;
            labelCategoria.Font = new Font("Segoe UI", 9F);
            labelCategoria.Location = new Point(438, 71);
            labelCategoria.Name = "labelCategoria";
            labelCategoria.Size = new Size(88, 25);
            labelCategoria.TabIndex = 4;
            labelCategoria.Text = "Categoría";
            // 
            // cmbMarca
            // 
            cmbMarca.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbMarca.Location = new Point(573, 99);
            cmbMarca.Name = "cmbMarca";
            cmbMarca.Size = new Size(120, 33);
            cmbMarca.TabIndex = 5;
            // 
            // labelMarca
            // 
            labelMarca.AutoSize = true;
            labelMarca.Font = new Font("Segoe UI", 9F);
            labelMarca.Location = new Point(573, 71);
            labelMarca.Name = "labelMarca";
            labelMarca.Size = new Size(60, 25);
            labelMarca.TabIndex = 5;
            labelMarca.Text = "Marca";
            // 
            // cmbTalla
            // 
            cmbTalla.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbTalla.Location = new Point(709, 99);
            cmbTalla.Name = "cmbTalla";
            cmbTalla.Size = new Size(90, 33);
            cmbTalla.TabIndex = 6;
            // 
            // labelTalla
            // 
            labelTalla.AutoSize = true;
            labelTalla.Font = new Font("Segoe UI", 9F);
            labelTalla.Location = new Point(709, 71);
            labelTalla.Name = "labelTalla";
            labelTalla.Size = new Size(45, 25);
            labelTalla.TabIndex = 6;
            labelTalla.Text = "Talla";
            // 
            // cmbColor
            // 
            cmbColor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbColor.Location = new Point(817, 99);
            cmbColor.Name = "cmbColor";
            cmbColor.Size = new Size(90, 33);
            cmbColor.TabIndex = 7;
            // 
            // labelColor
            // 
            labelColor.AutoSize = true;
            labelColor.Font = new Font("Segoe UI", 9F);
            labelColor.Location = new Point(817, 71);
            labelColor.Name = "labelColor";
            labelColor.Size = new Size(55, 25);
            labelColor.TabIndex = 7;
            labelColor.Text = "Color";
            // 
            // cmbEstado
            // 
            cmbEstado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbEstado.Location = new Point(925, 99);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(90, 33);
            cmbEstado.TabIndex = 8;
            // 
            // labelEstado
            // 
            labelEstado.AutoSize = true;
            labelEstado.Font = new Font("Segoe UI", 9F);
            labelEstado.Location = new Point(925, 71);
            labelEstado.Name = "labelEstado";
            labelEstado.Size = new Size(66, 25);
            labelEstado.TabIndex = 8;
            labelEstado.Text = "Estado";
            // 
            // btnLimpiar
            // 
            btnLimpiar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimpiar.Location = new Point(1099, 43);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(100, 36);
            btnLimpiar.TabIndex = 9;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Location = new Point(26, 260);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(199, 25);
            lblCount.TabIndex = 10;
            lblCount.Text = "0 variantes encontradas";
            // 
            // dgvListado
            // 
            dgvListado.AllowUserToAddRows = false;
            dgvListado.AllowUserToDeleteRows = false;
            dgvListado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListado.Location = new Point(26, 288);
            dgvListado.Name = "dgvListado";
            dgvListado.ReadOnly = true;
            dgvListado.RowHeadersWidth = 62;
            dgvListado.Size = new Size(1185, 412);
            dgvListado.TabIndex = 11;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBox1.Location = new Point(1031, 99);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(90, 33);
            comboBox1.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F);
            label1.Location = new Point(1031, 71);
            label1.Name = "label1";
            label1.Size = new Size(62, 25);
            label1.TabIndex = 8;
            label1.Text = "Orden";
            // 
            // guna2GroupBox1
            // 
            guna2GroupBox1.Controls.Add(guna2HtmlLabel1);
            guna2GroupBox1.Controls.Add(txtBuscar);
            guna2GroupBox1.Controls.Add(labelBuscar);
            guna2GroupBox1.Controls.Add(cmbCategoria);
            guna2GroupBox1.Controls.Add(btnLimpiar);
            guna2GroupBox1.Controls.Add(label1);
            guna2GroupBox1.Controls.Add(labelColor);
            guna2GroupBox1.Controls.Add(comboBox1);
            guna2GroupBox1.Controls.Add(labelEstado);
            guna2GroupBox1.Controls.Add(labelTalla);
            guna2GroupBox1.Controls.Add(cmbColor);
            guna2GroupBox1.Controls.Add(cmbEstado);
            guna2GroupBox1.Controls.Add(labelMarca);
            guna2GroupBox1.Controls.Add(cmbTalla);
            guna2GroupBox1.Controls.Add(labelCategoria);
            guna2GroupBox1.Controls.Add(cmbMarca);
            guna2GroupBox1.CustomizableEdges = customizableEdges1;
            guna2GroupBox1.Font = new Font("Segoe UI", 9F);
            guna2GroupBox1.ForeColor = Color.FromArgb(125, 137, 149);
            guna2GroupBox1.Location = new Point(14, 91);
            guna2GroupBox1.Name = "guna2GroupBox1";
            guna2GroupBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2GroupBox1.Size = new Size(1205, 154);
            guna2GroupBox1.TabIndex = 12;
            guna2GroupBox1.Text = "Inventario detallado";
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(12, 43);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(895, 27);
            guna2HtmlLabel1.TabIndex = 10;
            guna2HtmlLabel1.Text = "Los filtros se aplican automaticamente al  escribir o seleccionar una opcion. Cada fila muestra una variante distinta";
            // 
            // form_Listado_Producto
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1231, 720);
            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(btn_volver);
            Controls.Add(lblCount);
            Controls.Add(dgvListado);
            Controls.Add(guna2GroupBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "form_Listado_Producto";
            Text = "Listado de productos";
            ((System.ComponentModel.ISupportInitialize)dgvListado).EndInit();
            guna2GroupBox1.ResumeLayout(false);
            guna2GroupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Button btn_volver;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label labelBuscar;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label labelCategoria;
        private System.Windows.Forms.ComboBox cmbMarca;
        private System.Windows.Forms.Label labelMarca;
        private System.Windows.Forms.ComboBox cmbTalla;
        private System.Windows.Forms.Label labelTalla;
        private System.Windows.Forms.ComboBox cmbColor;
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label labelEstado;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.DataGridView dgvListado;
        private ComboBox comboBox1;
        private Label label1;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}