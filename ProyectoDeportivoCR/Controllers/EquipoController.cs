using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ProyectoDeportivoCR.Controllers
{
    public class EquipoController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public EquipoController(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult RegistrarEquipo(long torneoId)
        {
            var model = new EquipoModel
            {
                TorneoId = torneoId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult RegistrarEquipo(EquipoModel model)
        {
            if (model.TorneoId <= 0)
            {
                ModelState.AddModelError("", "Error: No se encontró el torneo.");
                return View(model);
            }

            using (var http = _httpClient.CreateClient())
            {
                var url = _configuration.GetSection("Variables:urlWebApi").Value + "Equipo/RegistrarEquipo";

                // http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var response = http.PostAsJsonAsync(url, model).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("ConsultarTorneos", "Torneos");
            }

            ModelState.AddModelError("", "Error al registrar el equipo. Intente nuevamente.");
            return View(model);
        }
    }
}
