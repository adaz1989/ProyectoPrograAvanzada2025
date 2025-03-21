namespace ProyectoDeportivoCR.Repositories
{
    public interface IUsuarioRepositorie
    {

        public Task<HttpResponseMessage> IniciarSesion(UsuarioModel model);
        //public Task<bool?> RegistrarUsuario(UsuarioModel model);
    }
}
