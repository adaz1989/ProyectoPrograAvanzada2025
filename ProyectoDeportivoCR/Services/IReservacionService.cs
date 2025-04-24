namespace ProyectoDeportivoCR.Services
{
    public interface IReservacionService
    {
        Task<Respuesta2Model<List<ReservacionCanchaModel>>> ObtenerReservacionesPorFecha(DateTime fecha, long canchaId);
        Task<Respuesta2Model<object>> RegistrarReservacion(ReservacionCanchaModel model);
        Task<Respuesta2Model<object>> DeshabilitarReservacion(long reservacionId);
    }
}
