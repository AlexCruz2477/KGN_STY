namespace Nk_Colletion_New.Presentacion.Productos
{
    partial class Frm_catalogo_rapido
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
            pnlListado = new Panel();
            lblTotal = new Label();
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            dgvCatalogo = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colDescripcion = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            pnlFormulario = new Panel();
            lblSeleccion = new Label();
            btnNuevo = new Button();
            btnBorrar = new Button();
            btnEditar = new Button();
            btnGuardar = new Button();
            txtDescripcion = new TextBox();
            lblDescripcion = new Label();
            txtNombre = new TextBox();
            lblNombre = new Label();
            lblFormulario = new Label();
            pnlEncabezado.SuspendLayout();
            pnlListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCatalogo).BeginInit();
            pnlFormulario.SuspendLayout();
            SuspendLayout();
            // 
            // pnlEncabezado
            // 
            pnlEncabezado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlEncabezado.BackColor = Color.FromArgb(232, 221, 202);
            pnlEncabezado.Controls.Add(btnVolver);
            pnlEncabezado.Controls.Add(lblSubtitulo);
            pnlEncabezado.Controls.Add(lblTitulo);
            pnlEncabezado.Location = new Point(0, 0);
            pnlEncabezado.Name = "pnlEncabezado";
            pnlEncabezado.Size = new Size(1556, 120);
            pnlEncabezado.TabIndex = 2;
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
            btnVolver.Location = new Point(1325, 39);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(195, 45);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver a productos";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(33, 59);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(415, 25);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Guarde, edite o desactive registros del catálogo.";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(33, 22);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(180, 25);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Gestión de catálogo";
            // 
            // pnlListado
            // 
            pnlListado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlListado.BackColor = Color.FromArgb(232, 221, 202);
            pnlListado.Controls.Add(lblTotal);
            pnlListado.Controls.Add(btnBuscar);
            pnlListado.Controls.Add(txtBuscar);
            pnlListado.Controls.Add(dgvCatalogo);
            pnlListado.Location = new Point(28, 128);
            pnlListado.Name = "pnlListado";
            pnlListado.Size = new Size(900, 706);
            pnlListado.TabIndex = 1;
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(31, 664);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(107, 25);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Registros: 0";
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Cursor = Cursors.Hand;
            btnBuscar.FlatAppearance.BorderSize = 1;
            btnBuscar.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 221, 202);
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.FromArgb(41, 41, 41);
            btnBuscar.Location = new Point(738, 25);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(145, 42);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.BackColor = Color.FromArgb(232, 221, 202);
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.ForeColor = Color.FromArgb(41, 41, 41);
            txtBuscar.Location = new Point(31, 25);
            txtBuscar.Margin = new Padding(4, 6, 4, 6);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nombre";
            txtBuscar.Size = new Size(680, 34);
            txtBuscar.TabIndex = 2;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // dgvCatalogo
            // 
            dgvCatalogo.AllowUserToAddRows = false;
            dgvCatalogo.AllowUserToDeleteRows = false;
            dgvCatalogo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(232, 221, 202);
            dgvCatalogo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvCatalogo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCatalogo.BackgroundColor = Color.FromArgb(232, 221, 202);
            dgvCatalogo.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(41, 41, 41);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(250, 249, 246);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvCatalogo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvCatalogo.ColumnHeadersHeight = 44;
            dgvCatalogo.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colDescripcion, colEstado });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(41, 41, 41);
            dataGridViewCellStyle3.Padding = new Padding(6, 3, 6, 3);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(232, 221, 202);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(250, 249, 246);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvCatalogo.DefaultCellStyle = dataGridViewCellStyle3;
            dgvCatalogo.EnableHeadersVisualStyles = false;
            dgvCatalogo.GridColor = Color.FromArgb(229, 227, 223);
            dgvCatalogo.Location = new Point(31, 84);
            dgvCatalogo.MultiSelect = false;
            dgvCatalogo.Name = "dgvCatalogo";
            dgvCatalogo.ReadOnly = true;
            dgvCatalogo.RowHeadersVisible = false;
            dgvCatalogo.RowHeadersWidth = 62;
            dgvCatalogo.RowTemplate.Height = 40;
            dgvCatalogo.Size = new Size(834, 560);
            dgvCatalogo.TabIndex = 3;
            dgvCatalogo.SelectionChanged += dgvCatalogo_SelectionChanged;
            // 
            // colId
            // 
            colId.HeaderText = "ID";
            colId.MinimumWidth = 8;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Width = 150;
            // 
            // colNombre
            // 
            colNombre.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colNombre.FillWeight = 130F;
            colNombre.HeaderText = "Nombre";
            colNombre.MinimumWidth = 8;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colDescripcion
            // 
            colDescripcion.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDescripcion.HeaderText = "Descripción";
            colDescripcion.MinimumWidth = 8;
            colDescripcion.Name = "colDescripcion";
            colDescripcion.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 8;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            colEstado.Width = 150;
            // 
            // pnlFormulario
            // 
            pnlFormulario.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlFormulario.BackColor = Color.FromArgb(232, 221, 202);
            pnlFormulario.Controls.Add(lblSeleccion);
            pnlFormulario.Controls.Add(btnNuevo);
            pnlFormulario.Controls.Add(btnBorrar);
            pnlFormulario.Controls.Add(btnEditar);
            pnlFormulario.Controls.Add(btnGuardar);
            pnlFormulario.Controls.Add(txtDescripcion);
            pnlFormulario.Controls.Add(lblDescripcion);
            pnlFormulario.Controls.Add(txtNombre);
            pnlFormulario.Controls.Add(lblNombre);
            pnlFormulario.Controls.Add(lblFormulario);
            pnlFormulario.Location = new Point(950, 128);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Size = new Size(578, 706);
            pnlFormulario.TabIndex = 0;
            // 
            // lblSeleccion
            // 
            lblSeleccion.AutoSize = true;
            lblSeleccion.Location = new Point(31, 60);
            lblSeleccion.Name = "lblSeleccion";
            lblSeleccion.Size = new Size(137, 25);
            lblSeleccion.TabIndex = 0;
            lblSeleccion.Text = "Nuevo registro";
            // 
            // btnNuevo
            // 
            btnNuevo.Cursor = Cursors.Hand;
            btnNuevo.FlatAppearance.BorderColor = Color.Black;
            btnNuevo.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 221, 202);
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.FromArgb(41, 41, 41);
            btnNuevo.Location = new Point(28, 390);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(468, 44);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "Nuevo / Limpiar";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnBorrar
            // 
            btnBorrar.Cursor = Cursors.Hand;
            btnBorrar.Enabled = false;
            btnBorrar.FlatAppearance.BorderSize = 1;
            btnBorrar.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 221, 202);
            btnBorrar.FlatStyle = FlatStyle.Flat;
            btnBorrar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnBorrar.ForeColor = Color.FromArgb(41, 41, 41);
            btnBorrar.Location = new Point(353, 465);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(146, 44);
            btnBorrar.TabIndex = 2;
            btnBorrar.Text = "Borrar";
            btnBorrar.UseVisualStyleBackColor = false;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Cursor = Cursors.Hand;
            btnEditar.Enabled = false;
            btnEditar.FlatAppearance.BorderSize = 1;
            btnEditar.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 221, 202);
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnEditar.ForeColor = Color.FromArgb(41, 41, 41);
            btnEditar.Location = new Point(192, 465);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(146, 44);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = false;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 1;
            btnGuardar.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 221, 202);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.FromArgb(41, 41, 41);
            btnGuardar.Location = new Point(31, 465);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(146, 44);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtDescripcion
            // 
            txtDescripcion.BackColor = Color.FromArgb(232, 221, 202);
            txtDescripcion.BorderStyle = BorderStyle.FixedSingle;
            txtDescripcion.Font = new Font("Segoe UI", 10F);
            txtDescripcion.ForeColor = Color.FromArgb(41, 41, 41);
            txtDescripcion.Location = new Point(31, 237);
            txtDescripcion.Margin = new Padding(4, 6, 4, 6);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "Descripción opcional";
            txtDescripcion.Size = new Size(516, 105);
            txtDescripcion.TabIndex = 5;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(31, 207);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(111, 25);
            lblDescripcion.TabIndex = 6;
            lblDescripcion.Text = "Descripción";
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(232, 221, 202);
            txtNombre.BorderStyle = BorderStyle.FixedSingle;
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.ForeColor = Color.FromArgb(41, 41, 41);
            txtNombre.Location = new Point(31, 137);
            txtNombre.Margin = new Padding(4, 6, 4, 6);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Ingrese el nombre";
            txtNombre.Size = new Size(516, 34);
            txtNombre.TabIndex = 7;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(31, 108);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(81, 25);
            lblNombre.TabIndex = 8;
            lblNombre.Text = "Nombre";
            // 
            // lblFormulario
            // 
            lblFormulario.AutoSize = true;
            lblFormulario.Location = new Point(30, 25);
            lblFormulario.Name = "lblFormulario";
            lblFormulario.Size = new Size(161, 25);
            lblFormulario.TabIndex = 9;
            lblFormulario.Text = "Datos del registro";
            // 
            // Frm_catalogo_rapido
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(250, 249, 246);
            ClientSize = new Size(1556, 867);
            Controls.Add(pnlFormulario);
            Controls.Add(pnlListado);
            Controls.Add(pnlEncabezado);
            Font = new Font("Segoe UI", 9.5F);
            ForeColor = Color.FromArgb(41, 41, 41);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Frm_catalogo_rapido";
            Text = "Catálogo";
            Load += Frm_catalogo_rapido_Load;
            pnlEncabezado.ResumeLayout(false);
            pnlEncabezado.PerformLayout();
            pnlListado.ResumeLayout(false);
            pnlListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCatalogo).EndInit();
            pnlFormulario.ResumeLayout(false);
            pnlFormulario.PerformLayout();
            txtBuscar.BackColor = Color.FromArgb(232, 221, 202);
            txtBuscar.ForeColor = Color.FromArgb(41, 41, 41);
            txtDescripcion.BackColor = Color.FromArgb(232, 221, 202);
            txtDescripcion.ForeColor = Color.FromArgb(41, 41, 41);
            txtNombre.BackColor = Color.FromArgb(232, 221, 202);
            txtNombre.ForeColor = Color.FromArgb(41, 41, 41);
            ResumeLayout(false);
            }

        #endregion

        private Panel pnlEncabezado;

        private Button btnVolver;

        private Label lblSubtitulo;
        private Label lblTitulo;

        private Panel pnlListado;

        private Label lblTotal;

        private Button btnBuscar;

        private TextBox txtBuscar;

        private DataGridView dgvCatalogo;

        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colDescripcion;
        private DataGridViewTextBoxColumn colEstado;

        private Panel pnlFormulario;

        private Label lblSeleccion;

        private Button btnNuevo;
        private Button btnBorrar;
        private Button btnEditar;
        private Button btnGuardar;

        private TextBox txtDescripcion;

        private Label lblDescripcion;

        private TextBox txtNombre;

        private Label lblNombre;
        private Label lblFormulario;
    }
}


