

namespace ProyectoApi.Services
{
    public class FacturasService : IFacturaService
    {

        private readonly IFacturaRepository _facturaRepository;


        public FacturasService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }


        public async Task<RespuestaModel> ObtenerFacturaPorId(long facturaId)
        {
            // Se obtiene la factura mediante el repositorio
            var resultado = await _facturaRepository.ObtenerFacturaPorId(facturaId);
            var respuesta = new RespuestaModel();

            if (resultado != null)
            {
                respuesta.Exito = true;
                respuesta.Datos = resultado;
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "No se encontró una factura válida con ese Id";
            }
            return respuesta;
        }

        public async Task<RespuestaModel> ObtenerTodasLasFacturas()
        {
            // Se obtienen todas las facturas a través del repositorio
            var resultado = await _facturaRepository.ObtenerTodasLasFacturas();
            var respuesta = new RespuestaModel();

            if (resultado != null && resultado.Any())
            {
                respuesta.Exito = true;
                respuesta.Datos = resultado;
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "No se encontraron facturas.";
            }
            return respuesta;
        }

        public async Task<RespuestaModel> RegistrarFactura(FacturaModel model)
        {
            // Se llama al repositorio para registrar la factura y se recibe un tuple con (CodigoError, Mensaje)
            var (CodigoError, Mensaje) = await _facturaRepository.RegistrarFactura(model);

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
