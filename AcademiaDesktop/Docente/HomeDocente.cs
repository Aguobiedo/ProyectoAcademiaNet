using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcademiaDesktop.Docente;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AcademiaDesktop
{
    public partial class HomeDocente : Form
    {
        public HomeDocente()
        {
            InitializeComponent();
        }

       

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new CondicionAlumnos().Show();
            this.Hide();
        }
    }
}
