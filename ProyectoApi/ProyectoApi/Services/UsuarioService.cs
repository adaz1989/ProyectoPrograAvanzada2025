using Microsoft.AspNetCore.Components.Forms;

namespace ProyectoApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<RespuestaModel> RegistrarUsuario(UsuarioModel usuario)
        {
            var(CodigoError, Mensaje) = await _usuarioRepository.RegistrarUsuario(usuario);
            
            var respuesta = new RespuestaModel
            {
                Exito = true,
                Mensaje = Mensaje
            };

            if (CodigoError != 0) respuesta.Exito = false;
            //if (CodigoError == 2) respuesta.Mensaje = "Error inesperado en la base de datos";
            
            return respuesta;

        }
    }
}
