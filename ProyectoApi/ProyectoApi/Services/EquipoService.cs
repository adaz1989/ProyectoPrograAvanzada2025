
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace ProyectoApi.Services
{
    public class EquipoService : IEquipoService
    {
        private readonly IJwtService _jwtService;
        private readonly IEquipoRepository _equipoRepository;

        public EquipoService(IJwtService jwtService, IEquipoRepository equipoRepository)
        {
            _jwtService = jwtService;
            _equipoRepository = equipoRepository;
        }

        public async Task<RespuestaModel> ActualizarInformacionEquipo(EquipoModel model)
        {
            var (CodigoError, Mensaje) = await _equipoRepository.ActualizarInformacionEquipo(model);

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

        public async Task<RespuestaModel> DeshabilitarEquipo(int equipoId)
        {
            var (CodigoError, Mensaje) = await _equipoRepository.DeshabilitarEquipo(equipoId);

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

        public async Task<RespuestaModel> ObtenerInformacionEquipo(int equipoId)
        {
            var respuesta = new RespuestaModel();

            try
            {
                var resultado = await _equipoRepository.ObtenerEquipo(equipoId);

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
    

        public async Task<RespuestaModel> RegistrarEquipo(EquipoModel model)
        {
            var (CodigoError, Mensaje) = await _equipoRepository.RegistrarEquipo(model);

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
