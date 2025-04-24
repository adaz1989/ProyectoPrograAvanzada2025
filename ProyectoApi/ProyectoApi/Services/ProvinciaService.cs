
using Microsoft.Data.SqlClient;

namespace ProyectoApi.Services
{
    public class ProvinciaService : IProvinciaService
    {

        private readonly IProvinciaRepository _repository;
        private readonly IJwtService _jwtService;


        public ProvinciaService(IProvinciaRepository provinciaRepository, IJwtService jwtService) 
        {
            _repository = provinciaRepository;
            _jwtService = jwtService;
        }

        public async Task<RespuestaModel> ActualizarInformacionProvincia(ProvinciaModel model)
        {
            var (CodigoError, Mensaje) = await _repository.ActualizarInformacionProvincia(model);

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

        public async Task<RespuestaModel> ObtenerInformacionProvincia(int ProvinciaId)
        {
            var resultado = await _repository.ObtenerProvincia(ProvinciaId);

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

        public async Task<RespuestaModel> ObtenerTodasProvincias()
        {
            var respuesta = new RespuestaModel();

            try
            {
                var resultado = await _repository.ObtenerTodasProvincias();

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

        public async Task<RespuestaModel> RegistrarProvincia(ProvinciaModel model)
        {
            try
            {
                var (CodigoError, Mensaje) = await _repository.RegistrarProvincia(model);

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
            catch (Exception ex)
            {
      
                Console.WriteLine($"Error en RegistrarProvincia: {ex.ToString()}");

   
                string detalleInterno = ex.InnerException != null ? $" Detalle interno: {ex.InnerException.Message}" : "";

                return new RespuestaModel
                {
                    Exito = false,
                    Mensaje = $"Error en el servidor: {ex.Message}{detalleInterno}"
                };
            }
        }
    }
}
