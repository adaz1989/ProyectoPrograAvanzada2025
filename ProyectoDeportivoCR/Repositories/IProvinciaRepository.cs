namespace ProyectoDeportivoCR.Repositories
{
    public interface IProvinciaRepository
    {
        public Task<HttpResponseMessage> ObtenerTodasProvincias();
    }
}
