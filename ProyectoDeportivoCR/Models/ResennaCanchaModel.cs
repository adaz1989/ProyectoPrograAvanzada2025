using System.ComponentModel.DataAnnotations;

namespace ProyectoDeportivoCR.Models
{
    public class ResennaCanchaModel
    {
        public long CanchaId { get; set; }
        public long UsuarioId { get; set; }
        public string? Comentario { get; set; }
        public int Calificacion { get; set; }
    }
}
