namespace ProyectoDeportivoCR.Repositories
{
    public interface IUsuarioRepositorie
    {

        public Task<HttpResponseMessage> IniciarSesion(UsuarioModel model);
        public Task<HttpResponseMessage> RegistrarUsuario(UsuarioModel model);
    }
}
