namespace Nk_Colletion_New.Presentacion.Autenticacion
{
    partial class Frm_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Login));
            guna2ContextMenuStrip1 = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            txt_Contrasena = new TextBox();
            btn_Ingresar = new Button();
            label1 = new Label();
            linkLabel1 = new LinkLabel();
            panel1 = new Panel();
            panel2 = new Panel();
            label2 = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            txt_Usuario = new TextBox();
            pictureBox1 = new PictureBox();
            panel5 = new Panel();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // guna2ContextMenuStrip1
            // 
            guna2ContextMenuStrip1.ImageScalingSize = new Size(24, 24);
            guna2ContextMenuStrip1.Name = "guna2ContextMenuStrip1";
            guna2ContextMenuStrip1.RenderStyle.ArrowColor = Color.FromArgb(151, 143, 255);
            guna2ContextMenuStrip1.RenderStyle.BorderColor = Color.Gainsboro;
            guna2ContextMenuStrip1.RenderStyle.ColorTable = null;
            guna2ContextMenuStrip1.RenderStyle.RoundedEdges = true;
            guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = Color.White;
            guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = Color.FromArgb(100, 88, 255);
            guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = Color.White;
            guna2ContextMenuStrip1.RenderStyle.SeparatorColor = Color.Gainsboro;
            guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            guna2ContextMenuStrip1.Size = new Size(61, 4);
            // 
            // txt_Contrasena
            // 
            txt_Contrasena.Location = new Point(3, 3);
            txt_Contrasena.Multiline = true;
            txt_Contrasena.Name = "txt_Contrasena";
            txt_Contrasena.Size = new Size(373, 63);
            txt_Contrasena.TabIndex = 1;
            // 
            // btn_Ingresar
            // 
            btn_Ingresar.BackColor = Color.Maroon;
            btn_Ingresar.Font = new Font("PMingLiU-ExtB", 10F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            btn_Ingresar.ForeColor = SystemColors.ControlLightLight;
            btn_Ingresar.Location = new Point(185, 401);
            btn_Ingresar.Name = "btn_Ingresar";
            btn_Ingresar.Size = new Size(158, 50);
            btn_Ingresar.TabIndex = 2;
            btn_Ingresar.Text = "Ingresar";
            btn_Ingresar.UseVisualStyleBackColor = false;
            btn_Ingresar.Click += btn_Ingresar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("PMingLiU-ExtB", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(75, 167);
            label1.Name = "label1";
            label1.Size = new Size(69, 18);
            label1.TabIndex = 3;
            label1.Text = "Usuario";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.LinkColor = Color.FromArgb(64, 0, 0);
            linkLabel1.Location = new Point(157, 470);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(214, 25);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "¿Olvidaste tu contraseña?";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(338, 518);
            panel1.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ButtonHighlight;
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(linkLabel1);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(btn_Ingresar);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(panel4);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(335, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(511, 518);
            panel2.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("PMingLiU-ExtB", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(75, 270);
            label2.Name = "label2";
            label2.Size = new Size(96, 18);
            label2.TabIndex = 5;
            label2.Text = "Contraseña";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(64, 0, 0);
            panel3.Controls.Add(txt_Contrasena);
            panel3.Location = new Point(75, 291);
            panel3.Name = "panel3";
            panel3.Size = new Size(379, 69);
            panel3.TabIndex = 6;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(64, 0, 0);
            panel4.Controls.Add(txt_Usuario);
            panel4.Location = new Point(75, 188);
            panel4.Name = "panel4";
            panel4.Size = new Size(379, 69);
            panel4.TabIndex = 7;
            // 
            // txt_Usuario
            // 
            txt_Usuario.Location = new Point(3, 3);
            txt_Usuario.Multiline = true;
            txt_Usuario.Name = "txt_Usuario";
            txt_Usuario.Size = new Size(373, 63);
            txt_Usuario.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.imagen_circular_recortada1;
            pictureBox1.Location = new Point(194, 21);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(131, 125);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(64, 0, 0);
            panel5.Dock = DockStyle.Left;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(10, 518);
            panel5.TabIndex = 9;
            // 
            // Frm_Login
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(846, 518);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Frm_Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Frm_Login";
            Load += Frm_Login_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ContextMenuStrip guna2ContextMenuStrip1;
        private TextBox txt_Contrasena;
        private Button btn_Ingresar;
        private Label label1;
        private LinkLabel linkLabel1;
        private Panel panel1;
        private Panel panel2;
        private TextBox txt_Usuario;
        private Label label2;
        private Panel panel3;
        private Panel panel4;
        private PictureBox pictureBox1;
        private Panel panel5;
    }
}