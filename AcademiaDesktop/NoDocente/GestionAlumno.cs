using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace AcademiaDesktop.NoDocente
{
    public partial class GestionAlumno : Form
    {
        private int IdPer { get; set; }
        private RestClient client { get; set; }
        private string claveOriginal;

        public GestionAlumno()
        {
            InitializeComponent();
            // Forzar uso de TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Ignorar los errores de certificado SSL (solo para desarrollo)
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            IdPer = Properties.Settings.Default.IdPersona;

            var options = new RestClientOptions("https://localhost:7090")
            {
                RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true // Ignorar validación SSL
            };
            client = new RestClient(options);
        }

        private async void GestionAlumno_Load(object sender, EventArgs e)
        {
            await CargarAlumnos();
        }

        private async Task CargarAlumnos()
        {
            try
            {
                // Realizar la solicitud a la API
                var requestAlumnos = new RestRequest("api/Persona", Method.Get);
                var responseAlumnos = await client.ExecuteAsync(requestAlumnos);

                if (responseAlumnos.IsSuccessful)
                {
                    // Parsear la respuesta JSON
                    var jsonResponse = JObject.Parse(responseAlumnos.Content);
                    var personas = jsonResponse["data"]?.ToObject<List<JObject>>();

                    if (personas != null)
                    {
                        // Filtrar solo los alumnos
                        var alumnos = personas.Where(p => p["tipo"]?.ToString() == "Alumno").ToList();

                        // Limpiar la tabla antes de agregar datos
                        dgvAlumnos.Rows.Clear();

                        foreach (var alumno in alumnos)
                        {
                            // Obtener información de idPlan
                            int idPlan = 0;
                            string DescPlan = "Plan no asignado";
                            string Carrera = "Carrera no asignada";

                            if (int.TryParse(alumno["idPlan"]?.ToString(), out idPlan))
                            {
                                DescPlan = await ObtenerDescripcionPlan(idPlan);
                                Carrera = await ObtenerCarrera(idPlan);
                            }

                            // Agregar fila a la tabla
                            dgvAlumnos.Rows.Add(
                                alumno["idPersona"]?.ToString(),
                                alumno["nombre"]?.ToString(),
                                alumno["apellido"]?.ToString(),
                                alumno["direccion"]?.ToString(),
                                alumno["email"]?.ToString(),
                                alumno["telefono"]?.ToString(),
                                alumno["fechaNacimiento"]?.ToString(),  
                                alumno["legajo"]?.ToString(),
                                alumno["clave"]?.ToString(),
                                alumno["idPlan"]?.ToString(),
                                DescPlan,
                                Carrera
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron datos de alumnos.");
                    }
                }
                else
                {
                    MessageBox.Show($"Error al cargar los alumnos: {responseAlumnos.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los alumnos: {ex.Message}");
            }
        }

        // Método para obtener la descripción del plan
        private async Task<string> ObtenerDescripcionPlan(int idPlan)
        {
            try
            {
                var requestPlan = new RestRequest($"api/Plan/{idPlan}", Method.Get);
                var responsePlan = await client.ExecuteAsync(requestPlan);

                if (responsePlan.IsSuccessful)
                {
                    var jsonResponse = JObject.Parse(responsePlan.Content);
                    return jsonResponse["data"]?["descPlan"]?.ToString() ?? "Plan no encontrado";
                }
                return "Plan no encontrado";
            }
            catch
            {
                return "Error al obtener el plan";
            }
        }


        private async Task<string> ObtenerCarrera(int idPlan)
        {
            try
            {
                // Solicitud para obtener el plan
                var requestPlan = new RestRequest($"api/Plan/{idPlan}", Method.Get);
                var responsePlan = await client.ExecuteAsync(requestPlan);

                if (responsePlan.IsSuccessful)
                {
                    // Parsear la respuesta del plan
                    var jsonResponsePlan = JObject.Parse(responsePlan.Content);
                    var idEspecialidad = jsonResponsePlan["data"]?["idEspecialidad"]?.ToString();

                    if (!string.IsNullOrEmpty(idEspecialidad))
                    {
                        // Solicitud para obtener la especialidad
                        var requestEspecialidad = new RestRequest($"api/Especialidad/{idEspecialidad}", Method.Get);
                        var responseEspecialidad = await client.ExecuteAsync(requestEspecialidad);

                        if (responseEspecialidad.IsSuccessful)
                        {
                            // Parsear la respuesta de la especialidad
                            var jsonResponseEspecialidad = JObject.Parse(responseEspecialidad.Content);
                            return jsonResponseEspecialidad["data"]?["descEspecialidad"]?.ToString() ?? "Carrera no encontrada";
                        }
                        return "Carrera no encontrada";
                    }
                    return "ID de especialidad no encontrado en el plan";
                }
                return "Plan no encontrado";
            }
            catch (Exception ex)
            {
                return $"Error al obtener la especialidad: {ex.Message}";
            }
        }

        private async void Actualizar_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows.Count > 0)
            {
                var selectedRow = dgvAlumnos.SelectedRows[0];

                // Obtener y validar el idPersona
                if (!int.TryParse(selectedRow.Cells["idPersona"].Value?.ToString(), out int idPersona))
                {
                    MessageBox.Show("El ID de la persona no es válido.");
                    return;
                }

                string legajo = selectedRow.Cells["legajo"].Value?.ToString() ?? string.Empty;

                // Validar si la fecha de nacimiento y legajo son válidos
                if (!EsEdadValida(selectedRow.Cells["fechaNacimiento"].Value?.ToString()) ||
                    await ExisteLegajo(legajo, idPersona))
                {
                    return;
                }

                string claveIngresada = selectedRow.Cells["clave"].Value?.ToString() ?? string.Empty;
                string claveHash = claveIngresada == claveOriginal ? claveOriginal : HashClave(claveIngresada);

                // Obtener y validar el idPlan
                int? idPlan = null;
                string idPlanValue = selectedRow.Cells["idPlan"].Value?.ToString();

                if (!string.IsNullOrEmpty(idPlanValue) && int.TryParse(idPlanValue, out int parsedIdPlan))
                {
                    idPlan = parsedIdPlan;
                }
                else
                {
                    MessageBox.Show("IdPlan contiene un valor no válido o está vacío.");
                }

                // Crear objeto Alumno con los valores actualizados
                var alumnoActualizacion = new Alumno
                {
                    IdPersona = idPersona,
                    Nombre = selectedRow.Cells["nombre"].Value?.ToString() ?? string.Empty,
                    Apellido = selectedRow.Cells["apellido"].Value?.ToString() ?? string.Empty,
                    Direccion = selectedRow.Cells["direccion"].Value?.ToString() ?? string.Empty,
                    Email = selectedRow.Cells["email"].Value?.ToString() ?? string.Empty,
                    Telefono = selectedRow.Cells["telefono"].Value?.ToString() ?? string.Empty,
                    FechaNacimiento = DateTime.TryParse(selectedRow.Cells["fechaNacimiento"].Value?.ToString(), out DateTime fechaNacimiento) ? fechaNacimiento : DateTime.Now,
                    Tipo = "Alumno",
                    Legajo = legajo,
                    Clave = claveHash,
                    IdPlan = idPlan,
                    Plan = null  // Opcional: asignar un objeto Plan si se requiere.
                };

                // Realizar la solicitud PUT para actualizar el alumno
                var requestUpdate = new RestRequest($"api/Persona/{idPersona}", Method.Put);
                requestUpdate.AddJsonBody(alumnoActualizacion);

                var responseUpdate = await client.ExecuteAsync(requestUpdate);

                if (responseUpdate.IsSuccessful)
                {
                    MessageBox.Show("Alumno actualizado correctamente");
                    await CargarAlumnos();
                }
                else
                {
                    // Mostrar detalles del error
                    string errorMessage = responseUpdate.Content ?? "Error desconocido";
                    MessageBox.Show($"Error al actualizar el alumno: {errorMessage}");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione a un alumno para actualizar");
            }
        }

        private void dgvAlumnos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            FiltrarAlumnos();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarAlumnos();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            FiltrarAlumnos();
        }

        private void FiltrarAlumnos()
        {
            try
            {
                if (dgvAlumnos.IsCurrentCellInEditMode)
                {
                    dgvAlumnos.EndEdit(); // Confirmamos la edición
                    dgvAlumnos.CurrentCell = null; // Desvinculamos la celda actual para evitar conflictos
                }

                dgvAlumnos.ClearSelection();
                dgvAlumnos.SuspendLayout();

                string nombreFiltro = textBox1.Text.ToLower();
                bool filtrarIngenieriaSistemas = true;
                bool filtrarIngenieriaQuimica = true;

                foreach (DataGridViewRow row in dgvAlumnos.Rows)
                {
                    bool mostrarFila = true;

                    if (!string.IsNullOrWhiteSpace(nombreFiltro))
                    {
                        var nombreCell = row.Cells["nombre"].Value;
                        string nombreAlumno = nombreCell != null ? nombreCell.ToString().ToLower() : string.Empty;

                        if (!nombreAlumno.Contains(nombreFiltro))
                        {
                            mostrarFila = false;
                        }
                    }

                    if (mostrarFila && (filtrarIngenieriaSistemas || filtrarIngenieriaQuimica))
                    {
                        var carreraCell = row.Cells["carrera"].Value;
                        string carreraAlumno = carreraCell != null ? carreraCell.ToString().ToLower() : string.Empty;

                        if (filtrarIngenieriaSistemas && !carreraAlumno.Contains("Ingeniería en Sistemas"))
                        {
                            mostrarFila = false;
                        }
                        if (filtrarIngenieriaQuimica && !carreraAlumno.Contains("Ingeniería Química"))
                        {
                            mostrarFila = false;
                        }
                    }
                    row.Visible = mostrarFila;
                }
            }
            catch
            {
                // Ignorar el error y no mostrar ningún mensaje
            }
            finally
            {
                dgvAlumnos.ResumeLayout();
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
                    MessageBox.Show("El alumno debe ser mayor de 22 años.");
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

        private void button2_Click(object sender, EventArgs e)
        {
            HomeNoDocente homeNoDocente = new HomeNoDocente();
            homeNoDocente.Show();
            this.Hide();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string direccion = txtDireccion.Text.Trim();
                string email = txtEmail.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string legajo = txtLegajo.Text.Trim();
                string clave = txtClave.Text.Trim();
                string idPlan = txtIdPlan.Text.Trim();
                int idPlanInt = int.Parse(idPlan);
                DateTime fechaNacimiento = dtpFechaNacimiento.Value;

                int edad = DateTime.Now.Year - fechaNacimiento.Year;
                if (fechaNacimiento > DateTime.Now.AddYears(-edad)) edad--;
                if (edad < 18)
                {
                    MessageBox.Show("El alumno debe tener al menos 18 años.");
                    return;
                }

                if (await ExisteLegajo(legajo, 0))
                {
                    MessageBox.Show("Por favor, ingrese un legajo único.");
                    return;
                }

                string claveHash = HashClave(clave);

                var nuevoAlumno = new Alumno
                {
                    IdPersona = 0,
                    Nombre = nombre,
                    Apellido = apellido,
                    Direccion = direccion,
                    Email = email,
                    Telefono = telefono,
                    FechaNacimiento = fechaNacimiento,
                    Tipo = "Alumno",
                    Legajo = legajo,
                    Clave = claveHash,
                    IdPlan = idPlanInt,
                    Plan = null
                };

                var requestPost = new RestRequest("api/Persona", Method.Post);
                requestPost.AddJsonBody(nuevoAlumno);
                var responsePost = await client.ExecuteAsync(requestPost);

                if (responsePost.IsSuccessful)
                {
                    MessageBox.Show("alumno agregado correctamente.");
                    await CargarAlumnos();
                }
                else
                {
                    MessageBox.Show("Error al agregar el alumno.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el alumno: {ex.Message}");
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

        private void txtLegajo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private class Alumno
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

        private async void button4_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows.Count > 0)
            {
                var selectedRow = dgvAlumnos.SelectedRows[0];
                int idPersona = int.Parse(selectedRow.Cells["idPersona"].Value.ToString());

                var requestDelete = new RestRequest($"api/Persona/{idPersona}", Method.Delete);
                var responseDelete = await client.ExecuteAsync(requestDelete);

                if (responseDelete.IsSuccessful)
                {
                    MessageBox.Show("Alumno eliminado correctamente");
                    await CargarAlumnos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el alumno");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione a un alumno para eliminarlo");
            }
        }

 
    }

}

 