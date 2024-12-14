namespace AcademiaDesktop
{
    partial class HomeNoDocente
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
            label1 = new Label();
            panel1 = new Panel();
            button4 = new Button();
            button3 = new Button();
            button1 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F);
            label1.Location = new Point(174, 112);
            label1.Name = "label1";
            label1.Size = new Size(434, 37);
            label1.TabIndex = 0;
            label1.Text = "Gestion de Alumnos - No Docentes";
            label1.Click += label1_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(button4);
            panel1.Location = new Point(-1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(802, 46);
            panel1.TabIndex = 11;
            // 
            // button4
            // 
            button4.Location = new Point(700, 11);
            button4.Name = "button4";
            button4.Size = new Size(88, 23);
            button4.TabIndex = 15;
            button4.Text = "Cerrar sesión";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(397, 215);
            button3.Name = "button3";
            button3.Size = new Size(97, 48);
            button3.TabIndex = 14;
            button3.Text = "Docentes";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button1
            // 
            button1.Location = new Point(269, 215);
            button1.Name = "button1";
            button1.Size = new Size(97, 48);
            button1.TabIndex = 15;
            button1.Text = "Alumnos";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // HomeNoDocente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(button3);
            Controls.Add(label1);
            Controls.Add(panel1);
            Name = "HomeNoDocente";
            Text = "HomeNoDocente";
            Load += HomeNoDocente_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Button button3;
        private Button button4;
        private Button button1;
    }
}