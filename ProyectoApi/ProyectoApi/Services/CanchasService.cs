
using Microsoft.Data.SqlClient;

namespace ProyectoApi.Services
{
    public class CanchasService : ICanchasService
    {
        private readonly IJwtService _jwtService;
        private readonly ICanchasRepository _canchasRepository;

        public CanchasService(IJwtService jwtService, ICanchasRepository canchasRepository) 
        {
            _jwtService = jwtService;
            _canchasRepository = canchasRepository;
        }

        public async Task<RespuestaModel> ActualizarInformacionCanchas(CanchaModel model)
        {
            var (CodigoError, Mensaje) = await _canchasRepository.ActualizarInformacionCancha(model);

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

        public async Task<RespuestaModel> DeshabilitarCanchas(long canchaId)
        {
            var (CodigoError, Mensaje) = await _canchasRepository.DeshabilitarCancha(canchaId);

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

        public async Task<RespuestaModel> ObtenerInformacionCanchas(long canchaId)
        {
            var respuesta = new RespuestaModel();

            try
            {
                var resultado = await _canchasRepository.ObtenerCancha(canchaId);

                if (resultado != null)
                {
                    respuesta.Exito = true;
                    respuesta.Datos = resultado;
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = "No se encontró una cancha válida con ese Id"; 
                }
            }
            catch (SqlException ex)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = ex.Message;
            }

            return respuesta;
        }

        public async Task<RespuestaModel> RegistrarCanchas(CanchaModel model)
        {
            var (CodigoError, Mensaje) = await _canchasRepository.RegistrarCancha(model);

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
        public async Task<RespuestaModel> ObtenerTodasLasCanchas()
        {
            var respuesta = new RespuestaModel();

            try
            {
                var resultado = await _canchasRepository.ObtenerTodasLasCanchas();

                if (resultado != null)
                {
                    respuesta.Exito = true;
                    respuesta.Datos = resultado;
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = "No se encontró una cancha válida con ese Id";
                }
            }
            catch (SqlException ex)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = ex.Message;
            }

            return respuesta;
        }
    }
}
