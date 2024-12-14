using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAcademiaNet.Models
{
    public class AlumnoInscripcion
    {
        [Key]
        public int IdInscripcion { get; set; }

        public DateTime FechaInscripcion { get; set; }

        [StringLength(50)]
        public string condicion { get; set; } = string.Empty;

        [StringLength(50)]
        public string nota { get; set; } = string.Empty;

        [ForeignKey("Persona")]
        [Required]
        public int IdPersona { get; set; }

        [ForeignKey("Curso")]
        [Required]
        public int IdCurso { get; set; }

        public Persona? Persona { get; set; } = new Persona();
        public Curso? Curso { get; set; } = new Curso();
    }
}