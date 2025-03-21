
namespace ProyectoDeportivoCR.Services
{
    public interface IUsuarioService
    {
        public Task<Respuesta2Model<UsuarioModel>> IniciarSesion(UsuarioModel model);
        //public Task<bool> RegistrarUsuario(UsuarioModel usuario);
    }
}
