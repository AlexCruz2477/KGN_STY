namespace Nk_Colletion_New
{
    partial class Form_recuperacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_recuperacion));
            panel1 = new Panel();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            btncontinuar = new Button();
            label3 = new Label();
            btncancelar = new Button();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(label4);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(btncontinuar);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(btncancelar);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBox1);
            panel1.Location = new Point(18, 17);
            panel1.Name = "panel1";
            panel1.Size = new Size(828, 508);
            panel1.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(324, 430);
            label4.Name = "label4";
            label4.Size = new Size(191, 25);
            label4.TabIndex = 11;
            label4.Text = "- - - - - - - o - - - - - - -";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(202, 315);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(43, 31);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // btncontinuar
            // 
            btncontinuar.BackColor = Color.Maroon;
            btncontinuar.ForeColor = Color.White;
            btncontinuar.Location = new Point(479, 382);
            btncontinuar.Name = "btncontinuar";
            btncontinuar.Size = new Size(112, 34);
            btncontinuar.TabIndex = 2;
            btncontinuar.Text = "Continuar";
            btncontinuar.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.ForeColor = Color.Peru;
            label3.Location = new Point(307, 469);
            label3.Name = "label3";
            label3.Size = new Size(230, 25);
            label3.TabIndex = 5;
            label3.Text = "Volver a la pantalla anterior.";
            // 
            // btncancelar
            // 
            btncancelar.ForeColor = Color.Maroon;
            btncancelar.Location = new Point(238, 382);
            btncancelar.Name = "btncancelar";
            btncancelar.Size = new Size(123, 34);
            btncancelar.TabIndex = 1;
            btncancelar.Text = "Cancelar";
            btncancelar.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(339, 34);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(139, 129);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 9;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Maroon;
            label1.Location = new Point(282, 184);
            label1.Name = "label1";
            label1.Size = new Size(266, 32);
            label1.TabIndex = 3;
            label1.Text = "Recuperar contraseña";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Location = new Point(238, 233);
            label2.Name = "label2";
            label2.Size = new Size(375, 50);
            label2.TabIndex = 4;
            label2.Text = "Ingrese su dirección de correo electrónico y te\r\n enviaremos un enlace para restablecerla.";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(260, 315);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(353, 31);
            textBox1.TabIndex = 0;
            // 
            // Form_recuperacion
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 0, 0);
            ClientSize = new Size(864, 542);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form_recuperacion";
            Text = "Form_recuperacion";
            Load += Form_recuperacion_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label4;
        private PictureBox pictureBox1;
        private Button btncontinuar;
        private Label label3;
        private Button btncancelar;
        private PictureBox pictureBox2;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
    }
}