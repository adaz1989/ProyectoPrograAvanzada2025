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
        private readonly ICantonService _cantonService;
        private readonly IDistritoService _distritoService;

        public ProvinciaController(IProvinciaService provinciaService, ICantonService cantonService, IDistritoService distritoService)
        {
            _provinciaService = provinciaService;
            _cantonService = cantonService;
            _distritoService = distritoService;
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

        [HttpGet]
        [Route("ObtenerTodasProvincias")]
        public async Task<IActionResult> ObtenerTodasProvincias()
        {
            var respuesta = await _provinciaService.ObtenerTodasProvincias();
            return Ok(respuesta);
        }

        [HttpGet("Cantones/{provinciaId}")]
        public async Task<IActionResult> ObtenerCantones(int provinciaId)
        {
            var cantones = await _cantonService.ObtenerCantonesPorProvincia(provinciaId);
            return Ok(cantones);
        }

        [HttpGet("Distritos/{cantonId}")]
        public async Task<IActionResult> ObtenerDistritos(int cantonId)
        {
            var distritos = await _distritoService.ObtenerDistritosPorCanton(cantonId);
            return Ok(distritos);
        }

    }

}

