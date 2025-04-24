using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DeporteController : ControllerBase
    {
        private readonly IDeporteService _deporteService;

        public DeporteController(IDeporteService deporteService) 
        {
            _deporteService = deporteService;   
        }


        [HttpPut]
        [Route("RegistrarDeporte")]
        public async Task<IActionResult> RegistrarDeporte(DeporteModel model)
        {
            var respuesta = await _deporteService.RegistrarDeporte(model);
            return Ok(respuesta);
        }


        [HttpPut]
        [Route("EliminarDeporte/{deporteId}")]
        public async Task<IActionResult> EliminarDeporte(long deporteId)
        {
            var respuesta = await _deporteService.EliminarDeporte(deporteId);
            return Ok(respuesta);
        }


        [HttpPut]
        [Route("ActualizarInformacionDeporte")]
        public async Task<IActionResult> ActualizarInformacionDeporte(DeporteModel model)
        {
            var respuesta = await _deporteService.ActualizarInformacionDeporte(model);
            return Ok(respuesta);
        }


  

        [HttpGet]
        [Route("ObtenerInformacionDeporte/{DeporteId}")]
        public async Task<IActionResult> ObtenerInformacionCategoria(int DeporteId)
        {
            var respuesta = await _deporteService.ObtenerInformacionDeporte(DeporteId);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("ObtenerTodosLosDeportes")]
        public async Task<IActionResult> ObtenerTodasLasCanchas()
        {
            var respuesta = await _deporteService.ObtenerTodosLosDeportes();
            return Ok(respuesta);
        }
    }
}
