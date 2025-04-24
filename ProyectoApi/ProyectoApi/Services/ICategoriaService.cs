namespace ProyectoApi.Services
{
    public interface ICategoriaService
    {
        public Task<RespuestaModel> RegistrarCategoria(CategoriaModel model);
        public Task<RespuestaModel> ActualizarInformacionCategoria(CategoriaModel model);
        public Task<RespuestaModel> DeshabilitarCategoria(int CategoriaId);
        public Task<RespuestaModel> ObtenerInformacionCategoria(int CategoriaId);
        public Task<RespuestaModel> ObtenerTodasLasCategorias();
    }
}
