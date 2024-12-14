using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AcademiaDesktop.NoDocente
{
    public partial class GestionDocente : Form
    {
        private int IdPersona { get; set; }
        private RestClient client { get; set; }
        private string claveOriginal; // Almacenar la clave original

        public GestionDocente()
        {
            InitializeComponent();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            IdPersona = Properties.Settings.Default.IdPersona;

            var options = new RestClientOptions("https://localhost:7090")
            {
                RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            client = new RestClient(options);
        }

        private async void GestionDocente_Load(object sender, EventArgs e)
        {
            await CargarDocentes();
        }

        private async Task CargarDocentes()
        {
            try
            {
                var requestDocentes = new RestRequest("api/Persona", Method.Get);
                var responseDocentes = await client.ExecuteAsync(requestDocentes);

                if (responseDocentes.IsSuccessful)
                {
                    var jsonResponseDocentes = JObject.Parse(responseDocentes.Content);
                    var docentesArray = jsonResponseDocentes["data"] as JArray;

                    if (docentesArray != null)
                    {
                        dgvDocentes.Rows.Clear();

                        foreach (var persona in docentesArray)
                        {
                            if (persona["tipo"]?.ToString() == "Docente")
                            {
                                dgvDocentes.Rows.Add(persona["idPersona"]?.ToString(),
                                                     persona["nombre"]?.ToString(),
                                                     persona["apellido"]?.ToString(),
                                                     persona["direccion"]?.ToString(),
                                                     persona["email"]?.ToString(),
                                                     persona["telefono"]?.ToString(),
                                                     persona["fechaNacimiento"]?.ToString(),
                                                     persona["tipo"]?.ToString(),
                                                     persona["legajo"]?.ToString(),
                                                     persona["clave"]?.ToString());

                                claveOriginal = persona["clave"]?.ToString(); // Guardar la clave original
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("La respuesta no contiene un array de docentes.");
                    }
                }
                else
                {
                    MessageBox.Show($"Error al cargar los docentes: {responseDocentes.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los docentes: {ex.Message}");
            }
        }

        private async void buttonPut_Click(object sender, EventArgs e)
        {
            if (dgvDocentes.SelectedRows.Count > 0)
            {
                var selectedRow = dgvDocentes.SelectedRows[0];
                int idPersona = int.Parse(selectedRow.Cells["idPersona"].Value.ToString());
                string legajo = selectedRow.Cells["legajo"].Value?.ToString() ?? string.Empty;

                // Validar si la fecha de nacimiento y legajo son válidos
                if (!EsEdadValida(selectedRow.Cells["fechaNacimiento"].Value?.ToString()) ||
                    await ExisteLegajo(legajo, idPersona))
                {
                    return;
                }

                string claveIngresada = selectedRow.Cells["clave"].Value?.ToString() ?? string.Empty;
                string claveHash = claveIngresada == claveOriginal ? claveOriginal : HashClave(claveIngresada);

                var docenteActualizacion = new Docente
                {
                    IdPersona = idPersona,
                    Nombre = selectedRow.Cells["nombre"].Value?.ToString() ?? string.Empty,
                    Apellido = selectedRow.Cells["apellido"].Value?.ToString() ?? string.Empty,
                    Direccion = selectedRow.Cells["direccion"].Value?.ToString() ?? string.Empty,
                    Email = selectedRow.Cells["email"].Value?.ToString() ?? string.Empty,
                    Telefono = selectedRow.Cells["telefono"].Value?.ToString() ?? string.Empty,
                    FechaNacimiento = DateTime.Parse(selectedRow.Cells["fechaNacimiento"].Value?.ToString() ?? DateTime.Now.ToString()),
                    Tipo = "Docente",
                    Legajo = legajo,
                    Clave = claveHash,
                    IdPlan = null,
                    Plan = null
                };

                var requestUpdate = new RestRequest($"api/Persona/{idPersona}", Method.Put);
                requestUpdate.AddJsonBody(docenteActualizacion);

                var responseUpdate = await client.ExecuteAsync(requestUpdate);

                if (responseUpdate.IsSuccessful)
                {
                    MessageBox.Show("Docente actualizado correctamente");
                    await CargarDocentes();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el docente");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione a un docente para actualizar");
            }
        }

        private string HashClave(string clave)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(clave));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool EsEdadValida(string fechaNacimientoStr)
        {
            if (DateTime.TryParse(fechaNacimientoStr, out DateTime fechaNacimiento))
            {
                int edad = DateTime.Now.Year - fechaNacimiento.Year;
                if (fechaNacimiento > DateTime.Now.AddYears(-edad)) edad--;

                if (edad < 22)
                {
                    MessageBox.Show("El docente debe ser mayor de 22 años.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Fecha de nacimiento inválida.");
                return false;
            }
            return true;
        }

        private async Task<bool> ExisteLegajo(string legajo, int idPersonaActual)
        {
            var request = new RestRequest("api/Persona", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var jsonResponse = JObject.Parse(response.Content);
                var personasArray = jsonResponse["data"] as JArray;

                if (personasArray != null && personasArray.Any(persona =>
                        persona["legajo"]?.ToString() == legajo &&
                        (int)persona["idPersona"] != idPersonaActual))
                {
                    MessageBox.Show("El legajo ya existe. Debe ser único.");
                    return true;
                }
            }
            else
            {
                MessageBox.Show($"Error al verificar el legajo: {response.ErrorMessage}");
            }
            return false;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de los campos de texto
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string email = txtEmail.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string legajo = txtLegajo.Text.Trim();
                string clave = txtClave.Text.Trim();
                DateTime fechaNacimiento = dtpFechaNacimiento.Value;

                // Validación de edad mínima
                int edad = DateTime.Now.Year - fechaNacimiento.Year;
                if (fechaNacimiento > DateTime.Now.AddYears(-edad)) edad--;
                if (edad < 22)
                {
                    MessageBox.Show("El docente debe tener al menos 22 años.");
                    return;
                }

                // Verificación de que el legajo no exista ya
                if (await ExisteLegajo(legajo, 0))
                {
                    MessageBox.Show("Por favor, ingrese un legajo único.");
                    return;
                }

                // Hash de la clave
                string claveHash = HashClave(clave);

                // Crear objeto Docente
                var nuevoDocente = new Docente
                {
                    IdPersona = 0,
                    Nombre = nombre,
                    Apellido = apellido,
                    Direccion = direccion,
                    Email = email,
                    Telefono = telefono,
                    FechaNacimiento = fechaNacimiento,
                    Tipo = "Docente",
                    Legajo = legajo,
                    Clave = claveHash,
                    IdPlan = null,
                    Plan = null
                };

                // Enviar el POST request para crear el docente
                var requestPost = new RestRequest("api/Persona", Method.Post);
                requestPost.AddJsonBody(nuevoDocente);
                var responsePost = await client.ExecuteAsync(requestPost);

                if (responsePost.IsSuccessful)
                {
                    MessageBox.Show("Docente agregado correctamente.");
                    await CargarDocentes();  
                }
                else
                {
                    MessageBox.Show("Error al agregar el docente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el docente: {ex.Message}");
            }
        }



        private class Docente
        {
            public int IdPersona { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Apellido { get; set; } = string.Empty;
            public string Direccion { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Telefono { get; set; } = string.Empty;
            public DateTime FechaNacimiento { get; set; }
            public string Tipo { get; set; } = string.Empty;
            public string Legajo { get; set; } = string.Empty;
            public string Clave { get; set; } = string.Empty;
            public int? IdPlan { get; set; }
            public object Plan { get; set; } = null!;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HomeNoDocente home = new HomeNoDocente();
            home.Show();
            this.Hide();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (dgvDocentes.SelectedRows.Count > 0)
            {
                var selectedRow = dgvDocentes.SelectedRows[0];
                int idPersona = int.Parse(selectedRow.Cells["idPersona"].Value.ToString());

                var requestDelete = new RestRequest($"api/Persona/{idPersona}", Method.Delete);
                var responseDelete = await client.ExecuteAsync(requestDelete);

                if (responseDelete.IsSuccessful)
                {
                    MessageBox.Show("Docente eliminado correctamente");
                    await CargarDocentes();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el docente");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione a un docente para eliminarlo");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FiltrarDocentes();
        }

        private void FiltrarDocentes()
        {
            string nombreFiltro = textBox1.Text.ToLower();

            foreach (DataGridViewRow row in dgvDocentes.Rows)
            {
                var nombreDocente = row.Cells["nombre"].Value?.ToString().ToLower() ?? string.Empty;
                row.Visible = string.IsNullOrWhiteSpace(nombreFiltro) || nombreDocente.Contains(nombreFiltro);
            }
        }

        private void dgvDocentes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
