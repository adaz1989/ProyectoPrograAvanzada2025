using System.ComponentModel.DataAnnotations;

namespace ProyectoDeportivoCR.Models
{
    public class EquipoTorneoModel
    {
        // Equipo
        public string? NombreEquipo { get; set; }
        public long DeporteId { get; set; }

        // IntegranteEquipo
        public int Rol { get; set; }

        [StringLength(09)]
        public string? Cedula { get; set; }

        [Required]
        public long TorneoId { get; set; }
    }
}
