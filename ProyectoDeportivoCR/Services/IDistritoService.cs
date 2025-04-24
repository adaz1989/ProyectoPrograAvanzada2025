namespace ProyectoDeportivoCR.Services
{
    public interface IDistritoService
    {
        public Task<Respuesta2Model<List<DistritoModel>>> ObtenerTodosDistritos();
    }
}
