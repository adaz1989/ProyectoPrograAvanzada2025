using System.ComponentModel.DataAnnotations;

namespace ProyectoDeportivoCR.Models
{
    public class TorneoModel
    {
        public long TorneoId { get; set; }
        [StringLength(50)]
        public string? NombreTorneo { get; set; }
        [StringLength(255)]
        public string? DescripcionTorneo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        // Llaves Foraneas
        public long UsuarioId { get; set; }
        public long DeporteId { get; set; }
        public long CategoriaId { get; set; }

        // Desnormalizacion
        public string? NombreUsuario { get; set; }
        public string? NombreDeporte { get; set; }
        public string? NombreCategoria { get; set; }
    }
}
