using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class ReservacionCanchaModel
    {
        public long ReservacionId { get; set; }
        public DateTime FechaReservavion { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        // Llaves foráneas
        public long CanchaId { get; set; }
        public long UsuarioId { get; set; }
        public long? TorneoId { get; set; } = null;

        // Desnormalización
        public string? NombreCancha { get; set; }
        public string? NombreUsuario { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? NombreTorneo { get; set; }

    }
}
