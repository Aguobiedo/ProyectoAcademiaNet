using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiAcademiaNet.Models
{
    public class Plan
    {
        [Key]
        public int IdPlan { get; set; }

        [StringLength(255)]
        public string DescPlan { get; set; }

        [ForeignKey("Especialidad")]
        [Required]
        public int IdEspecialidad { get; set; }

        public Especialidad? Especialidad { get; set; } = new Especialidad();

        [JsonIgnore]
        public ICollection<Materia> Materia { get; set; } = new List<Materia>();
        [JsonIgnore]
        public ICollection<Comision> Comision { get; set; } = new List<Comision>();
        [JsonIgnore]
        public ICollection<Persona> Persona { get; set; } = new List<Persona>();
    }
}