namespace ProyectoDeportivoCR.Repositories
{
    public interface IReservacionRepositorie
    {
        Task<HttpResponseMessage> ObtenerReservacionesPorFecha(string token, DateTime fecha, long canchaId);
        Task<HttpResponseMessage> RegistrarReservacion(string token, ReservacionCanchaModel model);
        Task<HttpResponseMessage> DeshabilitarReservacion(string token, long reservacionId);
    }
}
