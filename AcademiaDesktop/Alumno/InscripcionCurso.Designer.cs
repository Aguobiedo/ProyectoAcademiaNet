namespace AcademiaDesktop.Alumno
{
    partial class InscripcionCurso
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
            cboCursos = new ComboBox();
            btnGuardarInscripcion = new Button();
            label1 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // cboCursos
            // 
            cboCursos.FormattingEnabled = true;
            cboCursos.Location = new Point(291, 133);
            cboCursos.Margin = new Padding(4, 3, 4, 3);
            cboCursos.Name = "cboCursos";
            cboCursos.Size = new Size(233, 23);
            cboCursos.TabIndex = 2;
            // 
            // btnGuardarInscripcion
            // 
            btnGuardarInscripcion.Location = new Point(291, 216);
            btnGuardarInscripcion.Margin = new Padding(4, 3, 4, 3);
            btnGuardarInscripcion.Name = "btnGuardarInscripcion";
            btnGuardarInscripcion.Size = new Size(233, 27);
            btnGuardarInscripcion.TabIndex = 3;
            btnGuardarInscripcion.Text = "Guardar Inscripción";
            btnGuardarInscripcion.UseVisualStyleBackColor = true;
            btnGuardarInscripcion.Click += btnGuardarInscripcion_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(322, 70);
            label1.Name = "label1";
            label1.Size = new Size(175, 28);
            label1.TabIndex = 4;
            label1.Text = "Cursos disponibles";
            // 
            // button1
            // 
            button1.Location = new Point(713, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "Volver";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // InscripcionCurso
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(cboCursos);
            Controls.Add(btnGuardarInscripcion);
            Margin = new Padding(4, 3, 4, 3);
            Name = "InscripcionCurso";
            Text = "Inscripción Curso";
            Load += InscripcionCurso_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.ComboBox cboCursos; // Declarar el control ComboBox
        private System.Windows.Forms.Button btnGuardarInscripcion;
        private Label label1;
        private Button button1;
    }
}
