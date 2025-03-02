using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class IntegranteEquipoModel
    {
        public long EquipoId { get; set; }
        [StringLength(50)]
        public string? Cedula { get; set; }

        public DateOnly FechaInscripcion { get; set; }

    }
}
