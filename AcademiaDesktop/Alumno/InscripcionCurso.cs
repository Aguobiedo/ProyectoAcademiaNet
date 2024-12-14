using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace AcademiaDesktop.Alumno
{
    public partial class InscripcionCurso : Form
    {
        private int IdPlan { get; set; }
        private int IdPersona { get; set; }
        private RestClient client;

        public InscripcionCurso()
        {
            InitializeComponent();
            IdPlan = Properties.Settings.Default.IdPlan;
            IdPersona = Properties.Settings.Default.IdPersona;

            // Forzar uso de TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Ignorar los errores de certificado SSL (solo para desarrollo)
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            // Configurar el cliente RestSharp con opción para ignorar validación SSL
            var options = new RestClientOptions("https://localhost:7090")
            {
                RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            client = new RestClient(options);
        }

        private async void InscripcionCurso_Load(object sender, EventArgs e)
        {
            await CargarCursos();
        }

        private async Task CargarCursos()
        {
            try
            {
                var request = new RestRequest($"api/Curso/plan/{IdPlan}", Method.Get);
                var response = await client.ExecuteAsync<List<Curso>>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                     
                    var cursosDisplay = response.Data.Select(curso => new
                    {
                        curso.idCurso,
                        DisplayInfo = $"{curso.materia.descMateria} - Comisión {curso.comision.descComision}"
                    }).ToList();

                    // Asignar la lista al comboBox
                    cboCursos.DataSource = cursosDisplay;
                    cboCursos.DisplayMember = "DisplayInfo";  
                    cboCursos.ValueMember = "idCurso";   
                }
                else
                {
                    MessageBox.Show($"Error al cargar cursos: {response.ErrorMessage}\n{response.Content}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cursos: {ex.Message}");
            }
        }


        private async void btnGuardarInscripcion_Click(object sender, EventArgs e)
        {
            if (cboCursos.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un curso.");
                return;
            }

            // Confirmación antes de guardar
            DialogResult result = MessageBox.Show("¿Está seguro de que desea inscribirse en este curso?", "Confirmación", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
            {
                return;
            }

            
            int idCursoSeleccionado = (int)cboCursos.SelectedValue;

            try
            {
                // obtener las inscripciones existentes de la persona
                var requestGet = new RestRequest($"api/AlumnoInscripcion/Persona/{IdPersona}", Method.Get);
                var responseGet = await client.ExecuteAsync<List<Inscripcion>>(requestGet);

                if (responseGet.IsSuccessful && responseGet.Data != null)
                {
                     
                    bool cursoYaInscrito = responseGet.Data.Any(i => i.idCurso == idCursoSeleccionado);
                    if (cursoYaInscrito)
                    {
                        MessageBox.Show("Ya está inscrito en este curso.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"Error al obtener las inscripciones: {responseGet.ErrorMessage}\n{responseGet.Content}");
                    return;
                }

                // Crear la nueva inscripción
                var inscripcion = new Inscripcion
                {
                    idInscripcion = 0,
                    fechaInscripcion = DateTime.UtcNow,
                    condicion = "Inscripto",
                    nota = "Pendiente",
                    idPersona = IdPersona,
                    idCurso = idCursoSeleccionado,
                    persona = null,
                    curso = null
                };

                var requestPost = new RestRequest("api/AlumnoInscripcion", Method.Post)
                    .AddJsonBody(inscripcion);
                var responsePost = await client.ExecuteAsync(requestPost);

                if (responsePost.IsSuccessful)
                {
                    MessageBox.Show("Inscripción guardada con éxito.");

                    // Actualizar la lista de cursos después de la inscripción
                    await CargarCursos();
                }
                else
                {
                    MessageBox.Show($"Error al guardar la inscripción: {responsePost.ErrorMessage}\n{responsePost.Content}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la inscripción: {ex.Message}");
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Cursos curso = new Cursos();
            curso.Show();
            this.Hide();
        }

        private void cboCursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Método para manejar cambios de selección si es necesario
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Cursos curso = new Cursos();
            curso.Show();
            this.Hide();
        }
    }

    public class Materia
    {
        public int idMateria { get; set; }
        public string descMateria { get; set; } = string.Empty;
        public int hsSemanales { get; set; }
        public int hsTotales { get; set; }
        public int idPlan { get; set; }
    }

    public class Comision
    {
        public int idComision { get; set; }
        public string descComision { get; set; } = string.Empty;
        public int idPlan { get; set; }
    }

    public class Inscripcion
    {
        public int idInscripcion { get; set; }
        public DateTime fechaInscripcion { get; set; }
        public string condicion { get; set; } = string.Empty;
        public string nota { get; set; } = string.Empty;
        public int idPersona { get; set; }
        public int idCurso { get; set; }
        public object persona { get; set; } = null; // Cambiar a tipo correcto si es necesario
        public object curso { get; set; } = null; // Cambiar a tipo correcto si es necesario
    }

    public class Curso
    {
        public int idCurso { get; set; }
        public int idMateria { get; set; }
        public int idComision { get; set; }
        public int anioCalendario { get; set; }
        public int cupo { get; set; }
        public Materia materia { get; set; } = null; // Tipo correcto para propiedades detalladas
        public Comision comision { get; set; } = null; // Tipo correcto para propiedades detalladas
    }
}
