using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class TipoUsuarioModel
    {
        public long TipoUsuarioId { get; set; }
        [StringLength(50)]
        public string? DescripcionTipoUsuario { get; set; }

    }
}
