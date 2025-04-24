using ProyectoApi.Data;
using System.Data;
using Dapper;

namespace ProyectoApi.Repositories
{
    public class ReservacionRepository : IReservacionRepository
    {
        private readonly IDapperContext _context;

        public ReservacionRepository(IDapperContext dapperContext)
        {
            _context = dapperContext;
        }

        public async Task<IEnumerable<ReservacionCanchaModel>> ObtenerReservacionesPorFecha(DateTime fecha, long canchaId)
        {
            using var conexion = _context.CrearConexion();
            var parametros = new DynamicParameters();
            parametros.Add("Fecha", fecha.Date, DbType.Date);
            parametros.Add("CanchaId", canchaId, DbType.Int64);

            var resultado = await conexion.QueryAsync<ReservacionCanchaModel>(
                "ObtenerReservacionesPorFecha",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }


        public async Task<(int CodigoError, string Mensaje)> RegistrarReservacion(ReservacionCanchaModel model)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@FechaReservavion", model.FechaReservavion.Date, DbType.Date);
            parametros.Add("@HoraInicio", model.HoraInicio, DbType.Time);
            parametros.Add("@HoraFin", model.HoraFin, DbType.Time);
            parametros.Add("@CanchaId", model.CanchaId, DbType.Int64);
            parametros.Add("@UsuarioId", model.UsuarioId, DbType.Int64);
            parametros.Add("@TorneoId", model.TorneoId, DbType.Int64);
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);


            using var conexion = _context.CrearConexion();
            await conexion.ExecuteAsync("RegistrarReservacion", parametros, commandType: CommandType.StoredProcedure);

            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<(int CodigoError, string Mensaje)> DeshabilitarReservacion(long reservacionId)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@ReservacionId", reservacionId, DbType.Int64);
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            using var conexion = _context.CrearConexion();
            await conexion.ExecuteAsync(
                "DeshabilitarReservacion",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            int codigo = parametros.Get<int>("@CodigoError");
            string mensaje = parametros.Get<string>("@Mensaje")!;
            return (codigo, mensaje);
        }

    }
}
