﻿
using Microsoft.Data.SqlClient;

namespace ProyectoApi.Services
{
    public class CantonService : ICantonService
    {

        private readonly ICantonRepository _cantonRepository;
        private readonly IJwtService _jwtService;


        public CantonService(ICantonRepository cantonRepository, IJwtService jwtService) 
        {
            _cantonRepository = cantonRepository;
            _jwtService = jwtService;
        }


        public async Task<RespuestaModel> ActualizarInformacionCanton(CantonModel model)
        {
            var (CodigoError, Mensaje) = await _cantonRepository.ActualizarInformacionCanton(model);

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

        public async Task<RespuestaModel> ObtenerInformacionCanton(int CantonId)
        {
            var resultado = await _cantonRepository.ObtenerCanton(CantonId);

            var respuesta = new RespuestaModel();

            if (resultado != null)
            {
                respuesta.Exito = true;
                respuesta.Datos = resultado;
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "No se encontro un usuario valido con ese Id";
            }
            return (respuesta);
        }

        public async Task<RespuestaModel> ObtenerTodosCantones()
        {
            var respuesta = new RespuestaModel();

            try
            {
                var resultado = await _cantonRepository.ObtenerTodosCantones();

                if (resultado != null && resultado.Any())
                {
                    respuesta.Exito = true;
                    respuesta.Datos = resultado;
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = "No hay cantones registrados.";
                }
            }
            catch (SqlException ex)
            {
                respuesta.Exito = false;
                respuesta.Mensaje = $"Error SQL: {ex.Message}";
            }

            return respuesta;
        }

        public async Task<RespuestaModel> RegistrarCanton(CantonModel model)
        {
            var (CodigoError, Mensaje) = await _cantonRepository.RegistrarCanton(model);

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

        public async Task<IEnumerable<CantonModel>> ObtenerCantonesPorProvincia(int provinciaId)
        {
            return await _cantonRepository.ObtenerCantonesPorProvincia(provinciaId);
        }

    }
}
