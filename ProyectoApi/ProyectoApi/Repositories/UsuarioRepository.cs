using System.Data;
using Dapper;
using ProyectoApi.Data;

namespace ProyectoApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDapperContext _context;

        public UsuarioRepository(IDapperContext dapperContext)
        {
            _context = dapperContext;
        }
        
        public async Task<(int CodigoError, string Mensaje)> RegistrarUsuario(UsuarioModel model)
        {
            using var conexion = _context.CrearConexion();

            // Crear los parámetros con datos de entrada
            var parametros = new DynamicParameters(new
            {
                model.NombreUsuario,
                model.ApellidosUsuario,
                model.CorreoUsuario,
                model.TelefonoUsuario,
                model.Contrasenna
            });

            // Agregar parámetros de salida
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            // Ejecutar el procedimiento
            await conexion.ExecuteAsync("dbo.RegistrarUsuario", parametros, commandType: CommandType.StoredProcedure);

            // Obtener los valores de salida
            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<UsuarioModel> AutenticarUsuario(UsuarioModel model)
        {
            using var coneccion = _context.CrearConexion();

            var resultado = await coneccion.QueryFirstOrDefaultAsync<UsuarioModel>(
                "AutenticarUsuario",
                new { model.CorreoUsuario, model.Contrasenna },
                commandType: CommandType.StoredProcedure
            );

            return resultado!;
        }

        public async Task<(int CodigoError, string Mensaje)> ActualizarInformacionUsuario(UsuarioModel model)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(new
            {
                model.UsuarioId,
                model.NombreUsuario,
                model.ApellidosUsuario,
                model.TelefonoUsuario
            });

            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.ActualizarInformacionUsuario", parametros, commandType: CommandType.StoredProcedure);


            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<(int CodigoError, string Mensaje)> DeshabilitarUsuario(int UsuarioId)
        {
            using var conexion = _context.CrearConexion();

            var parametros = new DynamicParameters(
                new
                {
                    UsuarioId
                });
            parametros.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parametros.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync("dbo.DeshabilitarUsuario", parametros, commandType: CommandType.StoredProcedure);

            int CodigoError = parametros.Get<int>("@CodigoError");
            string Mensaje = parametros.Get<string>("@Mensaje");

            return (CodigoError, Mensaje);
        }

        public async Task<UsuarioModel> ObtenerPerfilUsuario(int UsuarioId)
        {
            using var conexion = _context.CrearConexion();

            var resultado = await conexion.QueryFirstOrDefaultAsync<UsuarioModel>(
                "ObtenerPerfilUsuario",
                new { UsuarioId },
                commandType: CommandType.StoredProcedure
            );

            return resultado!;
        }
    }
}
    

