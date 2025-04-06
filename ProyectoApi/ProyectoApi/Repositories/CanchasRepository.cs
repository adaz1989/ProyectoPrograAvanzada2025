using Dapper;
using ProyectoApi.Data;
using System.Data;

namespace ProyectoApi.Repositories
{
    public class CanchasRepository : ICanchasRepository
    {

        private readonly IDapperContext _context;

        public CanchasRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<(int CodigoError, string Mensaje)> ActualizarInformacionCancha(CanchaModel model)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters();
            parametros.Add("@CanchaId", model.CanchaId);
            parametros.Add("@NombreCancha", model.NombreCancha);
            parametros.Add("@CorreoCancha", model.CorreoCancha);
            parametros.Add("@TelefonoCancha", model.TelefonoCancha);
            parametros.Add("@PrecioHora", model.PrecioHora);
            parametros.Add("@DeporteId", model.DeporteId);
            parametros.Add("@DetalleDireccion", model.DetalleDireccion);
            parametros.Add("@DescripcionCancha", model.DescripcionCancha);
            parametros.Add("@Estado", model.Estado);
            parametros.Add("@UsuarioId", model.UsuarioId);

            // Parámetros de salida
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.ActualizarInformacionCancha", parametros, commandType: CommandType.StoredProcedure);

            int codigoError = parametros.Get<int>("@CodigoError");
            string mensaje = parametros.Get<string>("@Mensaje");

            return (codigoError, mensaje);
        }


        public async Task<(int CodigoError, string Mensaje)> DeshabilitarCancha(long canchaId)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(new { CanchaId = canchaId });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.DeshabilitarCancha", parametros, commandType: CommandType.StoredProcedure);

            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<CanchaModel> ObtenerCancha(long canchaId)
        {
            using var conexion = _context.CrearConexion();

            var parameters = new DynamicParameters();
            parameters.Add("@CanchaId", canchaId);

            // Parámetros de salida
            parameters.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            var resultado = await conexion.QueryFirstOrDefaultAsync<CanchaModel>(
                "dbo.ObtenerCancha",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            int codigoError = parameters.Get<int>("@CodigoError");
            string mensaje = parameters.Get<string>("@Mensaje");

            return resultado!;
        }
        public async Task<(int CodigoError, string Mensaje)> RegistrarCancha(CanchaModel model)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters();
            parametros.Add("@NombreCancha", model.NombreCancha);
            parametros.Add("@CorreoCancha", model.CorreoCancha);
            parametros.Add("@TelefonoCancha", model.TelefonoCancha);
            parametros.Add("@PrecioHora", model.PrecioHora);
            parametros.Add("@DeporteId", model.DeporteId);
            parametros.Add("@ProvinciaId", model.ProvinciaId);
            parametros.Add("@CantonId", model.CantonId);
            parametros.Add("@DistritoId", model.DistritoId);
            parametros.Add("@DetalleDireccion", model.DetalleDireccion);
            parametros.Add("@DescripcionCancha", model.DescripcionCancha);
            parametros.Add("@UsuarioId", model.UsuarioId);

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            // Ejecutar el procedimiento
            await conexion.ExecuteAsync("dbo.RegistrarCancha", parametros, commandType: CommandType.StoredProcedure);

            // Obtener los valores de salida
            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<IEnumerable<CanchaModel>> ObtenerTodasLasCanchas()
        {
            using var conexion = _context.CrearConexion();

            var resultado = await conexion.QueryAsync<CanchaModel>(
                "dbo.ObtenerTodasLasCanchas",
                commandType: CommandType.StoredProcedure
            );

            return resultado;
        }
    }
}
