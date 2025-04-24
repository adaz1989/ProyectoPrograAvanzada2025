namespace ProyectoApi.Services
{
    public interface IResennaCanchaService
    {
        public Task<RespuestaModel> RegistrarResenna(ResennaCanchaModel model);
        public Task<RespuestaModel> ObtenerResennaPorCancha(long canchaId);
        public Task<RespuestaModel> ObtenerTodasLasResennas();
    }
}
