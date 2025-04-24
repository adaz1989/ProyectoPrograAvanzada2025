using ProyectoApi.Models;
using ProyectoApi.Repositories;
using Microsoft.Data.SqlClient;

namespace ProyectoApi.Services
{
    public class DistritoService : IDistritoService
    {
        private readonly IJwtService _jwtService;
        private readonly IDistritoRepository _repository;

        // Constructor con inyección de dependencias
        public DistritoService(
            IDistritoRepository repository,
            IJwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public async Task<RespuestaModel> ObtenerTodosDistritos()
        {
            var respuesta = new RespuestaModel();

            try
            {
                // Obtener datos del repositorio
                var resultado = await _repository.ObtenerTodosDistritos();

                if (resultado != null && resultado.Any())
                {
                    respuesta.Exito = true;
                    respuesta.Datos = resultado;
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = "No se encontraron distritos registrados.";
                }
            }
            catch (SqlException ex)
            {
                // Manejo de errores específicos de SQL
                respuesta.Exito = false;
                respuesta.Mensaje = $"Error de base de datos: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Manejo de errores genéricos
                respuesta.Exito = false;
                respuesta.Mensaje = $"Error interno: {ex.Message}";
            }

            return respuesta;
        }
    }
}