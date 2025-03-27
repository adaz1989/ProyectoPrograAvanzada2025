
using Dapper;
using ProyectoApi.Data;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class EquipoRepository : IEquipoRepository
    {
        private readonly IDapperContext _context;

        public EquipoRepository(IDapperContext dapperContext) 
        {
            _context = dapperContext;
        }

        public async Task<(int CodigoError, string Mensaje)> ActualizarInformacionEquipo(EquipoModel model)
        {
            using var conexion = _context.CrearConexion();

            // Crear parámetros de entrada para el procedimiento dbo.EditarEquipo
            var parametros = new DynamicParameters(new
            {
                model.EquipoId,
                model.NombreEquipo,
                model.DeporteId,
                model.CategoriaId,
                model.UsuarioId
            });

            // Agregar parámetros de salida
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            // Ejecutar el procedimiento almacenado
            await conexion.ExecuteAsync("dbo.EditarEquipo", parametros, commandType: CommandType.StoredProcedure);

            // Obtener los valores de salida
            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<(int CodigoError, string Mensaje)> DeshabilitarEquipo(int EquipoId)
        {
            using var conexion = _context.CrearConexion();

            // Crear parámetros de entrada para el procedimiento dbo.DeshabilitarEquipo
            var parametros = new DynamicParameters();
            parametros.Add("@EquipoId", EquipoId);
            // Agregar parámetros de salida
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            // Ejecutar el procedimiento almacenado
            await conexion.ExecuteAsync("dbo.DeshabilitarEquipo", parametros, commandType: CommandType.StoredProcedure);

            // Obtener los valores de salida
            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<EquipoModel> ObtenerEquipo(int EquipoId)
        {
            using var conexion = _context.CrearConexion();

            // Crear parámetros de entrada para el procedimiento dbo.ObtenerEquiposPorId.
            // Aquí se asume que se quiere obtener un solo equipo
            var parametros = new DynamicParameters();
            parametros.Add("@EquipoId", EquipoId);
            // También se agregan los parámetros de salida
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            // Ejecutar el procedimiento y obtener el equipo
            var equipo = await conexion.QueryFirstOrDefaultAsync<EquipoModel>(
                "dbo.ObtenerEquiposPorId",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            // Opcional: se pueden validar los parámetros de salida si se requiere manejar algún error específico.
            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            // Si no se encontró el equipo o hubo un error, se podría lanzar una excepción o devolver null.
            // Aquí simplemente retornamos el objeto obtenido (puede ser null si no existe).
            return equipo!;
        }

        public async Task<(int CodigoError, string Mensaje)> RegistrarEquipo(EquipoModel model)
        {
            using var conexion = _context.CrearConexion();

            // Crear los parámetros con datos de entrada
            var parametros = new DynamicParameters(new
            {
                model.NombreEquipo,
                model.EquipoId,
                model.CategoriaId,
                model.UsuarioId,

            });

            // Agregar parámetros de salida
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            // Ejecutar el procedimiento
            await conexion.ExecuteAsync("dbo.RegistrarEquipo", parametros, commandType: CommandType.StoredProcedure);

            // Obtener los valores de salida
            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }
    }
}
