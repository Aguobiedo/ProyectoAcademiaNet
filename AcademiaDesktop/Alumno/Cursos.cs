using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

namespace AcademiaDesktop.Alumno
{
    public partial class Cursos : Form
    {
        private int IdPersona { get; set; }
        private RestClient client;

        public Cursos()
        {
            InitializeComponent();
            IdPersona = Properties.Settings.Default.IdPersona;

            // Forzar uso de TLS 1.2 y configuración del cliente
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var options = new RestClientOptions("https://localhost:7090")
            {
                RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            client = new RestClient(options);
        }

        private async void Cursos_Load(object sender, EventArgs e)
        {
            await CargarInscripciones();
        }

        private async Task CargarInscripciones()
        {
            var request = new RestRequest($"/api/AlumnoInscripcion/persona/{IdPersona}", Method.Get);
            var response = await client.ExecuteAsync<List<InscripcionCursoPersona>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                var inscripcionesSimplificadas = new List<InscripcionSimplificada>();

                foreach (var inscripcion in response.Data)
                {
                    // Verificación de datos de curso
                    var cursoRequest = new RestRequest($"api/Curso/{inscripcion.IdCurso}", Method.Get);
                    var responseCurso = await client.ExecuteAsync<CursoInscripcion>(cursoRequest);

                    if (!responseCurso.IsSuccessful || responseCurso.Data == null)
                    {
                        MessageBox.Show($"No se encontraron datos para el curso con ID: {inscripcion.IdCurso}");
                        continue;
                    }

                    var cursoData = responseCurso.Data;
                    var materiaData = cursoData.Materia ?? new MateriaCurso();
                    var comisionData = cursoData.Comision ?? new ComisionCurso();

                    inscripcionesSimplificadas.Add(new InscripcionSimplificada
                    {
                        IdInscripcion = inscripcion.IdInscripcion,
                        FechaInscripcion = inscripcion.FechaInscripcion,
                        Condicion = inscripcion.Condicion,
                        Nota = inscripcion.Nota,
                        Materia = materiaData.DescMateria ?? "Sin asignar",
                        Comision = comisionData.DescComision ?? "Sin asignar",
                        Persona = $"{inscripcion.Persona?.Nombre ?? "N/A"} {inscripcion.Persona?.Apellido ?? "N/A"}"
                    });
                }

                dgvInscripciones.DataSource = inscripcionesSimplificadas;
            }
            else
            {
                MessageBox.Show("Error al cargar inscripciones: " + response.ErrorMessage);
            }
        }

        private async void btnEliminarInscripcion_Click(object sender, EventArgs e)
        {
            if (dgvInscripciones.SelectedRows.Count > 0)
            {
                var idInscripcion = (int)dgvInscripciones.SelectedRows[0].Cells["IdInscripcion"].Value;

                var request = new RestRequest($"/api/AlumnoInscripcion/{idInscripcion}", Method.Delete);
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show("Inscripción eliminada con éxito.");
                    await CargarInscripciones();
                }
                else
                {
                    MessageBox.Show($"Error al eliminar la inscripción: {response.ErrorMessage}");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una inscripción para eliminar.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InscripcionCurso inscripcionCurso = new InscripcionCurso();
            inscripcionCurso.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HomeAlumno home = new HomeAlumno();
            home.Show();
            this.Hide();
        }

        private void dgvInscripciones_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

    }

    // Clase para mapear solo los campos necesarios
    public class InscripcionSimplificada
    {
        public int IdInscripcion { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string Condicion { get; set; }
        public string Nota { get; set; }
        public string Materia { get; set; }
        public string Comision { get; set; }
        public string Persona { get; set; }
    }

    // Clases de mapeo según respuesta del API
    public class InscripcionCursoPersona
    {
        public int IdInscripcion { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string Condicion { get; set; }
        public string Nota { get; set; }
        public int IdPersona { get; set; }
        public int IdCurso { get; set; }
        public PersonaInscripcion Persona { get; set; }
        public CursoInscripcion Curso { get; set; }
    }

    public class PersonaInscripcion
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    public class CursoInscripcion
    {
        public int IdCurso { get; set; }
        public MateriaCurso Materia { get; set; }
        public ComisionCurso Comision { get; set; }
    }

    public class MateriaCurso
    {
        public int IdMateria { get; set; }
        public string DescMateria { get; set; }
    }

    public class ComisionCurso
    {
        public int IdComision { get; set; }
        public string DescComision { get; set; }
    }
}
