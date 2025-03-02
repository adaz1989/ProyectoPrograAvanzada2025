using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class ProvinciaModel
    {
        public long PrivinciaId { get; set; }
        [StringLength(50)]
        public string? NombreProvincia { get; set; }

    }
}
