using System.ComponentModel.DataAnnotations;

namespace ProyectoDeportivoCR.Models
{
    public class CantonModel
    {
        public long CantonId { get; set; }
        [StringLength(50)]
        public string? NombreCanton { get; set; }
        public long ProvinciaId { get; set; }

    }
}
