namespace ProyectoDeportivoCR.Repositories
{
    public interface IDeporteRepository
    {
        public Task<HttpResponseMessage> RegistrarDeporte(DeporteModel model);

        public Task<HttpResponseMessage> ObtenerInformacionDeporte(int deporteId);

        public Task<HttpResponseMessage> ObtenerTodosLosDeportes();
    }
}
