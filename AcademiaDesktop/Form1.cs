using RestSharp;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net;

namespace AcademiaDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Forzar uso de TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Ignorar los errores de certificado SSL (solo para desarrollo)
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            // Obtener legajo y clave
            string legajo = txtLegajo.Text;
            string clave = txtClave.Text;

            // Verificar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(legajo) || string.IsNullOrWhiteSpace(clave))
            {
                MessageBox.Show("Por favor ingresa el legajo y la clave.");
                return;
            }

            try
            {
                // Crear el cliente RestSharp apuntando a la API con configuración para ignorar SSL
                var options = new RestClientOptions("https://localhost:7090")
                {
                    RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true // Ignorar validación SSL
                };
                var client = new RestClient(options);

                // Crear la solicitud de login (POST)
                var request = new RestRequest("api/Persona/login", Method.Post);
                request.AddJsonBody(new { legajo, clave });

                // Realizar la solicitud de manera asíncrona
                var response = await client.ExecuteAsync<ApiResponse>(request);

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessful && response.Data != null && response.Data.Data != null)
                {
                    var loginResponse = response.Data.Data; // Aquí obtenemos el objeto `LoginResponse` dentro de "data"

                    // Almacenar el token y idPersona
                    Properties.Settings.Default.Token = loginResponse.Token;
                    Properties.Settings.Default.IdPersona = loginResponse.IdPersona;
                    Properties.Settings.Default.Save();

                    // Redirigir según el tipo de persona
                    switch (loginResponse.Tipo)
                    {
                        case "Alumno":
                            new HomeAlumno().Show();
                            this.Hide();
                            break;
                        case "Docente":
                            new HomeDocente().Show();
                            this.Hide();
                            break;
                        case "NoDocente":
                            new HomeNoDocente().Show();
                            this.Hide();
                            break;
                        default:
                            MessageBox.Show("Tipo de usuario no reconocido.");
                            break;
                    }
                }
                else
                {
                    // Mostrar el mensaje de error si la solicitud falló
                    MessageBox.Show("Error al iniciar sesion. Vuelve a intentarlo");
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier otro error, incluyendo excepciones internas
                if (ex.InnerException != null)
                {
                    MessageBox.Show($"Excepción general: {ex.Message}\nInner Exception: {ex.InnerException.Message}\nStack Trace:\n{ex.StackTrace}");
                }
                else
                {
                    MessageBox.Show($"Excepción general: {ex.Message}\nStack Trace:\n{ex.StackTrace}");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    // Esta clase ahora refleja la estructura de respuesta completa, incluyendo "data"
    public class ApiResponse
    {
        public LoginResponse Data { get; set; } = new LoginResponse();
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public int IdPersona { get; set; }
        public string Tipo { get; set; } = string.Empty;
    }
}
