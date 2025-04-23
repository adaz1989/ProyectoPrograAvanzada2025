namespace ProyectoApi.Services
{
    public interface IUsuarioService
    {
        public Task<RespuestaModel> RegistrarUsuario(UsuarioModel usuario);
        public Task<RespuestaModel> AutenticarUsuario(UsuarioModel model);
        public Task<RespuestaModel> ActualizarInformacionUsuario(UsuarioModel model);
        public Task<RespuestaModel> DeshabilitarUsuario(int usuarioId);
        public Task<RespuestaModel> ObtenerInformacionUsuario(HttpContext httpContext);
    }
}
