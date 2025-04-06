namespace ProyectoApi.Services
{
    public interface ICanchasService
    {
        public Task<RespuestaModel> RegistrarCanchas(CanchaModel model);
        public Task<RespuestaModel> ActualizarInformacionCanchas(CanchaModel model);
        public Task<RespuestaModel> DeshabilitarCanchas(long canchaId);
        public Task<RespuestaModel> ObtenerInformacionCanchas(long canchaId);
        public Task<RespuestaModel> ObtenerTodasLasCanchas();


    }
}
