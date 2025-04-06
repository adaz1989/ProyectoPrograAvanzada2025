namespace ProyectoDeportivoCR.Services
{
    public interface ICanchaService
    {
        public Task<Respuesta2Model<CanchaModel>> RegistrarCancha(CanchaModel model);

        public Task<Respuesta2Model<CanchaModel>> ActualizarInformacionCancha(CanchaModel model);

        public Task<Respuesta2Model<CanchaModel>> DeshabilitarCancha(int canchaId);

        public Task<Respuesta2Model<CanchaModel>> ObtenerCancha(int canchaId);

        public Task<Respuesta2Model<List<CanchaModel>>> ObtenerTodasLasCanchas();


    }
}
