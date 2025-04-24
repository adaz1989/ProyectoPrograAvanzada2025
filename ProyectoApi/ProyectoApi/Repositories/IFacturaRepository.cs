namespace ProyectoApi.Repositories
{
    public interface IFacturaRepository
    {
        public Task<(int CodigoError, string Mensaje)> RegistrarFactura(FacturaModel model);
        public Task<FacturaModel> ObtenerFacturaPorId(long facturaId);
        Task<IEnumerable<FacturaModel>> ObtenerTodasLasFacturas();
    }
}
