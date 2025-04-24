using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class CantonService : ICantonService
    {
        private readonly ICantonRepository _repository;

        public CantonService(ICantonRepository repository)
        {
            _repository = repository;
        }

        public async Task<Respuesta2Model<List<CantonModel>>> ObtenerTodosCantones()
        {
            var respuesta = await _repository.ObtenerTodosCantones();
            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<List<CantonModel>>();
            }

            return new Respuesta2Model<List<CantonModel>>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API.",
                Datos = null
            };
        }
    }
}
