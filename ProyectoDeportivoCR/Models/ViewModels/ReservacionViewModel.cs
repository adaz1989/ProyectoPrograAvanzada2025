using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoDeportivoCR.Models.ViewModels
{
    public class ReservacionViewModel
    {
        public CanchaModel Cancha { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public List<TimeSlotViewModel> Slots { get; set; } = new();

        public List<SelectListItem> HoraInicioOptions { get; set; } = new();
        public List<SelectListItem> HoraFinOptions { get; set; } = new();

        public string? SelectedHoraInicio { get; set; }
        public string? SelectedHoraFin { get; set; }
    }
}
