namespace ProyectoDeportivoCR.Repositories
{
    public interface IResennaRepository
    {
        public Task<HttpResponseMessage> RegistrarResenna(ResennaCanchaModel model, string? token);

        public Task<HttpResponseMessage> ObtenerResennaPorCancha(long canchaId, string? token);

        public Task<HttpResponseMessage> ObtenerTodasLasResennas(string? token);
    }
}
