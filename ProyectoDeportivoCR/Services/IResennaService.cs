namespace ProyectoDeportivoCR.Services
{
    public interface IResennaService
    {
        Task<Respuesta2Model<ResennaCanchaModel>> RegistrarResenna(ResennaCanchaModel model);
        Task<Respuesta2Model<ResennaCanchaModel>> ObtenerResennaPorCancha(long canchaId);
        Task<Respuesta2Model<List<ResennaCanchaModel>>> ObtenerTodasLasResennas();
    }
}
