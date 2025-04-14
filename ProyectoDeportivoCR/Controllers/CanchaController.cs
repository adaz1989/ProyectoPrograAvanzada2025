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

        // En el constructor se inyectan todos los servicios requeridos
        public CanchaController(ICanchaService canchaService,
                                IProvinciaService provinciaService,
                                ICantonService cantonService,
                                IDistritoService distritoService)
        {
            _canchaService = canchaService;
            _provinciaService = provinciaService;
            _cantonService = cantonService;
            _distritoService = distritoService;
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
        public async Task<IActionResult> RegistrarCancha()
        {
            await CargarListasDesplegables();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCancha(CanchaModel model)
        {
            var resultado = await _canchaService.RegistrarCancha(model);
            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito)
                return RedirectToAction("ObtenerCancha");
 
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


        #region Metodo auxiliar para cargar los datos de la BD 

        private async Task CargarListasDesplegables()
        {
            // Llamada a los servicios para obtener las listas
            var respuestaProvincias = await _provinciaService.ObtenerTodasProvincias();
            var respuestaCantones = await _cantonService.ObtenerTodosCantones();
            var respuestaDistritos = await _distritoService.ObtenerTodosDistritos();

            ViewBag.Provincias = (respuestaProvincias.Exito && respuestaProvincias.Datos != null)
                                ? respuestaProvincias.Datos
                                : new List<ProvinciaModel>();

            ViewBag.Cantones = (respuestaCantones.Exito && respuestaCantones.Datos != null)
                                ? respuestaCantones.Datos
                                : new List<CantonModel>();

            ViewBag.Distritos = (respuestaDistritos.Exito && respuestaDistritos.Datos != null)
                                ? respuestaDistritos.Datos
                                : new List<DistritoModel>();
        }

        #endregion
    }
}
