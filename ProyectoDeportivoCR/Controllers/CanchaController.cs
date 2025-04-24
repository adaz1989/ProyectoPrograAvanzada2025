using Microsoft.AspNetCore.Mvc;
using ProyectoDeportivoCR.Services;
using System.Threading.Tasks;

namespace ProyectoDeportivoCR.Controllers
{
    public class CanchaController : Controller
    {
        private readonly ICanchaService _canchaService;
        private readonly IProvinciaService _provinciaService;
        private readonly ICantonService _cantonService;
        private readonly IDistritoService _distritoService;
        private readonly IDeporteService _deporteService;
        private readonly DiasService _diasService; // Si tienes una interfaz, usar: IDiasService

        public CanchaController(
            ICanchaService canchaService,
            IProvinciaService provinciaService,
            ICantonService cantonService,
            IDistritoService distritoService,
            IDeporteService deporteService,
            DiasService diasService // Si existe interfaz: IDiasService diasService
        )
        {
            _canchaService = canchaService;
            _provinciaService = provinciaService;
            _cantonService = cantonService;
            _distritoService = distritoService;
            _deporteService = deporteService;
            _diasService = diasService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
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

        [HttpGet]
        public async Task<IActionResult> HorarioCancha(int canchaId)
        {
            // CAMBIAR ESTO PARA SI O SI RECIBIR EL ID DE LA CANCHA
            canchaId = 2;
            var resultadoCancha = await _canchaService.ObtenerCancha(canchaId);
            var resultadoHorario = await _canchaService.ObtenerHorariosCancha(canchaId);
            var resultadoDias = await _diasService.ObtenerDias();


            ViewBag.Cancha = resultadoCancha.Datos;
            ViewBag.Dias = resultadoDias.Datos;
            return View(resultadoHorario.Datos);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarHorarioCancha(HorarioCanchaModel model)
        {
            var resultado = await _canchaService.RegistrarHorarioCancha(model);
            ViewBag.Mensaje = resultado.Mensaje;


            // PESIMA PRACTICA, PERO POR TIEMPO PREFIERO CASTEAR LONG A INT ANTES QUE ARREGLAR TODO LO DEMAS
            var resultadoCancha = await _canchaService.ObtenerCancha((int)model.CanchaId);
            var resultadoHorario = await _canchaService.ObtenerHorariosCancha(model.CanchaId);
            var resultadoDias = await _diasService.ObtenerDias();

            ViewBag.Cancha = resultadoCancha.Datos;
            ViewBag.Dias = resultadoDias.Datos;

            return View("HorarioCancha", resultadoHorario.Datos);
        }
    }
}
