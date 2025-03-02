using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class CantonModel
    {
        public long CantonId { get; set; }
        [StringLength(50)]
        public string? NombreCanton { get; set; }
        public long ProvinciaId { get; set; }

        // Desnormalizacion?
    }
}
