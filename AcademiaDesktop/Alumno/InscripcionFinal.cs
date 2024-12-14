using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Newtonsoft.Json.Linq;

namespace AcademiaDesktop.Alumno
{
    public partial class InscripcionFinal : Form
    {
        private int IdPersona { get; set; }
        private RestClient client { get; set; }

        public InscripcionFinal()
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

        private async void InscripcionFinal_Load(object sender, EventArgs e)
        {
            await CargarInscripciones();
        }

        private async Task CargarInscripciones()
        {
            var request = new RestRequest($"/api/AlumnoInscripcion/persona/{IdPersona}", Method.Get);
            var response = await client.ExecuteAsync<List<InscripcionCursoPersona>>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                // Agregar las columnas al DataGridView solo una vez
                if (dataGridView1.Columns.Count == 0)
                {
                    dataGridView1.Columns.Add("Materia", "Materia");
                    dataGridView1.Columns.Add("Comision", "Comisión");
                    dataGridView1.Columns.Add("Condicion", "Condición");
                }

                dataGridView1.Rows.Clear(); // Limpiar filas previas

                bool tieneRegular = false; // Bandera para verificar si hay inscripciones en "Regular"

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

                    if (inscripcion.Condicion == "Regular")
                    {
                        var rowIndex = dataGridView1.Rows.Add(
                            materiaData.DescMateria ?? "Sin asignar",
                            comisionData.DescComision ?? "Sin asignar",
                            inscripcion.Condicion
                        );

                        // Asociar el objeto de inscripción con la fila
                        dataGridView1.Rows[rowIndex].Tag = inscripcion;

                        tieneRegular = true; // Si encuentra una inscripción "Regular", actualizar la bandera
                    }
                }

                if (!tieneRegular)
                {
                    MessageBox.Show("No tienes ninguna materia en condición 'Regular'");
                }
            }
            else
            {
                MessageBox.Show("Error al cargar inscripciones: " + response.ErrorMessage);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Verificar que hay una fila seleccionada
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener el objeto AlumnoInscripcion de la fila seleccionada
                var selectedRow = dataGridView1.SelectedRows[0];
                var inscripcion = selectedRow.Tag as InscripcionCursoPersona;

                if (inscripcion != null)
                {
                    var inscripcionActualizada = new InscripcionCursoPersona
                    {
                        IdInscripcion = inscripcion.IdInscripcion,
                        FechaInscripcion = inscripcion.FechaInscripcion,
                        Condicion = "Inscripto en final",
                        Nota = "Pendiente",
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
                        MessageBox.Show("Inscripto en el examen final. Descargando comprobante...");

                        // Obtener los datos del alumno con encabezados adicionales
                        var alumnoRequest = new RestRequest($"api/Persona/{inscripcion.IdPersona}", Method.Get);
                        alumnoRequest.AddHeader("accept", "*/*");
                        var responseAlumno = await client.ExecuteAsync(alumnoRequest);

                        var jsonResponse = JObject.Parse(responseAlumno.Content);
                        var data = jsonResponse["data"];
                        string nombre = data["nombre"].ToString();
                        string apellido = data["apellido"].ToString();
                        DateTime fechaHoy = DateTime.Now;

                        // Obtener los datos de la materia y comisión de la fila seleccionada
                        var materia = selectedRow.Cells["Materia"].Value?.ToString() ?? "Sin asignar";
                        var comision = selectedRow.Cells["Comision"].Value?.ToString() ?? "Sin asignar";

                        // Llamamos al método para descargar el comprobante
                        DescargarComprobante(inscripcion, nombre, apellido, fechaHoy, materia, comision);

                        await CargarInscripciones();
                    }
                    else
                    {
                        MessageBox.Show("Error al inscribirse");
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

        private void button2_Click(object sender, EventArgs e)
        {
            HomeAlumno homeAlumno = new HomeAlumno();
            homeAlumno.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Método privado para descargar el comprobante en formato PDF
        private void DescargarComprobante(InscripcionCursoPersona inscripcion, string nombre, string apellido, DateTime fechaHoy, string nombreMateria, string comision)
        {
            try
            {
                // Crear el documento PDF
                Document doc = new Document(PageSize.A4, 50, 50, 25, 25);
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ComprobanteInscripcionExamenFinal.pdf");
                PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

                doc.Open();

                // Estilos de fuente
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLUE);
                var subtitleFont = FontFactory.GetFont(FontFactory.HELVETICA, 14, BaseColor.BLACK);
                var regularFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);

                // Agregar título centrado
                Paragraph title = new Paragraph("Comprobante de Inscripción a Examen Final", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                doc.Add(title);

                // Agregar datos en formato de tabla
                PdfPTable table = new PdfPTable(2) { WidthPercentage = 80, SpacingBefore = 10f };
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                table.SetWidths(new float[] { 2, 3 });

                // Encabezados de la tabla
                table.AddCell(new PdfPCell(new Phrase("Dato", subtitleFont)) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });
                table.AddCell(new PdfPCell(new Phrase("Descripción", subtitleFont)) { BackgroundColor = BaseColor.LIGHT_GRAY, HorizontalAlignment = Element.ALIGN_CENTER });

                // Agregar datos
                table.AddCell(new PdfPCell(new Phrase("Materia", regularFont)));
                table.AddCell(new PdfPCell(new Phrase(nombreMateria, regularFont)));

                table.AddCell(new PdfPCell(new Phrase("Comisión", regularFont)));
                table.AddCell(new PdfPCell(new Phrase(comision, regularFont)));

                table.AddCell(new PdfPCell(new Phrase("Condición", regularFont)));
                table.AddCell(new PdfPCell(new Phrase("Inscripto en final", regularFont)));

                table.AddCell(new PdfPCell(new Phrase("Fecha de Inscripción", regularFont)));
                table.AddCell(new PdfPCell(new Phrase(fechaHoy.ToString("dd/MM/yyyy"), regularFont)));

                table.AddCell(new PdfPCell(new Phrase("Persona", regularFont)));
                table.AddCell(new PdfPCell(new Phrase($"{nombre} {apellido}", regularFont)));

                doc.Add(table);

                // Pie de página
                Paragraph footer = new Paragraph("Este comprobante fue generado automáticamente por el sistema.", regularFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 20f
                };
                doc.Add(footer);

                doc.Close();

                MessageBox.Show($"Comprobante generado exitosamente. Se guardó en: {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el comprobante: {ex.Message}");
            }
        }

        private class InscripcionCursoPersona
        {
            public int IdInscripcion { get; set; }
            public DateTime FechaInscripcion { get; set; }
            public string Condicion { get; set; }
            public string Nota { get; set; }
            public int IdPersona { get; set; }
            public int IdCurso { get; set; }
            public CursoInscripcion Curso { get; set; }
            public Persona Persona { get; set; }
        }

        private class CursoInscripcion
        {
            public int IdCurso { get; set; }
            public MateriaCurso Materia { get; set; }
            public ComisionCurso Comision { get; set; }
        }

        private class MateriaCurso
        {
            public string DescMateria { get; set; }
        }

        private class ComisionCurso
        {
            public string DescComision { get; set; }
        }

        private class Persona
        {
            public string Nombre { get; set; } = string.Empty;
            public string Apellido { get; set; } = string.Empty;
        }
    }
}