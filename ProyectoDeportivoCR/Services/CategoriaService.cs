using System.Text.Json;
using ProyectoDeportivoCR.Services.Extensions;

namespace ProyectoDeportivoCR.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<Respuesta2Model<CategoriaModel>> RegistrarCategoria(CategoriaModel model)
        {
            var respuesta = await _categoriaRepository.RegistrarCategoria(model);

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
            var respuesta = await _categoriaRepository.ActualizarCategoria(model);

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
            var respuesta = await _categoriaRepository.ObtenerCategorias(categoriaId);

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
            var respuesta = await _categoriaRepository.DesabilitarCategoria(categoriaId);

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
    }
}
