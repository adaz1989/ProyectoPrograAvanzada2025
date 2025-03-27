using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService) 
        {
            _categoriaService = categoriaService;
        }

        [HttpPut]
        [Route("RegistrarCategoria")]
        public async Task<IActionResult> RegistrarCategoria(CategoriaModel model)
        {
            var respuesta = await _categoriaService.RegistrarCategoria(model);
            return Ok(respuesta);
        }


        [HttpPut]
        [Route("ActualizarInformacionCategoria")]
        public async Task<IActionResult> ActualizarInformacionCategoria(CategoriaModel model)
        {
            var respuesta = await _categoriaService.ActualizarInformacionCategoria(model);
            return Ok(respuesta);
        }


        [HttpPut]
        // Es buena practica incluir {parametro} en el Route cuando es por la URL
        [Route("DeshabilitarCategoria/{categoriaId}")]
        public async Task<IActionResult> DeshabilitarCategoria(int categoriaId)
        {
            var respuesta = await _categoriaService.DeshabilitarCategoria(categoriaId);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("ObtenerInformacionCategoria/{categoriaId}")]
        public async Task<IActionResult> ObtenerInformacionCategoria(int categoriaId)
        {
            var respuesta = await _categoriaService.ObtenerInformacionCategoria(categoriaId);
            return Ok(respuesta);
        }



    }
}
