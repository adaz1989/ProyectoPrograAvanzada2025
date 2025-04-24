using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            if (resultado.Exito && resultado.Datos != null)
            {
                return View(resultado.Datos); 
            }

            ViewBag.ErrorMessage = resultado.Mensaje ?? "No hay facturas registradas.";
            return View(new List<FacturaModel>()); 
        }

        [HttpGet]
        public IActionResult RegistrarFactura()
        {
            ViewBag.MetodosPago = new List<MetodoPagoModel>
            {
                new MetodoPagoModel { MetodoPagoId = 1, DescripcionMetodoPago = "Efectivo" },
                new MetodoPagoModel { MetodoPagoId = 2, DescripcionMetodoPago = "Tarjeta" },
                new MetodoPagoModel { MetodoPagoId = 3, DescripcionMetodoPago = "SINPE" }
            };

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegistrarFactura(FacturaModel model)
        {
            if (model.FotoComprobanteWeb != null && model.FotoComprobanteWeb.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await model.FotoComprobanteWeb.CopyToAsync(memoryStream);
                model.FotoComprobante = memoryStream.ToArray(); 
            }

            var resultado = await _facturaService.RegistrarFactura(model);

            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito)
                return RedirectToAction("Index");

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
