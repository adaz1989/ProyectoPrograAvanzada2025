using Microsoft.AspNetCore.Mvc;
using ProyectoDeportivoCR.Services;

namespace ProyectoDeportivoCR.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Llamamos al servicio que obtiene todas las categorías
            var resultado = await _categoriaService.ObtenerTodasLasCategorias();

            if (resultado.Exito && resultado.Datos != null)
            {
                // Pasamos la lista de categorías al View
                return View(resultado.Datos);
            }

            // Si hay un error, mostramos mensaje y enviamos una lista vacía
            ViewBag.Mensaje = resultado.Mensaje;
            return View(new List<CategoriaModel>());
        }

        [HttpGet]
        public IActionResult RegistrarCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCategoria(CategoriaModel model)
        {
            var resultado = await _categoriaService.RegistrarCategoria(model);

            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito) return RedirectToAction("Index");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarCategoria(int categoriaId)
        {
            // Obtén la categoría por su ID
            var resultado = await _categoriaService.ObtenerCategorias(categoriaId);

            if (resultado.Exito && resultado.Datos != null)
            {
                // Retorna la vista con el modelo que se va a editar
                return View(resultado.Datos);
            }

            ViewBag.Mensaje = resultado.Mensaje;
            // Si no se encontró o hubo un error, redirige a la vista de búsqueda
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCategoria(CategoriaModel model)
        {
            // Lógica de actualización
            var resultado = await _categoriaService.ActualizarCategoria(model);

            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito)
            {
                // Si se actualiza con éxito, podrías volver a mostrar la misma categoría
                // o redirigir a otra acción (por ejemplo, al listado o a la vista de búsqueda).
                return RedirectToAction("Index", new { categoriaId = model.CategoriaId });
            }

            // Si ocurre algún error, vuelve a la vista con el modelo para mostrar mensajes.
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerCategorias(int categoriaId)
        {
            var resultado = await _categoriaService.ObtenerCategorias(categoriaId);

            if (resultado.Exito && resultado.Datos != null)
            {
                return View(resultado.Datos);
            }

            ViewBag.Mensaje = resultado.Mensaje;
            return View();
        }
            
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DesabilitarCategoria(int categoriaId)
        {
            var resultado = await _categoriaService.DesabilitarCategoria(categoriaId);
            ViewBag.Mensaje = resultado.Mensaje;
            return RedirectToAction("Index");
        }

    }
}
