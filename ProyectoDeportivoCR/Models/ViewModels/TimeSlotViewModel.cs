namespace ProyectoDeportivoCR.Models.ViewModels
{
    public class TimeSlotViewModel
    {
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin { get; set; }
        public string Estado { get; set; } = "";
    }
}
