using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AcademiaDesktop.Docente
{
    public partial class CondicionAlumnos : Form
    {
        private int IdPer { get; set; }
        private RestClient client;

        public CondicionAlumnos()
        {
            InitializeComponent();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var options = new RestClientOptions("https://localhost:7090")
            {
                RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };
            client = new RestClient(options);
        }

        private async void CondicionAlumnos_Load(object sender, EventArgs e)
        {
            IdPer = Properties.Settings.Default.IdPersona;
            await AgregarDatos();
        }

        private async Task AgregarDatos()
        {
            try
            {
                // Obtener los IdCurso a los que está inscrita la persona
                var requestCursos = new RestRequest($"api/DocenteCurso/Persona/cursos/{IdPer}", Method.Get);
                var responseCursos = await client.ExecuteAsync<List<int>>(requestCursos);
                var arrayCursos = responseCursos.Data;

                if (arrayCursos == null || arrayCursos.Count == 0)
                {
                    MessageBox.Show("No se encontraron cursos para la persona.");
                    return;
                }

                // Obtener las inscripciones correspondientes a los cursos
                var requestInscripciones = new RestRequest("api/AlumnoInscripcion/CursosArray", Method.Post);
                requestInscripciones.AddJsonBody(arrayCursos);
                var responseInscripciones = await client.ExecuteAsync<List<AlumnoInscripcion>>(requestInscripciones);

                if (responseInscripciones.Data != null && responseInscripciones.Data.Count > 0)
                {
                    if (dataGridView1.Columns.Count == 0)
                    {
                        dataGridView1.Columns.Add("IdInscripcion", "ID Inscripción");
                        dataGridView1.Columns.Add("FechaInscripcion", "Fecha Inscripción");
                        dataGridView1.Columns.Add("Nota", "Nota");
                        dataGridView1.Columns.Add("Condicion", "Condición");
                        dataGridView1.Columns.Add("Alumno", "Alumno");
                        dataGridView1.Columns.Add("Materia", "Materia");
                        dataGridView1.Columns.Add("Comision", "Comisión");
                    }

                    // Limpiar filas existentes antes de agregar nuevas
                    dataGridView1.Rows.Clear();

                    foreach (var inscripcion in responseInscripciones.Data)
                    {
                        // Obtener los datos del alumno con encabezados adicionales
                        var alumnoRequest = new RestRequest($"api/Persona/{inscripcion.IdPersona}", Method.Get);
                        alumnoRequest.AddHeader("accept", "*/*");

                        var responseAlumno = await client.ExecuteAsync(alumnoRequest);

                        if (!responseAlumno.IsSuccessful)
                        {
                            MessageBox.Show($"Error al obtener datos del alumno con ID: {inscripcion.IdPersona}");
                            continue;
                        }

                        // Extraer datos del alumno de la respuesta JSON
                        var jsonResponse = JObject.Parse(responseAlumno.Content);
                        var data = jsonResponse["data"];
                        string nombre = data["nombre"].ToString();
                        string apellido = data["apellido"].ToString();
                        string nombreAlumno = $"{nombre} {apellido}";

                        // Obtener los datos del curso (materia y comisión)
                        var cursoRequest = new RestRequest($"api/Curso/{inscripcion.IdCurso}", Method.Get);
                        var responseCurso = await client.ExecuteAsync<Curso>(cursoRequest);
                        var cursoData = responseCurso.Data;

                        if (cursoData == null)
                        {
                            MessageBox.Show($"No se encontraron datos para el curso con ID: {inscripcion.IdCurso}");
                            continue;
                        }

                        var materiaData = cursoData.Materia;
                        var comisionData = cursoData.Comision;

                        // Agregar la fila al DataGridView
                        var rowIndex = dataGridView1.Rows.Add(
                            inscripcion.IdInscripcion,
                            inscripcion.FechaInscripcion.ToString("dd/MM/yyyy"),
                            inscripcion.Nota,
                            inscripcion.Condicion,
                            nombreAlumno,
                            materiaData?.DescMateria,
                            comisionData?.DescComision
                        );

                        // Guardar el objeto AlumnoInscripcion en la propiedad Tag de la fila
                        dataGridView1.Rows[rowIndex].Tag = inscripcion;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron inscripciones para los cursos proporcionados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            // Verificar que hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el objeto AlumnoInscripcion de la fila seleccionada
                var selectedRow = dataGridView1.SelectedRows[0];
                var inscripcion = selectedRow.Tag as AlumnoInscripcion;

                // Verifica  que el objeto inscripcion no sea null
                if (inscripcion != null)
                {
                 
                    var inscripcionActualizada = new AlumnoInscripcion
                    {
                        IdInscripcion = inscripcion.IdInscripcion,
                        FechaInscripcion = inscripcion.FechaInscripcion,  
                        Condicion = selectedRow.Cells["Condicion"].Value?.ToString(),
                        Nota = selectedRow.Cells["Nota"].Value?.ToString(),
                        IdPersona = inscripcion.IdPersona,  
                        IdCurso = inscripcion.IdCurso,      
                        Persona = null,                    
                        Curso = null                       
                    };
 
                    var requestUpdate = new RestRequest($"api/AlumnoInscripcion/{inscripcionActualizada.IdInscripcion}", Method.Put);
                    requestUpdate.AddJsonBody(inscripcionActualizada);

                    var responseUpdate = await client.ExecuteAsync(requestUpdate);

                    if (responseUpdate.IsSuccessful)
                    {
                        MessageBox.Show("Inscripción actualizada correctamente.");
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar la inscripción.");
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo encontrar la inscripción seleccionada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una inscripción para actualizar.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            HomeDocente home = new HomeDocente();
            home.Show();
            this.Hide();
        }
    }

    public class AlumnoInscripcion
    {
        public int IdInscripcion { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string Condicion { get; set; } = string.Empty;
        public string Nota { get; set; } = string.Empty;
        public int IdPersona { get; set; }
        public int IdCurso { get; set; }
        public object Persona { get; set; }
        public object Curso { get; set; }
    }

    public class Persona
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
    }

    public class Curso
    {
        public Materia Materia { get; set; } = new Materia();
        public Comision Comision { get; set; } = new Comision();
    }
     
    public class Materia
    {
        public string DescMateria { get; set; } = string.Empty;
    }

    public class Comision
    {
        public string DescComision { get; set; } = string.Empty;
    }
}
