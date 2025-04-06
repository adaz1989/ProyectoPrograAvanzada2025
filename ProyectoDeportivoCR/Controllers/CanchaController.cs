using Microsoft.AspNetCore.Mvc;
using ProyectoDeportivoCR.Services;
using System.Threading.Tasks;

namespace ProyectoDeportivoCR.Controllers
{
    public class CanchaController : Controller
    {
        private readonly ICanchaService _canchaService;

        public CanchaController(ICanchaService canchaService)
        {
            _canchaService = canchaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var respuesta = await _canchaService.ObtenerTodasLasCanchas();

            if (respuesta.Exito)
            {
                return View(respuesta.Datos);  // Retorna la lista de canchas a la vista
            }
            else
            {
                ViewBag.ErrorMessage = respuesta.Mensaje; // Muestra mensaje de error
                return View();
            }
        }

        [HttpGet]
        public IActionResult RegistrarCancha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCancha(CanchaModel model)
        {
            var resultado = await _canchaService.RegistrarCancha(model);

            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito) return RedirectToAction("ObtenerCancha");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarCancha(int canchaId)
        {
            var resultado = await _canchaService.ObtenerCancha(canchaId);

            if (resultado.Exito && resultado.Datos != null)
            {
                return View(resultado.Datos);
            }

            ViewBag.Mensaje = resultado.Mensaje;
            return RedirectToAction("ObtenerCancha");
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCancha(CanchaModel model)
        {
            var resultado = await _canchaService.ActualizarInformacionCancha(model);

            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito)
            {
                return RedirectToAction("ObtenerCancha", new { canchaId = model.CanchaId });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCancha(int canchaId)
        {
            var resultado = await _canchaService.ObtenerCancha(canchaId);

            if (resultado.Exito && resultado.Datos != null)
            {
                return View(resultado.Datos);
            }

            ViewBag.Mensaje = resultado.Mensaje;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeshabilitarCancha(int canchaId)
        {
            var resultado = await _canchaService.DeshabilitarCancha(canchaId);

            ViewBag.Mensaje = resultado.Mensaje;

            return RedirectToAction("ObtenerCancha");
        }
    }
}
