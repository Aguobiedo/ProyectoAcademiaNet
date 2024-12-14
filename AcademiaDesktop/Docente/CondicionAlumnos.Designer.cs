namespace AcademiaDesktop.Docente
{
    partial class CondicionAlumnos
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
            dataGridView1 = new DataGridView();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            IdInscripción = new DataGridViewTextBoxColumn();
            FechaInscripción = new DataGridViewTextBoxColumn();
            Nota = new DataGridViewTextBoxColumn();
            Condicion = new DataGridViewTextBoxColumn();
            Alumno = new DataGridViewTextBoxColumn();
            Materia = new DataGridViewTextBoxColumn();
            Comision = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(239, 22);
            label1.Name = "label1";
            label1.Size = new Size(298, 28);
            label1.TabIndex = 0;
            label1.Text = "Actualizar condición de Alumnos";
            label1.Click += label1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { IdInscripción, FechaInscripción, Nota, Condicion, Alumno, Materia, Comision });
            dataGridView1.Location = new Point(12, 96);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(744, 226);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(12, 63);
            label2.Name = "label2";
            label2.Size = new Size(84, 21);
            label2.TabIndex = 2;
            label2.Text = "Mis cursos";
            label2.Click += label2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 337);
            button1.Name = "button1";
            button1.Size = new Size(130, 39);
            button1.TabIndex = 3;
            button1.Text = "Actualizar condición";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(713, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "Volver";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // IdInscripción
            // 
            IdInscripción.HeaderText = "ID Inscripción";
            IdInscripción.Name = "IdInscripción";
            IdInscripción.ReadOnly = true;
            // 
            // FechaInscripción
            // 
            FechaInscripción.HeaderText = "Fecha Inscripción";
            FechaInscripción.Name = "FechaInscripción";
            FechaInscripción.ReadOnly = true;
            // 
            // Nota
            // 
            Nota.HeaderText = "Nota";
            Nota.Name = "Nota";
            // 
            // Condicion
            // 
            Condicion.HeaderText = "Condicion";
            Condicion.Name = "Condicion";
            // 
            // Alumno
            // 
            Alumno.HeaderText = "Alumno";
            Alumno.Name = "Alumno";
            Alumno.ReadOnly = true;
            // 
            // Materia
            // 
            Materia.HeaderText = "Materia";
            Materia.Name = "Materia";
            Materia.ReadOnly = true;
            // 
            // Comision
            // 
            Comision.HeaderText = "Comision";
            Comision.Name = "Comision";
            Comision.ReadOnly = true;
            // 
            // CondicionAlumnos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(dataGridView1);
            Controls.Add(label1);
            Name = "CondicionAlumnos";
            Text = "CondicionAlumnos";
            Load += CondicionAlumnos_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DataGridView dataGridView1;
        private Label label2;
        private Button button1;
        private Button button2;
        private DataGridViewTextBoxColumn IdInscripción;
        private DataGridViewTextBoxColumn FechaInscripción;
        private DataGridViewTextBoxColumn Nota;
        private DataGridViewTextBoxColumn Condicion;
        private DataGridViewTextBoxColumn Alumno;
        private DataGridViewTextBoxColumn Materia;
        private DataGridViewTextBoxColumn Comision;
    }
}