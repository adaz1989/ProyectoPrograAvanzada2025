
using Dapper;
using ProyectoApi.Data;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class ProvinciaRepository : IProvinciaRepository
    {

        private readonly IDapperContext _context;

        public ProvinciaRepository(IDapperContext context)
        {
            _context = context;
        }


        public async Task<(int CodigoError, string Mensaje)> ActualizarInformacionProvincia(ProvinciaModel model)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(new
            {
                model.ProvinciaId,
                model.NombreProvincia,
            });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.EditarProvincia", parametros, commandType: CommandType.StoredProcedure);


            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<ProvinciaModel> ObtenerProvincia(long ProvinciaId) // Cambiado a long
        {
            using var conexion = _context.CrearConexion();
            var parametros = new DynamicParameters(new { ProvinciaId });
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            var resultado = await conexion.QueryFirstOrDefaultAsync<ProvinciaModel>(
                "dbo.ObtenerProvinciasPorId",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            int codigoError = parametros.Get<int>("@CodigoError");
            string mensaje = parametros.Get<string>("@Mensaje");

            if (codigoError != 0)
                throw new Exception(mensaje);

            return resultado!;
        }

        public async Task<(int CodigoError, string Mensaje)> RegistrarProvincia(ProvinciaModel model)
        {
            using var conexion = _context.CrearConexion();

            // Crear los parámetros con datos de entrada
            var parametros = new DynamicParameters(new
            {
                model.ProvinciaId,
                model.NombreProvincia

            });

            // Agregar parámetros de salida
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            // Ejecutar el procedimiento
            await conexion.ExecuteAsync("dbo.RegistrarProvincia", parametros, commandType: CommandType.StoredProcedure);

            // Obtener los valores de salida
            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

    }
}
