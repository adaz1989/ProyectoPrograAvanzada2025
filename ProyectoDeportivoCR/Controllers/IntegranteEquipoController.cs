using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ProyectoDeportivoCR.Controllers
{
    public class IntegranteEquipoController : Controller
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;

        public IntegranteEquipoController(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult RegistrarIntegranteEquipo(long torneoId)
        {
            var model = new IntegranteEquipoModel
            {
                EquipoId = torneoId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult RegistrarIntegranteEquipo(IntegranteEquipoModel model)
        {
            using (var http = _httpClient.CreateClient())
            {
                var url = _configuration.GetSection("Variables:urlWebApi").Value + "IntegranteEquipo/RegistrarIntegranteEquipo";

                // http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var response = http.PostAsJsonAsync(url, model).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("ConsultarTorneos", "Torneos");
            }

            return View();
        }
    }
}
