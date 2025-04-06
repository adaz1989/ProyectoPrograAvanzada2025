using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class CanchaService : ICanchaService
    {
        private readonly ICanchaRepository _canchaRepository;

        public CanchaService(ICanchaRepository canchaRepository)
        {
            _canchaRepository = canchaRepository;
        }

        public async Task<Respuesta2Model<CanchaModel>> RegistrarCancha(CanchaModel model)
        {
            var respuesta = await _canchaRepository.RegistrarCancha(model);

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
            var respuesta = await _canchaRepository.ActualizarInformacionCancha(model);

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

        public async Task<Respuesta2Model<CanchaModel>> ObtenerCancha(int canchaId)
        {
            var respuesta = await _canchaRepository.ObtenerCancha(canchaId);

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

        public async Task<Respuesta2Model<CanchaModel>> DeshabilitarCancha(int canchaId)
        {
            var respuesta = await _canchaRepository.DeshabilitarCancha(canchaId);

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
            var respuesta = await _canchaRepository.ObtenerTodasLasCanchas();
            if (respuesta.IsSuccessStatusCode)
            {
                // Suponiendo que este método deserializa correctamente a Respuesta2Model<List<CanchaModel>>
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
