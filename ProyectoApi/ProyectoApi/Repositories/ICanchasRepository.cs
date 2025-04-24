namespace ProyectoApi.Repositories
{
    public interface ICanchasRepository
    {
        public Task<(int CodigoError, string Mensaje)> RegistrarCancha(CanchaModel model);
        public Task<(int CodigoError, string Mensaje)> ActualizarInformacionCancha(CanchaModel model);
        public Task<(int CodigoError, string Mensaje)> DeshabilitarCancha(long canchaId);
        public Task<CanchaModel> ObtenerCancha(long canchaId);
        Task<IEnumerable<CanchaModel>> ObtenerTodasLasCanchas(); 
    }
}
