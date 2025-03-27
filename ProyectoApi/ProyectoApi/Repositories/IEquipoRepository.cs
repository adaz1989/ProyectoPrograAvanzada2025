namespace ProyectoApi.Repositories
{
    public interface IEquipoRepository
    {
        public Task<(int CodigoError, string Mensaje)> RegistrarEquipo(EquipoModel model);
        public Task<(int CodigoError, string Mensaje)> ActualizarInformacionEquipo(EquipoModel model);
        public Task<(int CodigoError, string Mensaje)> DeshabilitarEquipo(int EquipoId);
        public Task<EquipoModel> ObtenerEquipo(int EquipoId);
    }
}
