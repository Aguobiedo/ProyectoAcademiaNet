using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiAcademiaNet.Models
{
    public class DocenteCurso
    {
        [Key]
        public int IdDictado { get; set; }

        [StringLength(255)]
        public string Cargo { get; set; } = string.Empty;

        [ForeignKey("Curso")]
        [Required]
        public int IdCurso { get; set; }

        [ForeignKey("Persona")]
        [Required]
        public int IdPersona { get; set; }

        public Curso? Curso { get; set; } = new Curso();

        public Persona? Persona { get; set; } = new Persona();
    }
}
