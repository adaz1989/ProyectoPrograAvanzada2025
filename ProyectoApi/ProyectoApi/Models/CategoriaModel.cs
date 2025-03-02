using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class CategoriaModel
    {
        public long CategoriaId { get; set; }
        [StringLength(50)]
        public string? NombreCategoria { get; set; }
        public int EdadMinima { get; set; }
        public int EdadMaxima { get; set; }
        
    }
}
