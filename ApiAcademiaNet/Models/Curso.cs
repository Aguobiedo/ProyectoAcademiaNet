using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAcademiaNet.Models
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }

        [ForeignKey("Materia")]
        [Required]
        public int IdMateria { get; set; }

        [ForeignKey("Comision")]
        [Required]
        public int IdComision { get; set; }

        public int AnioCalendario { get; set; }
        public int cupo { get; set; }

        public Materia? Materia { get; set; } = new Materia();
        public Comision? Comision { get; set; } = new Comision();

        [JsonIgnore]
        public ICollection<AlumnoInscripcion> AlumnoInscripcion { get; set; } = new List<AlumnoInscripcion>();

        [JsonIgnore]
        public ICollection<DocenteCurso> DocenteCurso { get; set; } = new List<DocenteCurso>();
    }
}
