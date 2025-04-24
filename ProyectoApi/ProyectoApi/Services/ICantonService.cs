namespace ProyectoApi.Services
{
    public interface ICantonService
    {
        public Task<RespuestaModel> RegistrarCanton(CantonModel model);
        public Task<RespuestaModel> ActualizarInformacionCanton(CantonModel model);
        public Task<RespuestaModel> ObtenerInformacionCanton(int CantonId);

        public Task<RespuestaModel> ObtenerTodosCantones();
    }
}
