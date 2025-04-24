using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class DistritoService : IDistritoService
    {
        private readonly IDistritoRepository _repository;

        public DistritoService(IDistritoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Respuesta2Model<List<DistritoModel>>> ObtenerTodosDistritos()
        {
            // Se corrige el método invocado: usar ObtenerTodosDistritos en lugar de ObtenerTodasLasCanchas
            var respuesta = await _repository.ObtenerTodosDistritos();
            if (respuesta.IsSuccessStatusCode)
            {
                // Se espera que se retorne una lista de DistritoModel
                return await respuesta.LeerRespuesta2Model<List<DistritoModel>>();
            }

            return new Respuesta2Model<List<DistritoModel>>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API.",
                Datos = null
            };
        }
    }
}
