namespace AcademiaDesktop.NoDocente
{
    partial class GestionDocente
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
            dgvDocentes = new DataGridView();
            idPersona = new DataGridViewTextBoxColumn();
            nombre = new DataGridViewTextBoxColumn();
            apellido = new DataGridViewTextBoxColumn();
            direccion = new DataGridViewTextBoxColumn();
            email = new DataGridViewTextBoxColumn();
            Telefono = new DataGridViewTextBoxColumn();
            FechaNacimiento = new DataGridViewTextBoxColumn();
            Tipo = new DataGridViewTextBoxColumn();
            legajo = new DataGridViewTextBoxColumn();
            Clave = new DataGridViewTextBoxColumn();
            textBox1 = new TextBox();
            label1 = new Label();
            buttonPut = new Button();
            txtNombre = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtApellido = new TextBox();
            label4 = new Label();
            txtDireccion = new TextBox();
            label5 = new Label();
            txtEmail = new TextBox();
            label6 = new Label();
            label7 = new Label();
            txtTelefono = new TextBox();
            dtpFechaNacimiento = new DateTimePicker();
            label8 = new Label();
            txtLegajo = new TextBox();
            label9 = new Label();
            txtClave = new TextBox();
            button3 = new Button();
            button2 = new Button();
            button4 = new Button();
            button5 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDocentes).BeginInit();
            SuspendLayout();
            // 
            // dgvDocentes
            // 
            dgvDocentes.AllowUserToAddRows = false;
            dgvDocentes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDocentes.Columns.AddRange(new DataGridViewColumn[] { idPersona, nombre, apellido, direccion, email, Telefono, FechaNacimiento, Tipo, legajo, Clave });
            dgvDocentes.Location = new Point(14, 14);
            dgvDocentes.Margin = new Padding(4, 3, 4, 3);
            dgvDocentes.Name = "dgvDocentes";
            dgvDocentes.Size = new Size(1005, 174);
            dgvDocentes.TabIndex = 0;
            dgvDocentes.CellContentClick += dgvDocentes_CellContentClick;
            // 
            // idPersona
            // 
            idPersona.HeaderText = "ID";
            idPersona.Name = "idPersona";
            idPersona.ReadOnly = true;
            // 
            // nombre
            // 
            nombre.HeaderText = "Nombre";
            nombre.Name = "nombre";
            // 
            // apellido
            // 
            apellido.HeaderText = "Apellido";
            apellido.Name = "apellido";
            // 
            // direccion
            // 
            direccion.HeaderText = "Dirección";
            direccion.Name = "direccion";
            // 
            // email
            // 
            email.HeaderText = "Email";
            email.Name = "email";
            // 
            // Telefono
            // 
            Telefono.HeaderText = "Telefono";
            Telefono.Name = "Telefono";
            Telefono.Visible = false;
            // 
            // FechaNacimiento
            // 
            FechaNacimiento.HeaderText = "FechaNacimiento";
            FechaNacimiento.Name = "FechaNacimiento";
            // 
            // Tipo
            // 
            Tipo.HeaderText = "Tipo";
            Tipo.Name = "Tipo";
            Tipo.ReadOnly = true;
            // 
            // legajo
            // 
            legajo.HeaderText = "Legajo";
            legajo.Name = "legajo";
            // 
            // Clave
            // 
            Clave.HeaderText = "Clave";
            Clave.Name = "Clave";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(13, 215);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(325, 23);
            textBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 196);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(92, 15);
            label1.TabIndex = 2;
            label1.Text = "Buscar Docente:";
            // 
            // buttonPut
            // 
            buttonPut.Location = new Point(107, 245);
            buttonPut.Margin = new Padding(4, 3, 4, 3);
            buttonPut.Name = "buttonPut";
            buttonPut.Size = new Size(88, 27);
            buttonPut.TabIndex = 5;
            buttonPut.Text = "Actualizar";
            buttonPut.UseVisualStyleBackColor = true;
            buttonPut.Click += buttonPut_Click;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(81, 300);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(257, 23);
            txtNombre.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 303);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 9;
            label2.Text = "Nombre";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 332);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 11;
            label3.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(81, 329);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(257, 23);
            txtApellido.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 363);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 13;
            label4.Text = "Direccion";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(81, 360);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(257, 23);
            txtDireccion.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(14, 392);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 15;
            label5.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(81, 389);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(257, 23);
            txtEmail.TabIndex = 14;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 456);
            label6.Name = "label6";
            label6.Size = new Size(103, 15);
            label6.TabIndex = 19;
            label6.Text = "Fecha Nacimiento";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 421);
            label7.Name = "label7";
            label7.Size = new Size(52, 15);
            label7.TabIndex = 17;
            label7.Text = "Telefono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(81, 418);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(257, 23);
            txtTelefono.TabIndex = 16;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Location = new Point(138, 450);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(200, 23);
            dtpFechaNacimiento.TabIndex = 20;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(376, 303);
            label8.Name = "label8";
            label8.Size = new Size(42, 15);
            label8.TabIndex = 22;
            label8.Text = "Legajo";
            // 
            // txtLegajo
            // 
            txtLegajo.Location = new Point(443, 300);
            txtLegajo.Name = "txtLegajo";
            txtLegajo.Size = new Size(257, 23);
            txtLegajo.TabIndex = 21;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(376, 335);
            label9.Name = "label9";
            label9.Size = new Size(36, 15);
            label9.TabIndex = 24;
            label9.Text = "Clave";
            // 
            // txtClave
            // 
            txtClave.Location = new Point(443, 332);
            txtClave.Name = "txtClave";
            txtClave.Size = new Size(257, 23);
            txtClave.TabIndex = 23;
            // 
            // button3
            // 
            button3.Location = new Point(11, 245);
            button3.Margin = new Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new Size(88, 27);
            button3.TabIndex = 25;
            button3.Text = "Agregar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(944, 484);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 27;
            button2.Text = "Volver";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button4
            // 
            button4.Location = new Point(203, 245);
            button4.Margin = new Padding(4, 3, 4, 3);
            button4.Name = "button4";
            button4.Size = new Size(88, 27);
            button4.TabIndex = 28;
            button4.Text = "Eliminar";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(366, 215);
            button5.Margin = new Padding(4, 3, 4, 3);
            button5.Name = "button5";
            button5.Size = new Size(88, 27);
            button5.TabIndex = 29;
            button5.Text = "Buscar";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // GestionDocente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1031, 519);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button2);
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
            Controls.Add(buttonPut);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(dgvDocentes);
            Margin = new Padding(4, 3, 4, 3);
            Name = "GestionDocente";
            Text = "Gestión de Docentes";
            Load += GestionDocente_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDocentes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvDocentes;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPut;
        private TextBox txtNombre;
        private Label label2;
        private Label label3;
        private TextBox txtApellido;
        private Label label4;
        private TextBox txtDireccion;
        private Label label5;
        private TextBox txtEmail;
        private Label label6;
        private Label label7;
        private TextBox txtTelefono;
        private DateTimePicker dtpFechaNacimiento;
        private Label label8;
        private TextBox txtLegajo;
        private Label label9;
        private TextBox txtClave;
        private DataGridViewTextBoxColumn idPersona;
        private DataGridViewTextBoxColumn nombre;
        private DataGridViewTextBoxColumn apellido;
        private DataGridViewTextBoxColumn direccion;
        private DataGridViewTextBoxColumn email;
        private DataGridViewTextBoxColumn Telefono;
        private DataGridViewTextBoxColumn FechaNacimiento;
        private DataGridViewTextBoxColumn Tipo;
        private DataGridViewTextBoxColumn legajo;
        private DataGridViewTextBoxColumn Clave;
        private Button button3;
        private Button button2;
        private Button button4;
        private Button button5;
    }
}
