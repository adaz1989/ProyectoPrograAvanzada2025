using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDeportivoCR.Models
{
    public class FacturaModel
    {
        public int FacturaId { get; set; }
        public double Monto { get; set; }
        public DateTime FechaHoraFactura { get; set; }
        public string? Comprobante { get; set; }

        [JsonIgnore]
        public IFormFile? FotoComprobante { get; set; }

        public byte[]? FotoComprobanteByte { get; set; }

        public long ReservacionId { get; set; }
        public long UsuarioId { get; set; }
        public long MetodoPagoId { get; set; }

        public string? UsuarioNombre { get; set; }
        public string? MetodoPagoNombre { get; set; }
    }
}
