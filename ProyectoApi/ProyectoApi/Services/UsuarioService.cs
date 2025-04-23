using System.Security.Claims;

namespace ProyectoApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtService _jwtService;

        public UsuarioService(IUsuarioRepository usuarioRepository, IJwtService jwtService)
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
        }

        public async Task<RespuestaModel> RegistrarUsuario(UsuarioModel model)
        {
            var (CodigoError, Mensaje) = await _usuarioRepository.RegistrarUsuario(model);

            return CodigoError switch
            {
                0 => new RespuestaModel { Exito = true, Mensaje = Mensaje },
                1 => new RespuestaModel { Exito = false, Mensaje = Mensaje },
                _ => new RespuestaModel
                {
                    Exito = false,
                    Mensaje = "Error inesperado en la base de datos"
                }
            };
        }

        public async Task<RespuestaModel> AutenticarUsuario(UsuarioModel model)
        {
            var resultado = await _usuarioRepository.AutenticarUsuario(model);
            var respuesta = new RespuestaModel();

            if (resultado != null)
            {
                resultado.Token = _jwtService.GenerarToken(resultado.UsuarioId, resultado.DescripcionTipoUsuario!);

                respuesta.Exito = true;
                respuesta.Datos = resultado;
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "Correo o contraseña incorrectos";
            }
            return (respuesta);
        }


        public async Task<RespuestaModel> ActualizarInformacionUsuario(UsuarioModel model)
        {
            var (CodigoError, Mensaje) = await _usuarioRepository.ActualizarInformacionUsuario(model);

            return CodigoError switch
            {
                0 => new RespuestaModel { Exito = true, Mensaje = Mensaje },
                1 => new RespuestaModel { Exito = false, Mensaje = Mensaje },
                _ => new RespuestaModel { Exito = false, Mensaje = "Error inesperado en la base de datos"
                }
            };

        }

        public async Task<RespuestaModel> DeshabilitarUsuario(int usuarioId)
        {
            var (CodigoError, Mensaje) = await _usuarioRepository.DeshabilitarUsuario(usuarioId);

            return CodigoError switch
            {
                0 => new RespuestaModel { Exito = true, Mensaje = Mensaje },
                1 => new RespuestaModel { Exito = false, Mensaje = Mensaje },
                _ => new RespuestaModel
                {
                    Exito = false,
                    Mensaje = "Error inesperado en la base de datos"
                }
            };
        }

        public async Task<RespuestaModel> ObtenerInformacionUsuario(HttpContext httpContext)
        {
            var usuarioId = _jwtService.ObtenerUsuarioJwt(httpContext.User.Claims);

            var resultado = await _usuarioRepository.ObtenerPerfilUsuario(usuarioId);

            var respuesta = new RespuestaModel();

            if (resultado != null)
            {
                respuesta.Exito = true;
                respuesta.Datos = resultado;
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "No se encontro un usuario valido con ese Id";
            }
            return (respuesta);

        }
    }

}
