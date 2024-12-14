namespace AcademiaDesktop
{
    partial class HomeDocente
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
            button2 = new Button();
            button1 = new Button();
            panel1 = new Panel();
            button5 = new Button();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // button2
            // 
            button2.Location = new Point(419, 210);
            button2.Name = "button2";
            button2.Size = new Size(134, 34);
            button2.TabIndex = 14;
            button2.Text = "...";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(255, 210);
            button1.Name = "button1";
            button1.Size = new Size(134, 34);
            button1.TabIndex = 13;
            button1.Text = "Pasar notas";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(button5);
            panel1.Location = new Point(-1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(803, 46);
            panel1.TabIndex = 10;
            // 
            // button5
            // 
            button5.Location = new Point(700, 11);
            button5.Name = "button5";
            button5.Size = new Size(88, 23);
            button5.TabIndex = 17;
            button5.Text = "Cerrar sesión";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(181, 130);
            label1.Name = "label1";
            label1.Size = new Size(392, 28);
            label1.TabIndex = 15;
            label1.Text = "Bienvenido al sistema Académico - Docente";
            // 
            // HomeDocente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(panel1);
            Name = "HomeDocente";
            Text = "HomeDocente";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private Button button1;
        private Panel panel1;
        private Button button5;
        private Label label1;
    }
}
