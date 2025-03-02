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
            using var connection = _context.CrearConexion();

            // Crear los parámetros con datos de entrada
            var parameters = new DynamicParameters(new
            {
                model.NombreUsuario,
                model.ApellidosUsuario,
                model.CorreoUsuario,
                model.TelefonoUsuario,
                model.Contrasenna
            });

            // Agregar parámetros de salida
            parameters.Add("@CodigoError", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Mensaje", dbType: DbType.String, size: 255, direction: ParameterDirection.Output);

            // Ejecutar el procedimiento
            await connection.ExecuteAsync("dbo.RegistrarUsuario", parameters, commandType: CommandType.StoredProcedure);

            // Obtener los valores de salida
            int Exito = parameters.Get<int>("@CodigoError");
            string Mensaje = parameters.Get<string>("@Mensaje");

            return (Exito, Mensaje);
        }
    }
}
