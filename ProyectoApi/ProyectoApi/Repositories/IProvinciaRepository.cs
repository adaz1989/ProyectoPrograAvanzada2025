namespace ProyectoApi.Repositories
{
    public interface IProvinciaRepository
    {
        public Task<(int CodigoError, string Mensaje)> RegistrarProvincia(ProvinciaModel model);
        public Task<(int CodigoError, string Mensaje)> ActualizarInformacionProvincia(ProvinciaModel model);
        public Task<ProvinciaModel> ObtenerProvincia(long ProvinciaId);

        public Task<IEnumerable<ProvinciaModel>> ObtenerTodasProvincias();
    }
}
