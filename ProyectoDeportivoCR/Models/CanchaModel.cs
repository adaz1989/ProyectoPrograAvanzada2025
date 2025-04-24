using System.ComponentModel.DataAnnotations;

namespace ProyectoDeportivoCR.Models
{
    public class CanchaModel
    {
        public long CanchaId { get; set; }
        [StringLength(50)]
        public string? NombreCancha { get; set; }
        [StringLength(50)]
        public string? CorreoCancha { get; set; }
        [StringLength(50)]
        public string? TelefonoCancha { get; set; }
        public double PrecioHora { get; set; }
        [StringLength(100)]
        public string? DetalleDireccion { get; set; }
        [StringLength(100)]
        public string? DescripcionCancha { get; set; }

        // Llaves foraneas
        public long UsuarioId { get; set; }
        public long DeporteId { get; set; }
        public long ProvinciaId { get; set; }
        public long CantonId { get; set; }
        public long DistritoId { get; set; }

        // Desnormalizacion
        public string? NombreUsuario { get; set; }
        public string? NombreDeporte { get; set; }
        public string? NombreProvincia { get; set; }
        public string? NombreCanton { get; set; }
        public string? NombreDistrito { get; set; }

        public List<HorarioCanchaModel> Horarios { get; set; } = new();
    }
}
