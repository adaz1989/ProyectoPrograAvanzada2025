
using Dapper;
using ProyectoApi.Data;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly IDapperContext _context;

        public CategoriaRepository(IDapperContext context) { 
            
            _context = context;
        }


        public async Task<(int CodigoError, string Mensaje)> ActualizarInformacionCategoria(CategoriaModel model)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(new
            {
                model.CategoriaId, 
                model.NombreCategoria,
                model.EdadMinima,
                model.EdadMaxima,
            });


            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.EditarCategoria", parametros, commandType: CommandType.StoredProcedure);


            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<(int CodigoError, string Mensaje)> DeshabilitarCategoria(int CategoriaId)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(
                new
                {
                    CategoriaId
                });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.DeshabilitarCategoria", parametros, commandType: CommandType.StoredProcedure);


            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<CategoriaModel> ObtenerCategoria(int CategoriaId)
        {
            using var conexion = _context.CrearConexion();

            var parameters = new DynamicParameters();
            parameters.Add("@CategoriaId", CategoriaId);
            parameters.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            var resultado = await conexion.QueryFirstOrDefaultAsync<CategoriaModel>(
                "dbo.ObtenerCategoriasPorId",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            int codigoError = parameters.Get<int>("@CodigoError");
            string mensaje = parameters.Get<string>("@Mensaje");

            return resultado!;
        }

        public async Task<(int CodigoError, string Mensaje)> RegistrarCategoria(CategoriaModel model)
        {
            using var conexion = _context.CrearConexion();

            // Crear los parámetros con datos de entrada
            var parametros = new DynamicParameters(new
            {
                model.NombreCategoria,
                model.EdadMinima,
                model.EdadMaxima,
                             
            });

            // Agregar parámetros de salida
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            // Ejecutar el procedimiento
            await conexion.ExecuteAsync("dbo.RegistrarCategoria", parametros, commandType: CommandType.StoredProcedure);

            // Obtener los valores de salida
            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }
        public async Task<IEnumerable<CategoriaModel>> ObtenerTodasLasCategorias()
        {
            using var conexion = _context.CrearConexion();

            var resultado = await conexion.QueryAsync<CategoriaModel>(
                "dbo.ObtenerTodasLasCategorias",
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }
    }
}
