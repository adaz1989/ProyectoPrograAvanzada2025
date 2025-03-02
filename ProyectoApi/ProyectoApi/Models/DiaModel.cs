using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class DiaModel
    {
        public long DiaId { get; set; }
        [StringLength(50)]
        public string? NombreDia { get; set; }        
    }
}
