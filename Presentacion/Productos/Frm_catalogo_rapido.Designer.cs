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
            btnVolver.Location = new Point(1325, 39);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(195, 45);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver a productos";
            btnVolver.Click += btnVolver_Click;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Location = new Point(38, 72);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(392, 25);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Guarde, edite o desactive registros del catálogo.";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(33, 22);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(171, 25);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Gestión de catálogo";
            // 
            // pnlListado
            // 
            pnlListado.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pnlListado.Controls.Add(lblTotal);
            pnlListado.Controls.Add(btnBuscar);
            pnlListado.Controls.Add(txtBuscar);
            pnlListado.Controls.Add(dgvCatalogo);
            pnlListado.Location = new Point(28, 128);
            pnlListado.Name = "pnlListado";
            pnlListado.Size = new Size(950, 706);
            pnlListado.TabIndex = 1;
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(31, 664);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(104, 25);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "Registros: 0";
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(770, 25);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(145, 42);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            btnBuscar.Click += btnBuscar_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(31, 25);
            txtBuscar.Margin = new Padding(4, 6, 4, 6);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nombre";
            txtBuscar.Size = new Size(720, 31);
            txtBuscar.TabIndex = 2;
            txtBuscar.KeyDown += txtBuscar_KeyDown;
            // 
            // dgvCatalogo
            // 
            dgvCatalogo.AllowUserToAddRows = false;
            dgvCatalogo.AllowUserToDeleteRows = false;
            dgvCatalogo.AllowUserToResizeRows = false;
            dgvCatalogo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCatalogo.ColumnHeadersHeight = 44;
            dgvCatalogo.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colDescripcion, colEstado });
            dgvCatalogo.Location = new Point(31, 84);
            dgvCatalogo.MultiSelect = false;
            dgvCatalogo.Name = "dgvCatalogo";
            dgvCatalogo.ReadOnly = true;
            dgvCatalogo.RowHeadersVisible = false;
            dgvCatalogo.RowHeadersWidth = 62;
            dgvCatalogo.RowTemplate.Height = 40;
            dgvCatalogo.Size = new Size(884, 560);
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
            pnlFormulario.Location = new Point(998, 128);
            pnlFormulario.Name = "pnlFormulario";
            pnlFormulario.Size = new Size(530, 706);
            pnlFormulario.TabIndex = 0;
            // 
            // lblSeleccion
            // 
            lblSeleccion.AutoSize = true;
            lblSeleccion.Location = new Point(31, 60);
            lblSeleccion.Name = "lblSeleccion";
            lblSeleccion.Size = new Size(130, 25);
            lblSeleccion.TabIndex = 0;
            lblSeleccion.Text = "Nuevo registro";
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(28, 390);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(169, 44);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "Nuevo / Limpiar";
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnBorrar
            // 
            btnBorrar.Enabled = false;
            btnBorrar.Location = new Point(99, 501);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(145, 44);
            btnBorrar.TabIndex = 2;
            btnBorrar.Text = "Borrar";
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Enabled = false;
            btnEditar.Location = new Point(193, 451);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(145, 44);
            btnEditar.TabIndex = 3;
            btnEditar.Text = "Editar";
            btnEditar.Click += btnEditar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(16, 451);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(145, 44);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(31, 237);
            txtDescripcion.Margin = new Padding(4, 6, 4, 6);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.PlaceholderText = "Descripción opcional";
            txtDescripcion.Size = new Size(468, 105);
            txtDescripcion.TabIndex = 5;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(31, 207);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(104, 25);
            lblDescripcion.TabIndex = 6;
            lblDescripcion.Text = "Descripción";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(31, 137);
            txtNombre.Margin = new Padding(4, 6, 4, 6);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Ingrese el nombre";
            txtNombre.Size = new Size(468, 31);
            txtNombre.TabIndex = 7;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(31, 108);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(78, 25);
            lblNombre.TabIndex = 8;
            lblNombre.Text = "Nombre";
            // 
            // lblFormulario
            // 
            lblFormulario.AutoSize = true;
            lblFormulario.Location = new Point(30, 25);
            lblFormulario.Name = "lblFormulario";
            lblFormulario.Size = new Size(154, 25);
            lblFormulario.TabIndex = 9;
            lblFormulario.Text = "Datos del registro";
            // 
            // Frm_catalogo_rapido
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 241, 232);
            ClientSize = new Size(1556, 867);
            Controls.Add(pnlFormulario);
            Controls.Add(pnlListado);
            Controls.Add(pnlEncabezado);
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