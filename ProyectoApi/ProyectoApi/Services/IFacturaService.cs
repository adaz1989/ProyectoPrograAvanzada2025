namespace ProyectoApi.Services
{
    public interface IFacturaService
    {
        public Task<RespuestaModel> RegistrarFactura(FacturaModel model);
        public Task<RespuestaModel> ObtenerFacturaPorId(long facturaId);
        public Task<RespuestaModel> ObtenerTodasLasFacturas();
    }
}
