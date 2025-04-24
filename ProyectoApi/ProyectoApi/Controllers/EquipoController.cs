using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly IEquipoService _equipoService;


        public EquipoController(IEquipoService equipoService)
        {
            _equipoService = equipoService;

        }

        [HttpPut]
        [Route("RegistrarEquipo")]
        public async Task<IActionResult> RegistrarEquipo(EquipoModel model)
        {
            var respuesta = await _equipoService.RegistrarEquipo(model);
            return Ok(respuesta);
        }

        [HttpPut]
        [Route("ActualizarInformacionEquipo")]
        public async Task<IActionResult> ActualizarInformacionEquipo(EquipoModel model)
        {
            var respuesta = await _equipoService.ActualizarInformacionEquipo(model);
            return Ok(respuesta);
        }


        [HttpPut]
        // Es buena practica incluir {parametro} en el Route cuando es por la URL
        [Route("DeshabilitarEquipo/{equipoId}")]
        public async Task<IActionResult> DeshabilitarEquipo(int equipoId)
        {
            var respuesta = await _equipoService.DeshabilitarEquipo(equipoId);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("ObtenerInformacionEquipo/{equipoId}")]
        public async Task<IActionResult> ObtenerInformacionEquipo(int equipoId)
        {
            var respuesta = await _equipoService.ObtenerInformacionEquipo(equipoId);
            return Ok(respuesta);
        }
    }
}
