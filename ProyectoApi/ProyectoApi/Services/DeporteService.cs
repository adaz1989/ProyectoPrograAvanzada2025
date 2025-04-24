
namespace ProyectoApi.Services
{
    public class DeporteService : IDeporteService
    {

        private readonly IDeporteRepository _deporteRepository;
        private readonly IJwtService _jwtService;


        public DeporteService(IDeporteRepository deporteRepository, IJwtService jwtService) 
        {
            _deporteRepository = deporteRepository;
            _jwtService = jwtService;
        }
        public async Task<RespuestaModel> ActualizarInformacionDeporte(DeporteModel model)
        {
            var (CodigoError, Mensaje) = await _deporteRepository.ActualizarInformacionDeporte(model);

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


        public async Task<RespuestaModel> EliminarDeporte(long deporteId)
        {
            var (CodigoError, Mensaje) = await _deporteRepository.EliminarDeporte(deporteId);

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



        public async Task<RespuestaModel> ObtenerInformacionDeporte(long deporteId)
        {
            var resultado = await _deporteRepository.ObtenerDeporte(deporteId);

            var respuesta = new RespuestaModel();

            if (resultado != null)
            {
                respuesta.Exito = true;
                respuesta.Datos = resultado;
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "No se encontro un objeto valido con ese Id";
            }
            return (respuesta);
        }

        public Task<RespuestaModel> ObtenerInformacionDeporte(int DeporteId)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaModel> ObtenerTodosLosDeportes()
        {
            var resultado = await _deporteRepository.ObtenerTodosLosDeportes();

            var respuesta = new RespuestaModel();

            if (resultado != null)
            {
                respuesta.Exito = true;
                respuesta.Datos = resultado;
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "No se encontro un objeto valido con ese Id";
            }

            return (respuesta);
        }

        public async Task<RespuestaModel> RegistrarDeporte(DeporteModel model)
        {
            var (CodigoError, Mensaje) = await _deporteRepository.RegistrarDeporte(model);

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
    }
}
