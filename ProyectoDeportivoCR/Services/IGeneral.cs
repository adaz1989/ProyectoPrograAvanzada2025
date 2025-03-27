using ProyectoDeportivoCR.Models;

namespace ProyectoDeportivoCR.Services
{
    public interface IGeneral
    {
        List<TorneoModel> ConsultarDatosTorneos(long TorneoId);
    }
}
