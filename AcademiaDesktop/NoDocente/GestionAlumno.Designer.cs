namespace AcademiaDesktop.NoDocente
{
    partial class GestionAlumno
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
            dgvAlumnos = new DataGridView();
            label1 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button4 = new Button();
            button3 = new Button();
            label9 = new Label();
            txtClave = new TextBox();
            label8 = new Label();
            txtLegajo = new TextBox();
            dtpFechaNacimiento = new DateTimePicker();
            label6 = new Label();
            label7 = new Label();
            txtTelefono = new TextBox();
            label5 = new Label();
            txtEmail = new TextBox();
            label4 = new Label();
            txtDireccion = new TextBox();
            label3 = new Label();
            txtApellido = new TextBox();
            label2 = new Label();
            txtNombre = new TextBox();
            label11 = new Label();
            txtIdPlan = new TextBox();
            Actualizar = new Button();
            IdPersona = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Apellido = new DataGridViewTextBoxColumn();
            direccion = new DataGridViewTextBoxColumn();
            Email = new DataGridViewTextBoxColumn();
            Telefono = new DataGridViewTextBoxColumn();
            FechaNacimiento = new DataGridViewTextBoxColumn();
            Legajo = new DataGridViewTextBoxColumn();
            Clave = new DataGridViewTextBoxColumn();
            IdPlan = new DataGridViewTextBoxColumn();
            DescPlan = new DataGridViewTextBoxColumn();
            Carrera = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).BeginInit();
            SuspendLayout();
            // 
            // dgvAlumnos
            // 
            dgvAlumnos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAlumnos.Columns.AddRange(new DataGridViewColumn[] { IdPersona, Nombre, Apellido, direccion, Email, Telefono, FechaNacimiento, Legajo, Clave, IdPlan, DescPlan, Carrera });
            dgvAlumnos.Location = new Point(12, 84);
            dgvAlumnos.Name = "dgvAlumnos";
            dgvAlumnos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAlumnos.Size = new Size(990, 158);
            dgvAlumnos.TabIndex = 0;
            dgvAlumnos.CellContentClick += dgvAlumnos_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(25, 5);
            label1.Name = "label1";
            label1.Size = new Size(63, 28);
            label1.TabIndex = 2;
            label1.Text = "Filtrar";
            label1.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 39);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(130, 39);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "Buscar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(788, 10);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 10;
            button2.Text = "Volver";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button4
            // 
            button4.Location = new Point(131, 257);
            button4.Margin = new Padding(4, 3, 4, 3);
            button4.Name = "button4";
            button4.Size = new Size(88, 27);
            button4.TabIndex = 50;
            button4.Text = "Eliminar";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(13, 257);
            button3.Margin = new Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new Size(88, 27);
            button3.TabIndex = 49;
            button3.Text = "Agregar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(366, 366);
            label9.Name = "label9";
            label9.Size = new Size(36, 15);
            label9.TabIndex = 48;
            label9.Text = "Clave";
            // 
            // txtClave
            // 
            txtClave.Location = new Point(433, 363);
            txtClave.Name = "txtClave";
            txtClave.Size = new Size(257, 23);
            txtClave.TabIndex = 47;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(366, 334);
            label8.Name = "label8";
            label8.Size = new Size(42, 15);
            label8.TabIndex = 46;
            label8.Text = "Legajo";
            // 
            // txtLegajo
            // 
            txtLegajo.Location = new Point(433, 331);
            txtLegajo.Name = "txtLegajo";
            txtLegajo.Size = new Size(257, 23);
            txtLegajo.TabIndex = 45;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Location = new Point(139, 452);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(200, 23);
            dtpFechaNacimiento.TabIndex = 44;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(13, 458);
            label6.Name = "label6";
            label6.Size = new Size(103, 15);
            label6.TabIndex = 43;
            label6.Text = "Fecha Nacimiento";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 423);
            label7.Name = "label7";
            label7.Size = new Size(52, 15);
            label7.TabIndex = 42;
            label7.Text = "Telefono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(82, 420);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(257, 23);
            txtTelefono.TabIndex = 41;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 394);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 40;
            label5.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(82, 391);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(257, 23);
            txtEmail.TabIndex = 39;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 365);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 38;
            label4.Text = "Direccion";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(82, 362);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(257, 23);
            txtDireccion.TabIndex = 37;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 334);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 36;
            label3.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(82, 331);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(257, 23);
            txtApellido.TabIndex = 35;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 305);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 34;
            label2.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(82, 302);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(257, 23);
            txtNombre.TabIndex = 33;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(367, 305);
            label11.Name = "label11";
            label11.Size = new Size(30, 15);
            label11.TabIndex = 52;
            label11.Text = "Plan";
            // 
            // txtIdPlan
            // 
            txtIdPlan.Location = new Point(433, 297);
            txtIdPlan.Name = "txtIdPlan";
            txtIdPlan.Size = new Size(257, 23);
            txtIdPlan.TabIndex = 53;
            // 
            // Actualizar
            // 
            Actualizar.Location = new Point(255, 261);
            Actualizar.Name = "Actualizar";
            Actualizar.Size = new Size(75, 23);
            Actualizar.TabIndex = 54;
            Actualizar.Text = "Actualizar";
            Actualizar.UseVisualStyleBackColor = true;
            Actualizar.Click += Actualizar_Click;
            // 
            // IdPersona
            // 
            IdPersona.HeaderText = "IdPersona";
            IdPersona.Name = "IdPersona";
            IdPersona.ReadOnly = true;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.Name = "Nombre";
            // 
            // Apellido
            // 
            Apellido.HeaderText = "Apellido";
            Apellido.Name = "Apellido";
            // 
            // direccion
            // 
            direccion.HeaderText = "Direccion";
            direccion.Name = "direccion";
            // 
            // Email
            // 
            Email.HeaderText = "Email";
            Email.Name = "Email";
            // 
            // Telefono
            // 
            Telefono.HeaderText = "Telefono";
            Telefono.Name = "Telefono";
            // 
            // FechaNacimiento
            // 
            FechaNacimiento.HeaderText = "FechaNacimiento";
            FechaNacimiento.Name = "FechaNacimiento";
            // 
            // Legajo
            // 
            Legajo.HeaderText = "Legajo";
            Legajo.Name = "Legajo";
            Legajo.ReadOnly = true;
            // 
            // Clave
            // 
            Clave.HeaderText = "Clave";
            Clave.Name = "Clave";
            // 
            // IdPlan
            // 
            IdPlan.HeaderText = "IdPlan";
            IdPlan.Name = "IdPlan";
            // 
            // DescPlan
            // 
            DescPlan.HeaderText = "Plan";
            DescPlan.Name = "DescPlan";
            DescPlan.ReadOnly = true;
            // 
            // Carrera
            // 
            Carrera.HeaderText = "Carrera";
            Carrera.Name = "Carrera";
            Carrera.ReadOnly = true;
            // 
            // GestionAlumno
            // 
            ClientSize = new Size(894, 561);
            Controls.Add(Actualizar);
            Controls.Add(txtIdPlan);
            Controls.Add(label11);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label9);
            Controls.Add(txtClave);
            Controls.Add(label8);
            Controls.Add(txtLegajo);
            Controls.Add(dtpFechaNacimiento);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(txtTelefono);
            Controls.Add(label5);
            Controls.Add(txtEmail);
            Controls.Add(label4);
            Controls.Add(txtDireccion);
            Controls.Add(label3);
            Controls.Add(txtApellido);
            Controls.Add(label2);
            Controls.Add(txtNombre);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(dgvAlumnos);
            Name = "GestionAlumno";
            Load += GestionAlumno_Load;
            ((System.ComponentModel.ISupportInitialize)dgvAlumnos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvAlumnos;
        private Label label1;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Button button4;
        private Button button3;
        private Label label9;
        private TextBox txtClave;
        private Label label8;
        private TextBox txtLegajo;
        private DateTimePicker dtpFechaNacimiento;
        private Label label6;
        private Label label7;
        private TextBox txtTelefono;
        private Label label5;
        private TextBox txtEmail;
        private Label label4;
        private TextBox txtDireccion;
        private Label label3;
        private TextBox txtApellido;
        private Label label2;
        private TextBox txtNombre;
        private Label label11;
        private TextBox txtIdPlan;
        private Button Actualizar;
        private DataGridViewTextBoxColumn IdPersona;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Apellido;
        private DataGridViewTextBoxColumn direccion;
        private DataGridViewTextBoxColumn Email;
        private DataGridViewTextBoxColumn Telefono;
        private DataGridViewTextBoxColumn FechaNacimiento;
        private DataGridViewTextBoxColumn Legajo;
        private DataGridViewTextBoxColumn Clave;
        private DataGridViewTextBoxColumn IdPlan;
        private DataGridViewTextBoxColumn DescPlan;
        private DataGridViewTextBoxColumn Carrera;
    }
}
