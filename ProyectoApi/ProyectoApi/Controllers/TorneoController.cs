using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ProyectoApi.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TorneoController : Controller
    {
        private readonly IConfiguration _configuration;
        public TorneoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // [Authorize]
        [HttpGet]
        [Route("ConsultarTorneos")]
        public IActionResult ConsultarTorneos(long TorneoId)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
            {
                var result = context.Query<TorneoModel>("ConsultarTorneos",
                    new { TorneoId });

                var respuesta = new RespuestaModel();

                if (result != null)
                {
                    respuesta.Exito = true;
                    respuesta.Datos = result;
                }
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = "No hay información registrada.";
                }

                return Ok(respuesta);
            }
        }
    }
}
