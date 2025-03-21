
namespace ProyectoDeportivoCR.Services
{
    public interface IUsuarioService
    {
        public Task<RespuestaModel<UsuarioModel>> IniciarSesion(UsuarioModel model);
        //public Task<bool> RegistrarUsuario(UsuarioModel usuario);
    }
}
