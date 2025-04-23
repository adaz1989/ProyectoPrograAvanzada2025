using System.ComponentModel.DataAnnotations;

namespace ProyectoApi.Models
{
    public class FotoCanchaModel
    {
        public long CanchaId { get; set; }
        [StringLength(255)]
        public byte[]? FotoCancha { get; set; }

    }
}
