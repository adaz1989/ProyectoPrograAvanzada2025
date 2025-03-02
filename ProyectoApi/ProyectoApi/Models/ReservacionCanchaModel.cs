using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class ReservacionCanchaModel
    {
        public long ReservacionId { get; set; }
        public DateOnly FechaReservacion { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        // Llaves foraneas
        public long CanchaId { get; set; }
        public long UsuarioId { get; set; }
        public long TorneoId { get; set; }

        // Desnormalizacion
        // Info de la cancha
        public string? NombreCancha { get; set; }
        public string? CorreoCancha { get; set; }
        public string? TelefonoCancha { get; set; }
        public double PrecioHora { get; set; }
        public string? DetalleDireccion { get; set; }
        public string? DescripcionCancha { get; set; }

        public string? NombreUsuario { get; set; }
        public string? NombreTorneo { get; set; }

    }
}
