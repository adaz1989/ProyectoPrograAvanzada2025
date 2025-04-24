namespace ProyectoDeportivoCR.Services
{
    public interface IProvinciaService
    {
    public Task<Respuesta2Model<List<ProvinciaModel>>> ObtenerTodasProvincias();
    }
}
