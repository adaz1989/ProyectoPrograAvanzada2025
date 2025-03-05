namespace ProyectoApi.Repositories
{
    public interface IUsuarioRepository
    {
        // Devuelve 
        public Task<(int CodigoError, string Mensaje)> RegistrarUsuario(UsuarioModel model);
        public Task<UsuarioModel> AutenticarUsuario(UsuarioModel model);
        public Task<(int CodigoError, string Mensaje)> ActualizarInformacionUsuario(UsuarioModel model);
        public Task<(int CodigoError, string Mensaje)> DeshabilitarUsuario(UsuarioModel model);
    }
}
