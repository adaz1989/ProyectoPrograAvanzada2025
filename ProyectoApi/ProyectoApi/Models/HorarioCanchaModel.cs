namespace ProyectoApi.Models
{
    public class HorarioCanchaModel
    {
        public long CanchaId { get; set; }
        public long DiaId { get; set; }
        public TimeOnly HoraApertura { get; set; }
        public TimeOnly HoraCierre { get; set; }
    }
}
