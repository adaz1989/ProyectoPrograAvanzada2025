using Dapper;
using ProyectoApi.Data;
using ProyectoApi.Models;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class ResennaRepository : IResennaCanchaRepository
    {
        private readonly IDapperContext _context;

        public ResennaRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<ResennaCanchaModel> ObtenerResennaPorCancha(long canchaId)
        {
            using var conexion = _context.CrearConexion();

            var parameters = new DynamicParameters();
            parameters.Add("@CanchaId", canchaId, DbType.Int64);

            parameters.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            var resenna = await conexion.QueryFirstOrDefaultAsync<ResennaCanchaModel>(
                "dbo.ObtenerResennaPorId",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return resenna!;
        }

        public async Task<IEnumerable<ResennaCanchaModel>> ObtenerTodasLasResennas()
        {
            using var conexion = _context.CrearConexion();

            var resennas = await conexion.QueryAsync<ResennaCanchaModel>(
                "dbo.ObtenerTodasLasResennas",
                commandType: CommandType.StoredProcedure
            );

            return resennas;
        }

        public async Task<(int CodigoError, string Mensaje)> RegistrarResenna(ResennaCanchaModel model)
        {
            using var conexion = _context.CrearConexion();
            var parametros = new DynamicParameters(new
            {
                model.CanchaId,
                model.UsuarioId,
                model.Comentario,
                model.Calificacion
            });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.RegistrarResennaCancha", parametros, commandType: CommandType.StoredProcedure);

            int codigoError = parametros.Get<int>("@CodigoError");
            string mensaje = parametros.Get<string>("@Mensaje");

            return (codigoError, mensaje);
        }
    }
}
