using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models;
using ProyectoApi.Services;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistritoController : ControllerBase
    {
        private readonly IDistritoService _distritoService;

        // Inyección de dependencias del servicio
        public DistritoController(IDistritoService distritoService)
        {
            _distritoService = distritoService;
        }

        [HttpGet]
        [Route("ObtenerTodosDistritos")]
        public async Task<IActionResult> ObtenerTodosDistritos()
        {
            var respuesta = await _distritoService.ObtenerTodosDistritos();
            return Ok(respuesta);
        }
    }
}