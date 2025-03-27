using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CantonController : ControllerBase
    {

        private readonly ICantonService _cantonService;

        public CantonController(ICantonService cantonService) 
        {
            _cantonService = cantonService;
        }


        [HttpPut]
        [Route("RegistrarCanton")]
        public async Task<IActionResult> RegistrarCanton(CantonModel model)
        {
            var respuesta = await _cantonService.RegistrarCanton(model);
            return Ok(respuesta);
        }


        [HttpPut]
        [Route("ActualizarInformacionCanton")]
        public async Task<IActionResult> ActualizarInformacionCanton(CantonModel model)
        {
            var respuesta = await _cantonService.ActualizarInformacionCanton(model);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("ObtenerInformacionCanton/{cantonId}")]
        public async Task<IActionResult> ObtenerInformacionCanton(int cantonId)
        {
            var respuesta = await _cantonService.ObtenerInformacionCanton(cantonId);
            return Ok(respuesta);
        }


    }
}
