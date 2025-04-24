using System.Text.Json;
using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class DeporteService : IDeporteService
    {
        private readonly IDeporteRepository _deporteRepository;

        public DeporteService(IDeporteRepository deporteRepository)
        {
            _deporteRepository = deporteRepository;
        }

        public async Task<Respuesta2Model<DeporteModel>> RegistrarDeporte(DeporteModel model)
        {
            var respuesta = await _deporteRepository.RegistrarDeporte(model);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<DeporteModel>();
            }

            return new Respuesta2Model<DeporteModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<DeporteModel>> ObtenerInformacionDeporte(int deporteId)
        {
            var respuesta = await _deporteRepository.ObtenerInformacionDeporte(deporteId);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<DeporteModel>();
            }

            return new Respuesta2Model<DeporteModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<List<DeporteModel>>> ObtenerTodosLosDeportes()
        {
            var respuesta = await _deporteRepository.ObtenerTodosLosDeportes();
            if (respuesta.IsSuccessStatusCode)
            {

                return await respuesta.LeerRespuesta2Model<List<DeporteModel>>();
            }

            return new Respuesta2Model<List<DeporteModel>>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API.",
                Datos = null
            };
        }
    }
}
