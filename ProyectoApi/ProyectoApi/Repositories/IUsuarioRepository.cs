namespace ProyectoApi.Repositories
{
    public interface IUsuarioRepository
    {
        // Devuelve 
        public Task<(int CodigoError, string Mensaje)> RegistrarUsuario(UsuarioModel model);
        public Task<UsuarioModel> AutenticarUsuario(UsuarioModel model);
        //public Task<bool> ActualizarUsuario(UsuarioModel usuario);        
        //public Task<bool> InactivarUsuario(long usuarioId);
    }
}
