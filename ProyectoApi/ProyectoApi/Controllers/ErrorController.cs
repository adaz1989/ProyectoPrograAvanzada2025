using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    //Evitamos exponer el controlador
    [ApiExplorerSettings(IgnoreApi = true)]

    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("CapturarError")]
        public IActionResult CapturarError()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var respuesta = new RespuestaModel
            {
                Exito = false,
                Mensaje = ex?.Error.Message ?? "Se presentó un problema en el sistema."
            };

            return Ok(respuesta);
        }
    }
}