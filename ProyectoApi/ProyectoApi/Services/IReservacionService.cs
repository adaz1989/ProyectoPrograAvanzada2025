namespace ProyectoApi.Services
{
    public interface IReservacionService
    {
        public Task<RespuestaModel> RegistrarReservacion(ReservacionCanchaModel model, HttpContext httpContext);
        public Task<RespuestaModel> ObtenerReservacionesPorFecha(DateTime fecha, long canchaId);
        public Task<RespuestaModel> DeshabilitarReservacion(long reservacionId);
    }
}
