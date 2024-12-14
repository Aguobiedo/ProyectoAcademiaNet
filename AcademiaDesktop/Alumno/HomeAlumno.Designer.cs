namespace AcademiaDesktop
{
    partial class HomeAlumno
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
            panel1 = new Panel();
            button5 = new Button();
            label1 = new Label();
            panel2 = new Panel();
            button1 = new Button();
            button4 = new Button();
            button3 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = SystemColors.ActiveCaption;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(button5);
            panel1.Location = new Point(-4, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(886, 46);
            panel1.TabIndex = 1;
            // 
            // button5
            // 
            button5.Location = new Point(703, 12);
            button5.Name = "button5";
            button5.Size = new Size(88, 23);
            button5.TabIndex = 16;
            button5.Text = "Cerrar sesión";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(217, 99);
            label1.Name = "label1";
            label1.Size = new Size(385, 28);
            label1.TabIndex = 3;
            label1.Text = "Bienvenido al sistema académico - Alumno";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ControlLightLight;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Location = new Point(247, 142);
            panel2.Name = "panel2";
            panel2.Size = new Size(305, 135);
            panel2.TabIndex = 4;
            panel2.Paint += panel2_Paint;
            // 
            // button1
            // 
            button1.Location = new Point(247, 360);
            button1.Name = "button1";
            button1.Size = new Size(134, 34);
            button1.TabIndex = 5;
            button1.Text = "Ver Notas";
            button1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(418, 301);
            button4.Name = "button4";
            button4.Size = new Size(134, 34);
            button4.TabIndex = 8;
            button4.Text = "Rendir una materia";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(247, 301);
            button3.Name = "button3";
            button3.Size = new Size(134, 34);
            button3.TabIndex = 9;
            button3.Text = "Mis cursos";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // HomeAlumno
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(button1);
            Controls.Add(panel2);
            Controls.Add(label1);
            Controls.Add(panel1);
            Name = "HomeAlumno";
            Text = "HomeAlumno";
            Load += HomeAlumno_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Button button1;
        private Button button4;
        private Button button3;
        private Button button5;
    }
}