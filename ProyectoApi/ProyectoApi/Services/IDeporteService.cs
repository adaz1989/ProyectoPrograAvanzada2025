namespace ProyectoApi.Services
{
    public interface IDeporteService
    {
        public Task<RespuestaModel> RegistrarDeporte(DeporteModel model);
        public Task<RespuestaModel> ActualizarInformacionDeporte(DeporteModel model);

        public Task<RespuestaModel> EliminarDeporte(long deporteId);

        public Task<RespuestaModel> ObtenerInformacionDeporte(long deporteId);

        public Task<RespuestaModel> ObtenerTodosLosDeportes();
    }
}
