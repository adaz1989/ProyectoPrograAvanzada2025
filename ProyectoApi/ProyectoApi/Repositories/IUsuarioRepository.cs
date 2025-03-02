namespace ProyectoApi.Repositories
{
    public interface IUsuarioRepository
    {
        // Devuelve 
        public Task<(int CodigoError, string Mensaje)> RegistrarUsuario(UsuarioModel usuario);
        //public Task<UsuarioModel> AutenticarUsuario(string correo, string contrasenna);
        //public Task<bool> ActualizarUsuario(UsuarioModel usuario);        
        //public Task<bool> InactivarUsuario(long usuarioId);
    }
}
