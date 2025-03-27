namespace ProyectoDeportivoCR.Services
{
    public interface ICategoriaService
    {
        public Task<Respuesta2Model<CategoriaModel>> RegistrarCategoria(CategoriaModel model);

        public Task<Respuesta2Model<CategoriaModel>> ActualizarCategoria(CategoriaModel model);

        public Task<Respuesta2Model<CategoriaModel>> ObtenerCategorias(int categoriaId);

        public Task<Respuesta2Model<CategoriaModel>> DesabilitarCategoria(int categoriaId);
    }
}
