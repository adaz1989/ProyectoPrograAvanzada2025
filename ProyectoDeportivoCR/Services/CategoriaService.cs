using System.Text.Json;
using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoriaService(ICategoriaRepository categoriaRepository, IHttpContextAccessor httpContextAccessor)
        {
            _categoriaRepository = categoriaRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Respuesta2Model<CategoriaModel>> RegistrarCategoria(CategoriaModel model)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _categoriaRepository.RegistrarCategoria(model,token);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<CategoriaModel>();
            }

            return new Respuesta2Model<CategoriaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<CategoriaModel>> ActualizarCategoria(CategoriaModel model)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _categoriaRepository.ActualizarCategoria(model, token);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<CategoriaModel>();
            }

            return new Respuesta2Model<CategoriaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<CategoriaModel>> ObtenerCategorias(int categoriaId)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _categoriaRepository.ObtenerCategorias(categoriaId, token);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<CategoriaModel>();
            }

            return new Respuesta2Model<CategoriaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }

        public async Task<Respuesta2Model<CategoriaModel>> DesabilitarCategoria(int categoriaId)
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _categoriaRepository.DesabilitarCategoria(categoriaId, token);

            if (respuesta.IsSuccessStatusCode)
            {
                return await respuesta.LeerRespuesta2Model<CategoriaModel>();
            }

            return new Respuesta2Model<CategoriaModel>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API."
            };
        }
        public async Task<Respuesta2Model<List<CategoriaModel>>> ObtenerTodasLasCategorias()
        {
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");

            var respuesta = await _categoriaRepository.ObtenerTodasLasCategorias(token);
            if (respuesta.IsSuccessStatusCode)
            {

                return await respuesta.LeerRespuesta2Model<List<CategoriaModel>>();
            }

            return new Respuesta2Model<List<CategoriaModel>>
            {
                Exito = false,
                Mensaje = "Error al comunicarse con la API.",
                Datos = null
            };
        }
    }
}
