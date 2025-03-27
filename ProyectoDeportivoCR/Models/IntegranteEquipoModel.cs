using System.ComponentModel.DataAnnotations;

namespace ProyectoDeportivoCR.Models
{
    public class IntegranteEquipoModel
    {
        public long EquipoId { get; set; }
        public int Rol { get; set; }

        [StringLength(09)]
        public string? Cedula { get; set; }

        public DateOnly FechaInscripcion { get; set; }

    }
}
