
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace ProyectoApi.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IJwtService _jwtService;

        public CategoriaService(ICategoriaRepository categoriaRepository, IJwtService jwtService)
        {
            _jwtService = jwtService;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<RespuestaModel> ActualizarInformacionCategoria(CategoriaModel model) 
        {
            var (CodigoError, Mensaje) = await _categoriaRepository.ActualizarInformacionCategoria(model);

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

        public async Task<RespuestaModel> DeshabilitarCategoria(int CategoriaId)
        {
            var (CodigoError, Mensaje) = await _categoriaRepository.DeshabilitarCategoria(CategoriaId);

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


        public async Task<RespuestaModel> ObtenerInformacionCategoria(int CategoriaId)
        {
            var resultado = await _categoriaRepository.ObtenerCategoria(CategoriaId);

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

        public async Task<RespuestaModel> RegistrarCategoria(CategoriaModel model)
        {
            var (CodigoError, Mensaje) = await _categoriaRepository.RegistrarCategoria(model);

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
        public async Task<RespuestaModel> ObtenerTodasLasCategorias()
        {
            var respuesta = new RespuestaModel();

            try
            {
                var resultado = await _categoriaRepository.ObtenerTodasLasCategorias();

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
