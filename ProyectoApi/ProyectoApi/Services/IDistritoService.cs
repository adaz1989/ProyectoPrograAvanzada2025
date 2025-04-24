namespace ProyectoApi.Services
{
    public interface IDistritoService
    {
        public Task<RespuestaModel> ObtenerTodosDistritos();
        public Task<IEnumerable<DistritoModel>> ObtenerDistritosPorCanton(int cantonId);

    }
}
