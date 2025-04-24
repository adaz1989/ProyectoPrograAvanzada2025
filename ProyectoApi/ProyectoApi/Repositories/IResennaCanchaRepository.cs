namespace ProyectoApi.Repositories
{
    public interface IResennaCanchaRepository
    {
        public Task<(int CodigoError, string Mensaje)> RegistrarResenna(ResennaCanchaModel model);
        public Task<ResennaCanchaModel> ObtenerResennaPorCancha(long canchaId);
        Task<IEnumerable<ResennaCanchaModel>> ObtenerTodasLasResennas();
    }
}
