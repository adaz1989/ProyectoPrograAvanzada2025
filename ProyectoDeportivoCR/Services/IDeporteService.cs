namespace ProyectoDeportivoCR.Services
{
    public interface IDeporteService
    {
        public Task<Respuesta2Model<DeporteModel>> RegistrarDeporte(DeporteModel model);

        public Task<Respuesta2Model<DeporteModel>> ObtenerInformacionDeporte(int deporteId);

    }
}
