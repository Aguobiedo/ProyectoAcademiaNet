using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAcademiaNet.Models
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; } = string.Empty;

        [StringLength(255)]
        public string Direccion { get; set; } = string.Empty;

        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefono { get; set; }

        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(20)]
        public string Tipo {  get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Legajo { get; set; } = string.Empty;
        [Required]
        [StringLength(256)]
        public string Clave { get; set; } = string.Empty;

        [ForeignKey("Plan")]
        public int? IdPlan { get; set; }
 
        public Plan? Plan { get; set; } = new Plan();

        [JsonIgnore]
        public ICollection<AlumnoInscripcion> AlumnoInscripcion { get; set; } = new List<AlumnoInscripcion>();
        [JsonIgnore]
        public ICollection<DocenteCurso> DocenteCurso { get; set; } = new List<DocenteCurso>();
    }

}

