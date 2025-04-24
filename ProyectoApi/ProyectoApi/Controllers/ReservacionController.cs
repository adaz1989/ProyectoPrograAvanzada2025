using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionController : ControllerBase
    {
        private readonly IReservacionService _reservacionService;
        private readonly IJwtService _jwtService;
        public ReservacionController(IReservacionService reservacionService, IJwtService jwtService)
        {            
            _reservacionService = reservacionService;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("RegistrarReservacion")]
        public async Task<IActionResult> RegistrarReservacion(ReservacionCanchaModel model)
        {
            var respuesta = await _reservacionService.RegistrarReservacion(model);
            return Ok(respuesta);
        }


        [HttpGet]
        [Route("ObtenerReservacionesPorFecha/{fecha}/{canchaId}")]
        public async Task<IActionResult> ObtenerReservacionesPorFecha(DateTime fecha, long canchaId)
        {
            var respuesta = await _reservacionService.ObtenerReservacionesPorFecha(fecha, canchaId);
            return Ok(respuesta);
        }

        [HttpPut]
        [Route("DeshabilitarReservacion/{reservacionId}")]
        public async Task<IActionResult> DeshabilitarReservacion(long reservacionId)
        {
            var respuesta = await _reservacionService.DeshabilitarReservacion(reservacionId);
            return Ok(respuesta);
        }

    }
}
