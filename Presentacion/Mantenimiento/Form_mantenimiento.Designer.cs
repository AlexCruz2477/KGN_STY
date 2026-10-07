namespace Nk_Colletion_New
{
    partial class Form_mantenimiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_mantenimiento));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            btn_Restaurar = new Button();
            btn_Crear = new Button();
            label7 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel1 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel2 = new Panel();
            guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            colFechaRespaldo = new DataGridViewTextBoxColumn();
            colTamanoSalida = new DataGridViewTextBoxColumn();
            colTamanoEntrada = new DataGridViewTextBoxColumn();
            colDatosExportados = new DataGridViewTextBoxColumn();
            colDatosImportados = new DataGridViewTextBoxColumn();
            colTipoRespaldo = new DataGridViewTextBoxColumn();
            colArchivoRespaldo = new DataGridViewTextBoxColumn();
            cmbTipoRespaldo = new ComboBox();
            lblTipoRespaldo = new Label();
            lblDescripcionMantenimiento = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1055, 173);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(234, 253);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 115;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(58, 173);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(234, 253);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 114;
            pictureBox1.TabStop = false;
            // 
            // btn_Restaurar
            // 
            btn_Restaurar.FlatStyle = FlatStyle.Flat;
            btn_Restaurar.Font = new Font("PMingLiU-ExtB", 10F, FontStyle.Bold | FontStyle.Italic);
            btn_Restaurar.ForeColor = SystemColors.ControlLightLight;
            btn_Restaurar.Location = new Point(1113, 443);
            btn_Restaurar.Name = "btn_Restaurar";
            btn_Restaurar.Size = new Size(111, 33);
            btn_Restaurar.TabIndex = 113;
            btn_Restaurar.Text = "Restaurar";
            btn_Restaurar.UseVisualStyleBackColor = false;
            btn_Restaurar.Click += btn_Restaurar_Click;
            // 
            // btn_Crear
            // 
            btn_Crear.FlatStyle = FlatStyle.Flat;
            btn_Crear.Font = new Font("PMingLiU-ExtB", 10F, FontStyle.Bold | FontStyle.Italic);
            btn_Crear.ForeColor = SystemColors.ControlLightLight;
            btn_Crear.Location = new Point(126, 443);
            btn_Crear.Name = "btn_Crear";
            btn_Crear.Size = new Size(111, 33);
            btn_Crear.TabIndex = 112;
            btn_Crear.Text = "Crear";
            btn_Crear.UseVisualStyleBackColor = false;
            btn_Crear.Click += btn_Crear_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("PMingLiU-ExtB", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(41, 41, 41);
            label7.Location = new Point(1055, 129);
            label7.Name = "label7";
            label7.Size = new Size(223, 24);
            label7.TabIndex = 111;
            label7.Text = "Restaurar base de datos";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("PMingLiU-ExtB", 12F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(41, 41, 41);
            label4.Location = new Point(58, 131);
            label4.Name = "label4";
            label4.Size = new Size(235, 24);
            label4.TabIndex = 110;
            label4.Text = "Crear copia de seguridad";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("PMingLiU-ExtB", 20F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(41, 41, 41);
            label5.Location = new Point(527, 35);
            label5.Name = "label5";
            label5.Size = new Size(259, 40);
            label5.TabIndex = 109;
            label5.Text = "Mantenimiento";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("PMingLiU-ExtB", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(41, 41, 41);
            label6.Location = new Point(39, 53);
            label6.Name = "label6";
            label6.Size = new Size(1278, 32);
            label6.TabIndex = 108;
            label6.Text = "_______________________________________________________________________________";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(232, 221, 202);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1371, 10);
            panel1.TabIndex = 153;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(232, 221, 202);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 10);
            panel3.Name = "panel3";
            panel3.Size = new Size(10, 853);
            panel3.TabIndex = 155;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(232, 221, 202);
            panel4.Dock = DockStyle.Bottom;
            panel4.Location = new Point(10, 853);
            panel4.Name = "panel4";
            panel4.Size = new Size(1361, 10);
            panel4.TabIndex = 157;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(232, 221, 202);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(1361, 10);
            panel2.Name = "panel2";
            panel2.Size = new Size(10, 843);
            panel2.TabIndex = 158;
            panel2.Paint += panel2_Paint;
            // 
            // guna2DataGridView1
            // 
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.AllowUserToDeleteRows = false;
            guna2DataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(232, 221, 202);
            guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            guna2DataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            guna2DataGridView1.BackgroundColor = Color.FromArgb(24, 24, 24);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(41, 41, 41);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            guna2DataGridView1.ColumnHeadersHeight = 42;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2DataGridView1.Columns.AddRange(new DataGridViewColumn[] { colFechaRespaldo, colTamanoSalida, colTamanoEntrada, colDatosExportados, colDatosImportados, colTipoRespaldo, colArchivoRespaldo });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(41, 41, 41);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(250, 249, 246);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            guna2DataGridView1.GridColor = Color.FromArgb(229, 227, 223);
            guna2DataGridView1.Location = new Point(298, 195);
            guna2DataGridView1.MultiSelect = false;
            guna2DataGridView1.Name = "guna2DataGridView1";
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.RowHeadersWidth = 62;
            guna2DataGridView1.Size = new Size(751, 323);
            guna2DataGridView1.TabIndex = 159;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(232, 221, 202);
            guna2DataGridView1.ThemeStyle.BackColor = Color.FromArgb(232, 221, 202);
            guna2DataGridView1.ThemeStyle.GridColor = Color.FromArgb(229, 227, 223);
            guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(232, 221, 202);
            guna2DataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 42;
            guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(232, 221, 202);
            guna2DataGridView1.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(41, 41, 41);
            guna2DataGridView1.ThemeStyle.RowsStyle.Height = 33;
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(232, 221, 202);
            guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(250, 249, 246);
            // 
            // colFechaRespaldo
            // 
            colFechaRespaldo.HeaderText = "Fecha de respaldo";
            colFechaRespaldo.MinimumWidth = 8;
            colFechaRespaldo.Name = "colFechaRespaldo";
            colFechaRespaldo.ReadOnly = true;
            // 
            // colTamanoSalida
            // 
            colTamanoSalida.HeaderText = "Tamaño de salida";
            colTamanoSalida.MinimumWidth = 8;
            colTamanoSalida.Name = "colTamanoSalida";
            colTamanoSalida.ReadOnly = true;
            // 
            // colTamanoEntrada
            // 
            colTamanoEntrada.HeaderText = "Tamaño de entrada";
            colTamanoEntrada.MinimumWidth = 8;
            colTamanoEntrada.Name = "colTamanoEntrada";
            colTamanoEntrada.ReadOnly = true;
            // 
            // colDatosExportados
            // 
            colDatosExportados.HeaderText = "Datos exportados";
            colDatosExportados.MinimumWidth = 8;
            colDatosExportados.Name = "colDatosExportados";
            colDatosExportados.ReadOnly = true;
            // 
            // colDatosImportados
            // 
            colDatosImportados.HeaderText = "Datos importados";
            colDatosImportados.MinimumWidth = 8;
            colDatosImportados.Name = "colDatosImportados";
            colDatosImportados.ReadOnly = true;
            // 
            // colTipoRespaldo
            // 
            colTipoRespaldo.HeaderText = "Tipo de respaldo";
            colTipoRespaldo.MinimumWidth = 8;
            colTipoRespaldo.Name = "colTipoRespaldo";
            colTipoRespaldo.ReadOnly = true;
            // 
            // colArchivoRespaldo
            // 
            colArchivoRespaldo.HeaderText = "Archivo respaldo";
            colArchivoRespaldo.MinimumWidth = 8;
            colArchivoRespaldo.Name = "colArchivoRespaldo";
            colArchivoRespaldo.ReadOnly = true;
            colArchivoRespaldo.Visible = false;
            // 
            // cmbTipoRespaldo
            // 
            cmbTipoRespaldo.BackColor = Color.FromArgb(232, 221, 202);
            cmbTipoRespaldo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoRespaldo.FlatStyle = FlatStyle.Flat;
            cmbTipoRespaldo.Font = new Font("Segoe UI", 10F);
            cmbTipoRespaldo.ForeColor = Color.FromArgb(41, 41, 41);
            cmbTipoRespaldo.Items.AddRange(new object[] { "Base de datos completa", "Inferencial", "Incremental" });
            cmbTipoRespaldo.Location = new Point(363, 153);
            cmbTipoRespaldo.Name = "cmbTipoRespaldo";
            cmbTipoRespaldo.Size = new Size(220, 36);
            cmbTipoRespaldo.TabIndex = 161;
            // 
            // lblTipoRespaldo
            // 
            lblTipoRespaldo.AutoSize = true;
            lblTipoRespaldo.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblTipoRespaldo.ForeColor = Color.FromArgb(41, 41, 41);
            lblTipoRespaldo.Location = new Point(363, 122);
            lblTipoRespaldo.Name = "lblTipoRespaldo";
            lblTipoRespaldo.Size = new Size(165, 28);
            lblTipoRespaldo.TabIndex = 160;
            lblTipoRespaldo.Text = "Tipo de respaldo";
            // 
            // lblDescripcionMantenimiento
            // 
            lblDescripcionMantenimiento.AutoSize = true;
            lblDescripcionMantenimiento.Font = new Font("Segoe UI", 9F);
            lblDescripcionMantenimiento.ForeColor = Color.FromArgb(41, 41, 41);
            lblDescripcionMantenimiento.Location = new Point(243, 538);
            lblDescripcionMantenimiento.Name = "lblDescripcionMantenimiento";
            lblDescripcionMantenimiento.Size = new Size(925, 25);
            lblDescripcionMantenimiento.TabIndex = 162;
            lblDescripcionMantenimiento.Text = "El tamaño de entrada corresponde al respaldo restaurado; las cantidades indican objetos exportados e importados.";
            // 
            // Form_mantenimiento
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(1371, 863);
            Controls.Add(lblDescripcionMantenimiento);
            Controls.Add(cmbTipoRespaldo);
            Controls.Add(lblTipoRespaldo);
            Controls.Add(guna2DataGridView1);
            Controls.Add(panel2);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(btn_Restaurar);
            Controls.Add(btn_Crear);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label6);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form_mantenimiento";
            Text = "Form_mantenimiento";
            Load += Form_mantenimiento_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)guna2DataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Button btn_Restaurar;
        private Button btn_Crear;
        private Label label7;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel panel1;
        private Panel panel3;
        private Panel panel4;
        private Panel panel2;
        private Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView1;
        private DataGridViewTextBoxColumn colFechaRespaldo;
        private DataGridViewTextBoxColumn colTamanoSalida;
        private DataGridViewTextBoxColumn colTamanoEntrada;
        private DataGridViewTextBoxColumn colDatosExportados;
        private DataGridViewTextBoxColumn colDatosImportados;
        private DataGridViewTextBoxColumn colTipoRespaldo;
        private DataGridViewTextBoxColumn colArchivoRespaldo;
        private ComboBox cmbTipoRespaldo;
        private Label lblTipoRespaldo;
        private Label lblDescripcionMantenimiento;
    }
}


