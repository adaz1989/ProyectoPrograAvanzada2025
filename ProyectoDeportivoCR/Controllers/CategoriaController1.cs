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
        public IActionResult RegistrarCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarCategoria(CategoriaModel model)
        {
            var resultado = await _categoriaService.RegistrarCategoria(model);

            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito) return RedirectToAction("ObtenerCategorias");

            return View();
        }

        [HttpGet]
        public IActionResult ActualizarCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCategoria(CategoriaModel model)
        {
            var resultado = await _categoriaService.ActualizarCategoria(model);

            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito) return RedirectToAction("ObtenerCategorias");

            return View();
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
        public async Task<IActionResult> DesabilitarCategoria(int categoriaId)
        {
            var resultado = await _categoriaService.DesabilitarCategoria(categoriaId);

            ViewBag.Mensaje = resultado.Mensaje;

            return RedirectToAction("ObtenerCategorias");
        }
    }
}
