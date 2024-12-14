using AcademiaDesktop.NoDocente;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademiaDesktop
{
    public partial class HomeNoDocente : Form
    {
        public HomeNoDocente()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void HomeNoDocente_Load(object sender, EventArgs e)
        {

        }



        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GestionAlumno gestionAlumno = new GestionAlumno();
            gestionAlumno.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GestionDocente gestionDocente = new GestionDocente();
            gestionDocente.Show();
            this.Hide();
                
        }
    }
}
