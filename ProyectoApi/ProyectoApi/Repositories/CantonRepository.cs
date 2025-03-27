
using Dapper;
using ProyectoApi.Data;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class CantonRepository : ICantonRepository
    {

        private readonly IDapperContext _context;

        public CantonRepository(IDapperContext context)
        { 
            _context = context;
        }

        public async Task<(int CodigoError, string Mensaje)> ActualizarInformacionCanton(CantonModel model)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(new
            {
                model.NombreCanton,

            });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.EditarCanton", parametros, commandType: CommandType.StoredProcedure);


            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<CantonModel> ObtenerCanton(int CantonId)
        {
            using var conexion = _context.CrearConexion();

            var resultado = await conexion.QueryFirstOrDefaultAsync<CantonModel>(
                "dbo.ObtenerCantonesPorId",
                new { CantonId },
                commandType: CommandType.StoredProcedure
            );

            return resultado!;
        }

        public async Task<(int CodigoError, string Mensaje)> RegistrarCanton(CantonModel model)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(new
            {
                model.CantonId,  
                model.NombreCanton,
                model.ProvinciaId
            });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync(
                "dbo.RegistrarCanton",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            int codigoError = parametros.Get<int>("@CodigoError");
            string mensaje = parametros.Get<string>("@Mensaje");

            return (codigoError, mensaje);
        }
    }
}
