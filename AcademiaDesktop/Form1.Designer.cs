namespace AcademiaDesktop
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtLegajo;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Button btnLogin;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtLegajo = new TextBox();
            txtClave = new TextBox();
            btnLogin = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // txtLegajo
            // 
            txtLegajo.Location = new Point(310, 175);
            txtLegajo.Name = "txtLegajo";
            txtLegajo.Size = new Size(200, 23);
            txtLegajo.TabIndex = 0;
            // 
            // txtClave
            // 
            txtClave.Location = new Point(310, 246);
            txtClave.Name = "txtClave";
            txtClave.PasswordChar = '*';
            txtClave.Size = new Size(200, 23);
            txtClave.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(353, 308);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(108, 44);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(246, 178);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 3;
            label1.Text = "Legajo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(252, 249);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 4;
            label2.Text = "Clave";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 25F);
            label3.Location = new Point(192, 87);
            label3.Name = "label3";
            label3.Size = new Size(422, 46);
            label3.TabIndex = 5;
            label3.Text = "Bienvenido a AcademiaNet";
            // 
            // Form1
            // 
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLogin);
            Controls.Add(txtClave);
            Controls.Add(txtLegajo);
            Name = "Form1";
            Text = "Iniciar Sesión";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
    }
}
