namespace ProyectoDeportivoCR.Repositories
{
    public interface ICanchaRepository
    {
        public Task<HttpResponseMessage> RegistrarCancha(CanchaModel model);

        public Task<HttpResponseMessage> ActualizarInformacionCancha(CanchaModel model);

        public Task<HttpResponseMessage> DeshabilitarCancha(int canchaId);

        public Task<HttpResponseMessage> ObtenerCancha(int canchaId);
    }
}
