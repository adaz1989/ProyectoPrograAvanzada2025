using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using ProyectoDeportivoCR.Services.Extensions;


namespace ProyectoDeportivoCR.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IEncriptacionService _encriptacion;
        private readonly IUsuarioRepositorie _usuarioRepositorie;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioService(IEncriptacionService encriptacion, IUsuarioRepositorie usuarioRepositorie, IHttpContextAccessor httpContextAccessor)
        {
            _encriptacion = encriptacion;
            _usuarioRepositorie = usuarioRepositorie;
            _httpContextAccessor = httpContextAccessor;
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
            try
            {
                model.Contrasenna = _encriptacion.Encriptar(model.Contrasenna!);
                var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

                var respuesta = await _usuarioRepositorie.RegistrarUsuario(model, token);

                return await respuesta.LeerRespuesta2Model<UsuarioModel>();
            }
            catch (Exception ex)
            {
                return new Respuesta2Model<UsuarioModel>
                {
                    Exito = false,
                    Mensaje = $"Error al registrar usuario: {ex.Message}"
                };
            }
        }

        public async Task<Respuesta2Model<UsuarioModel>> ObtenerInformacionUsuario()
        {
            var token = _httpContextAccessor.HttpContext!.Session.GetString("Token");

            var respuesta = await _usuarioRepositorie.ObtenerInformacionUsuario(token!);
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
    }
}
