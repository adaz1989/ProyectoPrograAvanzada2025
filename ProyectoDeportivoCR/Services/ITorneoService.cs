using ProyectoDeportivoCR.Models;

namespace ProyectoDeportivoCR.Services
{
    public interface ITorneoService
    {
        List<TorneoModel> ConsultarDatosTorneos(long TorneoId);
    }
}
