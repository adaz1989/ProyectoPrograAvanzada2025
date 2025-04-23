namespace ProyectoDeportivoCR.Repositories
{
    public interface IFacturaRepository
    {
        public Task<HttpResponseMessage> RegistrarFactura(FacturaModel model,string? token);

        public Task<HttpResponseMessage> ObtenerFacturaPorId(int facturaId,string? token);

        public Task<HttpResponseMessage> ObtenerTodasLasFacturas(string? token);

    }
}
