namespace ProyectoApi.Services
{
    public interface IHorariosCanchasService
    {
        public Task<RespuestaModel> ObtenerHorariosCancha(long canchaId);
        public Task<RespuestaModel> RegistrarHorarioCancha(HorarioCanchaModel model);
    }
}
