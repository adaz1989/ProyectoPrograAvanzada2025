using Microsoft.AspNetCore.Mvc;
using ProyectoDeportivoCR.Services;

namespace ProyectoDeportivoCR.Controllers
{
    public class DeporteController : Controller
    {
        private readonly IDeporteService _deporteService;

        public DeporteController(IDeporteService deporteService)
        {
            _deporteService = deporteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var respuesta = await _deporteService.ObtenerTodosLosDeportes();
            if (respuesta.Exito)
                return View(respuesta.Datos);

            ViewBag.ErrorMessage = respuesta.Mensaje;
            return View(new List<DeporteModel>());
        }

        [HttpGet]
        public IActionResult RegistrarDeporte()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarDeporte(DeporteModel model)
        {
            var resultado = await _deporteService.RegistrarDeporte(model);
            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito)
                return RedirectToAction("Index");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerInformacionDeporte(int deporteId)
        {
            var resultado = await _deporteService.ObtenerInformacionDeporte(deporteId);

            if (resultado.Exito && resultado.Datos != null)
            {
                return View(resultado.Datos);
            }

            ViewBag.Mensaje = resultado.Mensaje;
            return View();
        }
    }
}
