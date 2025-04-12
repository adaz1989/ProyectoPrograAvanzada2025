using System.Text.Json;
using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<Respuesta2Model<FacturaModel>> RegistrarFactura(FacturaModel model)
        {
            var respuesta = await _facturaRepository.RegistrarFactura(model);

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
            var respuesta = await _facturaRepository.ObtenerFacturaPorId(facturaId);

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
            var respuesta = await _facturaRepository.ObtenerTodasLasFacturas();

            if (respuesta.IsSuccessStatusCode)
            {
                string jsonResponse = await respuesta.Content.ReadAsStringAsync();
                Console.WriteLine("JSON Response: " + jsonResponse);

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
