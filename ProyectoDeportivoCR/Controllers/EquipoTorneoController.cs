using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ProyectoDeportivoCR.Controllers
{
    public class EquipoTorneoController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public EquipoTorneoController(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult RegistrarEquipoTorneo(long torneoId)
        {
            var model = new EquipoTorneoModel
            {
                TorneoId = torneoId,
                Integrantes = new List<IntegranteModel>
                {
                    new IntegranteModel()
                }
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult RegistrarEquipoTorneo(EquipoTorneoModel model)
        {
            if (model.TorneoId <= 0 || model.Integrantes == null || !model.Integrantes.Any() )
            {
                ModelState.AddModelError("", "Debe agregar al menos un integrante.");
                return View(model);
            }

            using (var http = _httpClient.CreateClient())
            {
                var url = _configuration.GetSection("Variables:urlWebApi").Value + "EquipoTorneo/RegistrarEquipoTorneo";

                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var response = http.PostAsJsonAsync(url, model).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("ConsultarTorneos", "Torneos");
            }

            ModelState.AddModelError("", "Error al registrar el equipo. Intente nuevamente.");
            return View(model);
        }
    }
}
