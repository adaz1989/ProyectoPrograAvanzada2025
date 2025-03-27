using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaService _provinciaService;

        public ProvinciaController(IProvinciaService provinciaService)
        {
            _provinciaService = provinciaService;
        }

        [HttpPut]
        [Route("RegistrarProvincia")]
        public async Task<IActionResult> RegistrarProvincia(ProvinciaModel model)
        {
            var respuesta = await _provinciaService.RegistrarProvincia(model);
            return Ok(respuesta);
        }

        [HttpPut]
        [Route("ActualizarInformacionProvincia")]
        public async Task<IActionResult> ActualizarInformacionProvincia(ProvinciaModel model)
        {
            var respuesta = await _provinciaService.ActualizarInformacionProvincia(model);
            return Ok(respuesta);
        }
        [HttpGet]
        [Route("ObtenerInformacionProvincia/{provinciaId}")]
        public async Task<IActionResult> ObtenerInformacionProvincia([FromRoute] int provinciaId)
        {
            var respuesta = await _provinciaService.ObtenerInformacionProvincia(provinciaId);
            return Ok(respuesta);
        }
    }

}

