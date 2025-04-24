namespace ProyectoApi.Repositories
{
    public interface ICategoriaRepository
    {
        public Task<(int CodigoError, string Mensaje)> RegistrarCategoria(CategoriaModel model);
        public Task<(int CodigoError, string Mensaje)> ActualizarInformacionCategoria(CategoriaModel model);
        public Task<(int CodigoError, string Mensaje)> DeshabilitarCategoria(int CategoriaId);
        public Task<CategoriaModel> ObtenerCategoria(int CategoriaId);
        Task<IEnumerable<CategoriaModel>> ObtenerTodasLasCategorias();
    }
}
