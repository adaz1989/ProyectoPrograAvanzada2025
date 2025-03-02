
using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class FacturaModel
    {
        public int FacturaId { get; set; }
        public double Monto { get; set; }
        public DateTime FechaHoraFactura { get; set; }        
        public string? Comprobante { get; set; }
        // Llaves foráneas
        public long ReservcaionId { get; set; }
        public long UsuarioId { get; set; }
        public long MetodoPagoId { get; set; }

        // Desnormalización
        public string? UsuarioNombre { get; set; }
        public string? MetodoPagoNombre { get; set; }

    }
}
