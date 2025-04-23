namespace ProyectoDeportivoCR.Repositories
{
    public interface ICanchaRepository
    {
        Task<HttpResponseMessage> RegistrarCancha(CanchaModel model, string? token);
        Task<HttpResponseMessage> ActualizarInformacionCancha(CanchaModel model, string? token);
        Task<HttpResponseMessage> DeshabilitarCancha(int canchaId, string? token);
        Task<HttpResponseMessage> ObtenerCancha(int canchaId, string? token);
        Task<HttpResponseMessage> ObtenerTodasLasCanchas(string? token);
    }
}
