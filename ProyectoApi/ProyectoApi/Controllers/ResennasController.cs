using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models;
using ProyectoApi.Services;
using System.Threading.Tasks;

namespace ProyectoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ResennasController : ControllerBase
    {
        private readonly IResennaCanchaService _resennaService;

        public ResennasController(IResennaCanchaService resennaService)
        {
            _resennaService = resennaService;
        }

     
        [HttpPost]
        [Route("RegistrarResenna")]
        public async Task<IActionResult> RegistrarResenna([FromBody] ResennaCanchaModel model)
        {
            var respuesta = await _resennaService.RegistrarResenna(model);
            return Ok(respuesta);
        }
     
        [HttpGet]
        [Route("ObtenerResennaPorCancha/{canchaId}")]
        public async Task<IActionResult> ObtenerResennaPorCancha(long canchaId)
        {
            var respuesta = await _resennaService.ObtenerResennaPorCancha(canchaId);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("ObtenerTodasLasResennas")]
        public async Task<IActionResult> ObtenerTodasLasResennas()
        {
            var respuesta = await _resennaService.ObtenerTodasLasResennas();
            return Ok(respuesta);
        }
    }
}
