namespace ProyectoApi.Services
{
    public interface IEquipoService
    {
        public Task<RespuestaModel> RegistrarEquipo(EquipoModel model);
        public Task<RespuestaModel> ActualizarInformacionEquipo(EquipoModel model);
        public Task<RespuestaModel> DeshabilitarEquipo(int equipoId);
        public Task<RespuestaModel> ObtenerInformacionEquipo(int equipoId);
    }
}
