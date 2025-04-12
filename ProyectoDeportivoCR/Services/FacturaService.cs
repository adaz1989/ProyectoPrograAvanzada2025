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
                return await respuesta.LeerRespuesta2Model<List<FacturaModel>>(); 
            }

            return new Respuesta2Model<List<FacturaModel>>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }
    }
}
