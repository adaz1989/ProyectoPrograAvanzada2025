namespace ProyectoApi.Repositories
{
    public interface IDistritoRepository
    {
        public Task<IEnumerable<DistritoModel>> ObtenerTodosDistritos();
        public Task<IEnumerable<DistritoModel>> ObtenerDistritosPorCanton(int cantonId);
    }
}
