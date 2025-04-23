using Microsoft.AspNetCore.Mvc;
using ProyectoDeportivoCR.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProyectoDeportivoCR.Models;
using System.IO;

namespace ProyectoDeportivoCR.Controllers
{
    public class CanchaController : Controller
    {
        private readonly ICanchaService _canchaService;
        private readonly IProvinciaService _provinciaService;
        private readonly ICantonService _cantonService;
        private readonly IDistritoService _distritoService;
        private readonly IDeporteService _deporteService;

        public CanchaController(
            ICanchaService canchaService,
            IProvinciaService provinciaService,
            ICantonService cantonService,
            IDistritoService distritoService,
            IDeporteService deporteService)
        {
            _canchaService = canchaService;
            _provinciaService = provinciaService;
            _cantonService = cantonService;
            _distritoService = distritoService;
            _deporteService = deporteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var respuesta = await _canchaService.ObtenerTodasLasCanchas();
            if (respuesta.Exito)
                return View(respuesta.Datos);

            ViewBag.ErrorMessage = respuesta.Mensaje;
            return View(new List<CanchaModel>());
        }

        [HttpGet]
        public async Task<IActionResult> RegistrarCancha()
        {
            await CargarListasDesplegables();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCancha(CanchaModel model)
        {
            if (model.FotoCanchaWeb != null && model.FotoCanchaWeb.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await model.FotoCanchaWeb.CopyToAsync(memoryStream);
                model.FotoCancha = memoryStream.ToArray();
            }

            var resultado = await _canchaService.RegistrarCancha(model);
            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito)
                return RedirectToAction("Index");

            await CargarListasDesplegables();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarCancha(long canchaId)
        {
            var resultado = await _canchaService.ObtenerCancha(canchaId);
            if (!resultado.Exito)
                return RedirectToAction("Index");

            await CargarListasDesplegables();
            return View(resultado.Datos);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCancha(CanchaModel model)
        {

            if (!ModelState.IsValid)
            {
                await CargarListasDesplegables();
                return View(model);
            }

            if (model.FotoCanchaWeb != null && model.FotoCanchaWeb.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await model.FotoCanchaWeb.CopyToAsync(memoryStream);
                model.FotoCancha = memoryStream.ToArray();
            }

            var resultado = await _canchaService.ActualizarInformacionCancha(model);
            if (resultado.Exito)
                return RedirectToAction("ObtenerCancha", new { canchaId = model.CanchaId });

            ViewBag.Mensaje = resultado.Mensaje;
            await CargarListasDesplegables();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCancha(long canchaId)
        {
      
            var resultado = await _canchaService.ObtenerCancha(canchaId);
            if (!resultado.Exito)
            {
                TempData["ErrorMessage"] = resultado.Mensaje;
                return RedirectToAction("Index");
            }

            await CargarListasDesplegables();
            return View(resultado.Datos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeshabilitarCancha(long canchaId)
        {
            await _canchaService.DeshabilitarCancha(canchaId);
            return RedirectToAction("ObtenerCancha", new { canchaId });
        }

        #region Métodos auxiliares

        private async Task CargarListasDesplegables()
        {
            var respProv = await _provinciaService.ObtenerTodasProvincias();
            var respCant = await _cantonService.ObtenerTodosCantones();
            var respDist = await _distritoService.ObtenerTodosDistritos();
            var respDeport = await _deporteService.ObtenerTodosLosDeportes();

            ViewBag.Provincias = respProv.Exito ? respProv.Datos : new List<ProvinciaModel>();
            ViewBag.Cantones = respCant.Exito ? respCant.Datos : new List<CantonModel>();
            ViewBag.Distritos = respDist.Exito ? respDist.Datos : new List<DistritoModel>();
            ViewBag.Deportes = respDeport.Exito ? respDeport.Datos : new List<DeporteModel>();
        }

        #endregion
    }
}
