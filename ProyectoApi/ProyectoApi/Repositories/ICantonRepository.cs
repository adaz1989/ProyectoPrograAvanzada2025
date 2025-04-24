namespace ProyectoApi.Repositories
{
    public interface ICantonRepository
    {
        public Task<(int CodigoError, string Mensaje)> RegistrarCanton(CantonModel model);
        public Task<(int CodigoError, string Mensaje)> ActualizarInformacionCanton(CantonModel model);
        public Task<CantonModel> ObtenerCanton(int CantonId);

        public Task<IEnumerable<CantonModel>> ObtenerTodosCantones();
    }
}
