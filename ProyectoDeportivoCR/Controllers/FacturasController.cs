using Microsoft.AspNetCore.Mvc;
using ProyectoDeportivoCR.Models;
using ProyectoDeportivoCR.Services;
using System.Threading.Tasks;

namespace ProyectoDeportivoCR.Controllers
{
    public class FacturasController : Controller
    {
        private readonly IFacturaService _facturaService;

        public FacturasController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resultado = await _facturaService.ObtenerTodasLasFacturas();

            if (resultado.Exito)
            {
                return View(resultado.Datos); // Lista de facturas a la vista
            }

            ViewBag.Mensaje = resultado.Mensaje;
            return View();
        }

        [HttpGet]
        public IActionResult RegistrarFactura()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarFactura(FacturaModel model)
        {
            var resultado = await _facturaService.RegistrarFactura(model);

            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito)
                return RedirectToAction("ObtenerFactura", new { facturaId = resultado.Datos.FacturaId });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerFactura(int facturaId)
        {
            var resultado = await _facturaService.ObtenerFacturaPorId(facturaId);

            if (resultado.Exito && resultado.Datos != null)
            {
                return View(resultado.Datos);
            }

            ViewBag.Mensaje = resultado.Mensaje;
            return View();
        }
    }
}
