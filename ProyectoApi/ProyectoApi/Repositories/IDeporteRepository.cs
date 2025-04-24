namespace ProyectoApi.Repositories
{
    public interface IDeporteRepository
    {
        public Task<(int CodigoError, string Mensaje)> RegistrarDeporte(DeporteModel model);
        public Task<(int CodigoError, string Mensaje)> ActualizarInformacionDeporte(DeporteModel model);

        public Task<(int CodigoError, string Mensaje)> EliminarDeporte(long deporteId);

        public Task<DeporteModel> ObtenerDeporte(long DeporteId);

        Task<IEnumerable<DeporteModel>> ObtenerTodosLosDeportes();


    }
}
