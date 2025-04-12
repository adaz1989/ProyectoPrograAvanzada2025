namespace ProyectoApi.Repositories
{
    public interface IDistritoRepository
    {
        public Task<IEnumerable<DistritoModel>> ObtenerTodosDistritos();
    }
}
