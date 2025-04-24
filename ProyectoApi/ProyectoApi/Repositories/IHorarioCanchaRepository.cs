namespace ProyectoApi.Repositories
{
    public interface IHorarioCanchaRepository
    {
        public Task<IEnumerable<HorarioCanchaModel>> ObtenerHorariosCancha(long canchaId);
        public Task<(int CodigoError, string Mensaje)> RegistrarHorarioCancha(HorarioCanchaModel model);
    }
}
