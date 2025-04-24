using System.Data;

namespace ProyectoApi.Repositories
{
    public interface IReservacionRepository
    {
        public Task<IEnumerable<ReservacionCanchaModel>> ObtenerReservacionesPorFecha(DateTime fecha, long canchaId);

        public Task<(int CodigoError, string Mensaje)> RegistrarReservacion(ReservacionCanchaModel model);
        public Task<(int CodigoError, string Mensaje)> DeshabilitarReservacion(long reservacionId);
    }
}
