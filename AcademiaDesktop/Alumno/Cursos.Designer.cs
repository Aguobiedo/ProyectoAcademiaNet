namespace AcademiaDesktop.Alumno
{
    partial class Cursos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvInscripciones = new DataGridView();
            btnEliminarInscripcion = new Button();
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvInscripciones).BeginInit();
            SuspendLayout();
            // 
            // dgvInscripciones
            // 
            dgvInscripciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInscripciones.Location = new Point(12, 12);
            dgvInscripciones.Name = "dgvInscripciones";
            dgvInscripciones.Size = new Size(776, 376);
            dgvInscripciones.TabIndex = 0;
            dgvInscripciones.CellContentClick += dgvInscripciones_CellContentClick;
            // 
            // btnEliminarInscripcion
            // 
            btnEliminarInscripcion.Location = new Point(192, 394);
            btnEliminarInscripcion.Name = "btnEliminarInscripcion";
            btnEliminarInscripcion.Size = new Size(175, 44);
            btnEliminarInscripcion.TabIndex = 1;
            btnEliminarInscripcion.Text = "Eliminar Inscripción";
            btnEliminarInscripcion.UseVisualStyleBackColor = true;
            btnEliminarInscripcion.Click += btnEliminarInscripcion_Click;
            // 
            // button1
            // 
            button1.Location = new Point(11, 394);
            button1.Name = "button1";
            button1.Size = new Size(175, 44);
            button1.TabIndex = 2;
            button1.Text = "Inscribirse a una Materia";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(713, 405);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "Volver";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Cursos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btnEliminarInscripcion);
            Controls.Add(dgvInscripciones);
            Name = "Cursos";
            Text = "Cursos";
            Load += Cursos_Load;
            ((System.ComponentModel.ISupportInitialize)dgvInscripciones).EndInit();
            ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvInscripciones;
        private System.Windows.Forms.Button btnEliminarInscripcion;
        private Button button1;
        private Button button2;
    }
}
