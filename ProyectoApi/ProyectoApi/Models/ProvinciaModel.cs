using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class ProvinciaModel
    {
        public long? ProvinciaId { get; set; }
        [StringLength(50)]
        public string? NombreProvincia { get; set; }

    }
}
