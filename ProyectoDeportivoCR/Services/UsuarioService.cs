using System.Security.Cryptography.Xml;
using System.Text.Json;
using ProyectoDeportivoCR.Services.Extensions;


namespace ProyectoDeportivoCR.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IEncriptacionService _encriptacion;
        private readonly IUsuarioRepositorie _usuarioRepositorie;

        public UsuarioService(IEncriptacionService encriptacion, IUsuarioRepositorie usuarioRepositorie) 
        {
            _encriptacion = encriptacion;
            _usuarioRepositorie = usuarioRepositorie;
        }

        public async Task<Respuesta2Model<UsuarioModel>> IniciarSesion(UsuarioModel model)
        {
            model.Contrasenna = _encriptacion.Encriptar(model.Contrasenna!);

            var respuesta = await _usuarioRepositorie.IniciarSesion(model);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<UsuarioModel>();
            }

            return new Respuesta2Model<UsuarioModel>
            {
                Exito = false,
                Mensaje = "Error de comunicación con la API."
            };
        }

        public async Task<Respuesta2Model<UsuarioModel>> RegistrarUsuario(UsuarioModel model)
        {
            model.Contrasenna = _encriptacion.Encriptar(model.Contrasenna!);

            var respuesta = await _usuarioRepositorie.RegistrarUsuario(model);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<UsuarioModel>();
            }

            return new Respuesta2Model<UsuarioModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };

        }        
    }
}
