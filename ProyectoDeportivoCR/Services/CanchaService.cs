using ProyectoDeportivoCR.Services.Extensions;
using System.Net.Http.Headers;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;

namespace ProyectoDeportivoCR.Services
{
    public class CanchaService : ICanchaService
    {
        private readonly ICanchaRepository _canchaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CanchaService(ICanchaRepository canchaRepository, IHttpContextAccessor httpContextAccessor)
        {
            _canchaRepository = canchaRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Respuesta2Model<CanchaModel>> RegistrarCancha(CanchaModel model)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UsuarioId");
                if (claim != null && long.TryParse(claim.Value, out var usuarioId))
                {
                    model.UsuarioId = usuarioId;
                }
                else
                {
                    return new Respuesta2Model<CanchaModel>
                    {
                        Exito = false,
                        Mensaje = "No se encontró el UsuarioId en el token."
                    };
                }
            }

            var respuesta = await _canchaRepository.RegistrarCancha(model, token);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<CanchaModel>();
            }

            return new Respuesta2Model<CanchaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<CanchaModel>> ActualizarInformacionCancha(CanchaModel model)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _canchaRepository.ActualizarInformacionCancha(model, token);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<CanchaModel>();
            }

            return new Respuesta2Model<CanchaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<CanchaModel>> ObtenerCancha(long canchaId)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _canchaRepository.ObtenerCancha(canchaId, token);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<CanchaModel>();
            }

            return new Respuesta2Model<CanchaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<CanchaModel>> DeshabilitarCancha(long canchaId)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _canchaRepository.DeshabilitarCancha(canchaId, token);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<CanchaModel>();
            }

            return new Respuesta2Model<CanchaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<List<CanchaModel>>> ObtenerTodasLasCanchas()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _canchaRepository.ObtenerTodasLasCanchas(token);
            if (respuesta.IsSuccessStatusCode)
            {
               
                return await respuesta.LeerRespuesta2Model<List<CanchaModel>>();
            }

            return new Respuesta2Model<List<CanchaModel>>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API.",
                Datos = null
            };
        }
    }
}
