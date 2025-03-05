namespace ProyectoApi.Services
{
    public interface IUsuarioService
    {
        public Task<RespuestaModel> RegistrarUsuario(UsuarioModel usuario);
        public Task<RespuestaModel> AutenticarUsuario(UsuarioModel model);
        //public Task<RespuestaModel> ActualizarUsuario(UsuarioModel usuario);
        //public Task<RespuestaModel> InactivarUsuario(long usuarioId);
    }
}
