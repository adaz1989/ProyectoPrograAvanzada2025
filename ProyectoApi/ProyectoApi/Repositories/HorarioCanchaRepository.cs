
using System.Data;
using Dapper;
using ProyectoApi.Data;

namespace ProyectoApi.Repositories
{
    public class HorarioCanchaRepository : IHorarioCanchaRepository
    {
        private readonly IDapperContext _context;

        public HorarioCanchaRepository(IDapperContext dapperContext)
        {
            _context = dapperContext;
        }

        public async Task<IEnumerable<HorarioCanchaModel>> ObtenerHorariosCancha(long canchaId)
        {
            using var conexion = _context.CrearConexion();

            var resultado = await conexion.QueryAsync<HorarioCanchaModel>(
                "ObtenerHorariosCancha",
                new { canchaId },
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }

        public async Task<(int CodigoError, string Mensaje)> RegistrarHorarioCancha(HorarioCanchaModel model)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(new
            {
                model.CanchaId,
                model.DiaId,
                model.HoraApertura,
                model.HoraCierre
            });
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("RegistrarHorarioCancha", parametros, commandType: CommandType.StoredProcedure);

            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

    }
}

