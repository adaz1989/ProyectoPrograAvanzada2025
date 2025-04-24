using Microsoft.AspNetCore.Mvc;
using ProyectoDeportivoCR.Models;
using ProyectoDeportivoCR.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProyectoDeportivoCR.Controllers
{
    public class ResennasController : Controller
    {
        private readonly IResennaService _resennaService;

        public ResennasController(IResennaService resennaService)
        {
            _resennaService = resennaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var respuesta = await _resennaService.ObtenerTodasLasResennas();
            if (respuesta.Exito)
                return View(respuesta.Datos);

            ViewBag.Error = respuesta.Mensaje;
            return View(new List<ResennaCanchaModel>());
        }

        [HttpGet]
        public async Task<IActionResult> Details(long canchaId)
        {
            var respuesta = await _resennaService.ObtenerResennaPorCancha(canchaId);
            if (respuesta.Exito)
                return View(respuesta.Datos);

            return NotFound(respuesta.Mensaje);
        }

        // GET: /Resennas/RegistrarResenna/5
        [HttpGet]
        public IActionResult RegistrarResenna(long canchaId)
        {
            // Prellenamos el modelo con el CanchaId obtenido de la tarjeta
            var model = new ResennaCanchaModel
            {
                CanchaId = canchaId
            };
            return View(model);
        }

        // POST: /Resennas/RegistrarResenna
        [HttpPost]
        public async Task<IActionResult> RegistrarResenna(ResennaCanchaModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var respuesta = await _resennaService.RegistrarResenna(model);
            if (respuesta.Exito)
                return RedirectToAction("Index");

            ViewBag.Mensaje = respuesta.Mensaje;
            return View(model);
        }
    }
}
