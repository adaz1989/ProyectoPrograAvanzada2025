using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class DeporteModel
    {
        public long DeporteId { get; set; }
        [StringLength(50)]
        public string? NombreDeporte { get; set; }

    }
}
