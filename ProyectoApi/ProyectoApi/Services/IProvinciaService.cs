namespace ProyectoApi.Services
{
    public interface IProvinciaService
    {

        public Task<RespuestaModel> RegistrarProvincia(ProvinciaModel model);
        public Task<RespuestaModel> ActualizarInformacionProvincia(ProvinciaModel model);
        public Task<RespuestaModel> ObtenerInformacionProvincia(int ProvinciaId);
        public Task<RespuestaModel> ObtenerTodasProvincias();
    }
}
