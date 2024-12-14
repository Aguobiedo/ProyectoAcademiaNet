using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using AcademiaDesktop.Alumno;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AcademiaDesktop
{
    public partial class HomeAlumno : Form
    {
        public HomeAlumno()
        {
            InitializeComponent();
            // Forzar uso de TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Ignorar los errores de certificado SSL (solo para desarrollo)
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        private async void HomeAlumno_Load(object sender, EventArgs e)
        {
            await cargarDatosAlumno();
        }

        private async Task cargarDatosAlumno()
        {
            // ID del alumno y token guardados en settings
            int idAlumno = Properties.Settings.Default.IdPersona;
            string token = Properties.Settings.Default.Token;
            int idPlan = Properties.Settings.Default.IdPlan;

            // Configurar el cliente RestSharp y la solicitud
            var options = new RestClientOptions("https://localhost:7090")
            {
                RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true // Ignorar validación SSL
            };
            var client = new RestClient(options);

            var request = new RestRequest($"api/Persona/{idAlumno}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");
            request.AddHeader("accept", "*/*");

            try
            {
                // Ejecutar solicitud y obtener respuesta
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    // Convertir respuesta a JSON y extraer datos
                    var jsonResponse = JObject.Parse(response.Content);
                    var data = jsonResponse["data"];
                    string nombre = data["nombre"].ToString();
                    string apellido = data["apellido"].ToString();
                    string legajo = data["legajo"].ToString();
                    string direccion = data["direccion"].ToString();
                    string email = data["email"].ToString();

                    Properties.Settings.Default.IdPlan = (int)data["idPlan"];
                    Properties.Settings.Default.Save();

                    // Mostrar datos en el panel
                    panel2.Controls.Clear();
                    Label infoLabel = new Label()
                    {
                        AutoSize = true,
                        Text = $"Nombre: {nombre}\nApellido: {apellido}\nLegajo: {legajo}\nDirección: {direccion}\nEmail: {email}\nPlan: {idPlan}",
                        Font = new Font("Arial", 12),
                        Location = new Point(10, 10),
                    };
                    panel2.Controls.Add(infoLabel);
                }
                else
                {
                    MessageBox.Show("Error al obtener los datos del alumno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la solicitud: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cursos curso = new Cursos();
            curso.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            InscripcionFinal inscripcionFinal = new InscripcionFinal();
            inscripcionFinal.Show();
            this.Hide();
        }
    }
}
