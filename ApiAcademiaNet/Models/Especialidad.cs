using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiAcademiaNet.Models
{
    public class Especialidad
    {
        [Key]
        public int IdEspecialidad { get; set; }

        [StringLength(255)]
        public string DescEspecialidad { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Plan> Plan { get; set; } = new List<Plan>();
    }
}