namespace ProyectoDeportivoCR.Repositories
{
    public interface IDistritoRepository
    {
        public Task<HttpResponseMessage> ObtenerTodosDistritos();
    }
}
