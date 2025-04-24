using Microsoft.Data.SqlClient;
using ProyectoApi.Models;
using ProyectoApi.Repositories;

namespace ProyectoApi.Services
{
    public class ResennaCanchaService : IResennaCanchaService
    {
        private readonly IJwtService _jwtService;
        private readonly IResennaCanchaRepository _resennaRepository;

        public ResennaCanchaService(
            IJwtService jwtService,
            IResennaCanchaRepository resennaRepository)
        {
            _jwtService = jwtService;
            _resennaRepository = resennaRepository;
        }

        public async Task<RespuestaModel> ObtenerResennaPorCancha(long canchaId)
        {
            var respuesta = new RespuestaModel();

            try
            {
                var resultado = await _resennaRepository.ObtenerResennaPorCancha(canchaId);
                if (resultado != null)
                {
                    respuesta.Exito = true;
                    respuesta.Datos = resultado;
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = "No se encontró ninguna reseña para la cancha especificada.";
                }
            }
            catch (SqlException ex)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = ex.Message;
            }

            return respuesta;
        }

        public async Task<RespuestaModel> ObtenerTodasLasResennas()
        {
            var respuesta = new RespuestaModel();

            try
            {
                var resultado = await _resennaRepository.ObtenerTodasLasResennas();
                if (resultado != null && resultado.Any())
                {
                    respuesta.Exito = true;
                    respuesta.Datos = resultado;
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = "No hay reseñas registradas.";
                }
            }
            catch (SqlException ex)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = ex.Message;
            }

            return respuesta;
        }

        public async Task<RespuestaModel> RegistrarResenna(ResennaCanchaModel model)
        {
            var respuesta = new RespuestaModel();

            try
            {
                var (CodigoError, Mensaje) = await _resennaRepository.RegistrarResenna(model);

                respuesta = CodigoError switch
                {
                    0 => new RespuestaModel { Exito = true, Mensaje = Mensaje },
                    1 => new RespuestaModel { Exito = false, Mensaje = Mensaje },
                    _ => new RespuestaModel
                    {
                        Exito = false,
                        Mensaje = "Error inesperado en la base de datos."
                    }
                };
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
