namespace ProyectoDeportivoCR.Repositories
{
    public interface IUsuarioRepositorie
    {

        public Task<HttpResponseMessage> IniciarSesion(UsuarioModel model);
        Task<HttpResponseMessage> RegistrarUsuario(UsuarioModel model, string? token);
        public Task<HttpResponseMessage> ObtenerInformacionUsuario(string token);
    }
}
