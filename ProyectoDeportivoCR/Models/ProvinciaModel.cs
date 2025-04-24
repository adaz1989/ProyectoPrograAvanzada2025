using System.ComponentModel.DataAnnotations;

namespace ProyectoDeportivoCR.Models
{
    public class ProvinciaModel
    {
        public long? ProvinciaId { get; set; }
        [StringLength(50)]
        public string? NombreProvincia { get; set; }
    }
}
