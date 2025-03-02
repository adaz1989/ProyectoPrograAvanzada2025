using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class RenennaCanchaModel
    {
        public long CanchaId { get; set; }
        public long UsuarioId { get; set; }
        [StringLength(255)]
        public string? Comentario { get; set; }
        public int Calificacion { get; set; }
    }
}
