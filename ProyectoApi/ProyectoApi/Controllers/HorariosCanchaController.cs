using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosCanchaController : Controller
    {
        private readonly IHorariosCanchasService _horariosCanchasService;
        private readonly IJwtService _jwtService;

        public HorariosCanchaController(IHorariosCanchasService horariosCanchasService, IJwtService jwtService)
        {
            _horariosCanchasService = horariosCanchasService;
            _jwtService = jwtService;
        }

        [HttpGet]
        [Route("ObtenerHorariosCancha/{canchaId}")]
        public async Task<IActionResult> ObtenerHorariosCancha(long canchaId)
        {
            var respuesta = await _horariosCanchasService.ObtenerHorariosCancha(canchaId);
            return Ok(respuesta);
        }

        [HttpPost]
        [Route("RegistrarHorarioCancha")]
        public async Task<IActionResult> RegistrarHorarioCancha(HorarioCanchaModel model)
        {            
            var respuesta = await _horariosCanchasService.RegistrarHorarioCancha(model);
            return Ok(respuesta);
        }
    }
}

