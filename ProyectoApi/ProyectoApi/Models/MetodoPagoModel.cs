using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class MetodoPagoModel
    {
        public long MetodoPagoId { get; set; }
        [StringLength(50)]
        public string? DescripcionMetodoPago { get; set; }

    }
}
