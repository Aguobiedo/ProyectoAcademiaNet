using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiAcademiaNet.Models
{
    public class Materia
    {
        [Key]
        public int IdMateria { get; set; }

        [StringLength(255)]
        public string DescMateria { get; set; }

        public int HsSemanales { get; set; }
        public int HsTotales { get; set; }

        [ForeignKey("Plan")]
        [Required]
        public int IdPlan { get; set; }

        public Plan? Plan { get; set; }

        [JsonIgnore]
        public ICollection<Curso> Curso { get; set; } = new List<Curso>();
    }
}