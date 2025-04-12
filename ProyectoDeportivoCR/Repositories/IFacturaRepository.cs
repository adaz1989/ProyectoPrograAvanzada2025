namespace ProyectoDeportivoCR.Repositories
{
    public interface IFacturaRepository
    {
        public Task<HttpResponseMessage> RegistrarFactura(FacturaModel model);

        public Task<HttpResponseMessage> ObtenerFacturaPorId(int facturaId);

        public Task<HttpResponseMessage> ObtenerTodasLasFacturas();

    }
}
