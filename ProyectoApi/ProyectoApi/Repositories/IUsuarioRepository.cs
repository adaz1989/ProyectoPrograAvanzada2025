namespace ProyectoApi.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<(int CodigoError, string Mensaje)> RegistrarUsuario(UsuarioModel model);
        public Task<UsuarioModel> AutenticarUsuario(UsuarioModel model);
        public Task<(int CodigoError, string Mensaje)> ActualizarInformacionUsuario(UsuarioModel model);
        public Task<(int CodigoError, string Mensaje)> DeshabilitarUsuario(int UsuarioId);
        public Task<UsuarioModel> ObtenerPerfilUsuario(long UsuarioId);
    }
}
