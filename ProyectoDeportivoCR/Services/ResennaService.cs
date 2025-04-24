using System.IdentityModel.Tokens.Jwt;
using ProyectoDeportivoCR.Models;
using ProyectoDeportivoCR.Repositories;
using ProyectoDeportivoCR.Services.Extensions;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace ProyectoDeportivoCR.Services
{
    public class ResennaService : IResennaService
    {
        private readonly IResennaRepository _resennaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResennaService(
            IResennaRepository resennaRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _resennaRepository = resennaRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Respuesta2Model<ResennaCanchaModel>> RegistrarResenna(ResennaCanchaModel model)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return new Respuesta2Model<ResennaCanchaModel>
                {
                    Exito = false,
                    Mensaje = "No hay token de autenticación."
                };
            }

            // Extraer UsuarioId del JWT
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var claim = jwt.Claims.FirstOrDefault(c => c.Type == "UsuarioId");
            if (claim == null || !long.TryParse(claim.Value, out var usuarioId))
            {
                return new Respuesta2Model<ResennaCanchaModel>
                {
                    Exito = false,
                    Mensaje = "No se encontró el UsuarioId en el token."
                };
            }
            model.UsuarioId = usuarioId;

            var response = await _resennaRepository.RegistrarResenna(model, token);
            if (response.IsSuccessStatusCode)
                return await response.LeerRespuesta2Model<ResennaCanchaModel>();

            return new Respuesta2Model<ResennaCanchaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<ResennaCanchaModel>> ObtenerResennaPorCancha(long canchaId)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var response = await _resennaRepository.ObtenerResennaPorCancha(canchaId, token);

            if (response.IsSuccessStatusCode)
                return await response.LeerRespuesta2Model<ResennaCanchaModel>();

            return new Respuesta2Model<ResennaCanchaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<List<ResennaCanchaModel>>> ObtenerTodasLasResennas()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            var response = await _resennaRepository.ObtenerTodasLasResennas(token);

            if (response.IsSuccessStatusCode)
                return await response.LeerRespuesta2Model<List<ResennaCanchaModel>>();

            return new Respuesta2Model<List<ResennaCanchaModel>>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }
    }
}
