namespace ProyectoDeportivoCR.Repositories
{
    public interface ICategoriaRepository
    {
        public Task<HttpResponseMessage> RegistrarCategoria( CategoriaModel model );

        public Task<HttpResponseMessage> ActualizarCategoria(CategoriaModel model);

        public Task<HttpResponseMessage> ObtenerCategorias(int categoriaId);

        public Task<HttpResponseMessage> DesabilitarCategoria(int categoriaId);

    }
}
