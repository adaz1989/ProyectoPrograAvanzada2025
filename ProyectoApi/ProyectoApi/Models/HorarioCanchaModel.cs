namespace ProyectoApi.Models
{
    public class HorarioCanchaModel
    {
        public long CanchaId { get; set; }
        public long DiaId { get; set; }
        public TimeSpan HoraApertura { get; set; }
        public TimeSpan HoraCierre { get; set; }

        //Desnormalizado
        public string? NombreDia { get; set; }
    }
}
