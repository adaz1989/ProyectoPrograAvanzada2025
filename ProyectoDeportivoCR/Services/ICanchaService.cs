namespace ProyectoDeportivoCR.Services
{
    public interface ICanchaService
    {
        Task<Respuesta2Model<CanchaModel>> RegistrarCancha(CanchaModel model);
        Task<Respuesta2Model<CanchaModel>> ActualizarInformacionCancha(CanchaModel model);
        Task<Respuesta2Model<CanchaModel>> DeshabilitarCancha(long canchaId);
        Task<Respuesta2Model<CanchaModel>> ObtenerCancha(long canchaId);
        Task<Respuesta2Model<List<CanchaModel>>> ObtenerTodasLasCanchas();

        Task<Respuesta2Model<List<HorarioCanchaModel>>> ObtenerHorariosCancha(long canchaId);
        Task<Respuesta2Model<HorarioCanchaModel>> RegistrarHorarioCancha(HorarioCanchaModel model);
    }
}