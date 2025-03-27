using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class EquipoModel
    {
        public long EquipoId { get; set; }
        public int Rol { get; set; }

        [StringLength(09)]
        public string? Cedula { get; set; }

        public DateOnly FechaInscripcion { get; set; }

        /*public long EquipoId { get; set; }
        [StringLength(50)]
        public string? NombreEquipo { get; set; }
        public long DeporteId { get; set; }
        public long CategoriaId { get; set; }
        public long UsuarioId { get; set; }*/

    }
}
