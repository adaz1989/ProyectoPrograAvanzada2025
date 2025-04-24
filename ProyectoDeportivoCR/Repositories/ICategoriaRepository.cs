namespace ProyectoDeportivoCR.Repositories
{
    public interface ICategoriaRepository
    {
        public Task<HttpResponseMessage> RegistrarCategoria( CategoriaModel model, string? token);

        public Task<HttpResponseMessage> ActualizarCategoria(CategoriaModel model, string? token);

        public Task<HttpResponseMessage> ObtenerCategorias(int categoriaId, string? token);

        public Task<HttpResponseMessage> DesabilitarCategoria(int categoriaId, string? token);

        Task<HttpResponseMessage> ObtenerTodasLasCategorias(string? token);

    }
}
