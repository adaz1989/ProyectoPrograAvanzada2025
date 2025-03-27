using Microsoft.AspNetCore.Mvc;
using ProyectoDeportivoCR.Services;

namespace ProyectoDeportivoCR.Controllers
{
    public class TorneosController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IGeneral _general;

        public TorneosController(IHttpClientFactory httpClient, IConfiguration configuration, IGeneral general)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _general = general;
        }

        public IActionResult ConsultarTorneos()
        {
            var datosResult = _general.ConsultarDatosTorneos(0);

            var torneosActuales = datosResult.Where(t => t.FechaInicio <= DateTime.Now).ToList();
            var torneosFuturos = datosResult.Where(t => t.FechaInicio > DateTime.Now).ToList();

            ViewData["TorneosActuales"] = torneosActuales;
            ViewData["TorneosFuturos"] = torneosFuturos;

            return View();
        }
    }
}
