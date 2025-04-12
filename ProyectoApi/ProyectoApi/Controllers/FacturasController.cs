using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models;
using ProyectoApi.Services;

namespace ProyectoApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturasController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        // Método para registrar una factura
        [HttpPut("RegistrarFactura")]
        public async Task<IActionResult> RegistrarFactura([FromBody] FacturaModel model)
        {
            var respuesta = await _facturaService.RegistrarFactura(model);
            return Ok(respuesta);
        }

        // Método para obtener una factura por Id
        [HttpGet("ObtenerFacturaPorId/{facturaId}")]
        public async Task<IActionResult> ObtenerFacturaPorId(long facturaId)
        {
            var respuesta = await _facturaService.ObtenerFacturaPorId(facturaId);
            return Ok(respuesta);
        }

        // Método para obtener todas las facturas
        [HttpGet("ObtenerTodasLasFacturas")]
        public async Task<IActionResult> ObtenerTodasLasFacturas()
        {
            var respuesta = await _facturaService.ObtenerTodasLasFacturas();
            return Ok(respuesta);
        }
    }
}
