using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiAcademiaNet.Models
{
    public class Comision
    {
        [Key]
        public int IdComision { get; set; }

        [StringLength(255)]
        public string DescComision { get; set; }


        [ForeignKey("Plan")]
        [Required]
        public int IdPlan { get; set; }

        public Plan? Plan { get; set; } = new Plan();

        [JsonIgnore]
        public ICollection<Curso> Curso { get; set; }= new List<Curso>();
    }
}