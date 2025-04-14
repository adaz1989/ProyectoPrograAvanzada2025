namespace ProyectoDeportivoCR.Services
{
    public interface ICantonService
    {
        public Task<Respuesta2Model<List<CantonModel>>> ObtenerTodosCantones();
    }
}
