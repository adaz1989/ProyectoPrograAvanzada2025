
using Dapper;
using ProyectoApi.Data;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class DeporteRepository : IDeporteRepository
    {
        private readonly IDapperContext _context;

        public DeporteRepository(IDapperContext context) 
        { 
            _context = context;
            
        }
        public async Task<(int CodigoError, string Mensaje)> ActualizarInformacionDeporte(DeporteModel model)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(new
            {
                model.DeporteId,
                model.NombreDeporte,
            
            });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.EditarDeporte", parametros, commandType: CommandType.StoredProcedure);


            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }


        public async Task<(int CodigoError, string Mensaje)> EliminarDeporte(long deporteId)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(new
            {
                DeporteId = deporteId
            });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync(
                "dbo.EliminarDeporte",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            int codigoError = parametros.Get<int>("@CodigoError");
            string mensaje = parametros.Get<string>("@Mensaje");

            return (codigoError, mensaje);
        }


        public async Task<DeporteModel> ObtenerDeporte(long DeporteId)
        {
            using var conexion = _context.CrearConexion();

            var parameters = new DynamicParameters();
            parameters.Add("DeporteId", DeporteId);
            parameters.Add("CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("Mensaje", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

            var resultado = await conexion.QueryFirstOrDefaultAsync<DeporteModel>(
                "dbo.ObtenerDeportesPorId",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            int codigoError = parameters.Get<int>("CodigoError");
            string mensaje = parameters.Get<string>("Mensaje");

            if (codigoError != 0)
            {
                throw new Exception($"Error {codigoError}: {mensaje}");
            }

            return resultado!;
        }

        public async Task<(int CodigoError, string Mensaje)> RegistrarDeporte(DeporteModel model)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(new
            {
                model.NombreDeporte 
            });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.RegistrarDeporte", parametros, commandType: CommandType.StoredProcedure);

            return (parametros.Get<int>("@CodigoError"), parametros.Get<string>("@Mensaje"));
        }
    }
}
