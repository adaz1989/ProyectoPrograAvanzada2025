using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FacturaService(IFacturaRepository facturaRepository, IHttpContextAccessor httpContextAccessor)
        {
            _facturaRepository = facturaRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Respuesta2Model<FacturaModel>> RegistrarFactura(FacturaModel model)
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
                    return new Respuesta2Model<FacturaModel>
                    {
                        Exito = false,
                        Mensaje = "No se encontró el UsuarioId en el token."
                    };
                }
            }

            var respuesta = await _facturaRepository.RegistrarFactura(model, token);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<FacturaModel>();
            }

            return new Respuesta2Model<FacturaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }


        public async Task<Respuesta2Model<FacturaModel>> ObtenerFacturaPorId(int facturaId)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _facturaRepository.ObtenerFacturaPorId(facturaId, token);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<FacturaModel>();
            }

            return new Respuesta2Model<FacturaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<List<FacturaModel>>> ObtenerTodasLasFacturas()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _facturaRepository.ObtenerTodasLasFacturas(token);

            if (respuesta.IsSuccessStatusCode)
            {
                string jsonResponse = await respuesta.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(jsonResponse))
                {
                    return new Respuesta2Model<List<FacturaModel>>
                    {
                        Exito = true,
                        Datos = new List<FacturaModel>()
                    };
                }
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var respuestaDeserializada = JsonSerializer.Deserialize<Respuesta2Model<List<FacturaModel>>>(jsonResponse, options);

                    return respuestaDeserializada ?? new Respuesta2Model<List<FacturaModel>>
                    {
                        Exito = false,
                        Mensaje = "Error al deserializar la respuesta."
                    };
                }
                catch (JsonException ex)
                {
                    throw new Exception("Error al deserializar las facturas: " + ex.Message);
                }
            }

            return new Respuesta2Model<List<FacturaModel>>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

    }
}
