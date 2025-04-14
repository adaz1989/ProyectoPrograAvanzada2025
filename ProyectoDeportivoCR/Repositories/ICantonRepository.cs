namespace ProyectoDeportivoCR.Repositories
{
    public interface ICantonRepository
    {
        public Task<HttpResponseMessage> ObtenerTodosCantones();
    }
}
