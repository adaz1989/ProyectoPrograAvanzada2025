using System.ComponentModel.DataAnnotations;

namespace ProyectoDeportivoCR.Models
{
    public class EquipoTorneoModel
    {
        // Equipo
        public string? NombreEquipo { get; set; }
        public long DeporteId { get; set; }

        // IntegranteEquipo
        public List<IntegranteModel> Integrantes { get; set; } = new List<IntegranteModel>();

        [Required]
        public long TorneoId { get; set; }
    }

    public class IntegranteModel
    {
        [StringLength(09)]
        public string? Cedula { get; set; }
        public int Rol { get; set; }
    }
}
