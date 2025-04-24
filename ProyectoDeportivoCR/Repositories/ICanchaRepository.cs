namespace ProyectoDeportivoCR.Repositories
{
    public interface ICanchaRepository
    {
        Task<HttpResponseMessage> RegistrarCancha(CanchaModel model, string? token);
        Task<HttpResponseMessage> ActualizarInformacionCancha(CanchaModel model, string? token);
        Task<HttpResponseMessage> DeshabilitarCancha(long canchaId, string? token);
        Task<HttpResponseMessage> ObtenerCancha(long canchaId, string? token);
        Task<HttpResponseMessage> ObtenerTodasLasCanchas(string? token);
    }
}
