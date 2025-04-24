using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class ProvinciaService : IProvinciaService
    {
        private readonly IProvinciaRepository _repository;

        public ProvinciaService(IProvinciaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Respuesta2Model<List<ProvinciaModel>>> ObtenerTodasProvincias()
        {
            var respuesta = await _repository.ObtenerTodasProvincias();
            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<List<ProvinciaModel>>();
            }

            return new Respuesta2Model<List<ProvinciaModel>>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API.",
                Datos = null
            };
        }
    }
}
