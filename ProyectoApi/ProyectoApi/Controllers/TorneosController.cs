using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ProyectoApi.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TorneosController : Controller
    {
        private readonly IConfiguration _configuration;
        public TorneosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // [Authorize]
        [HttpGet]
        [Route("ConsultarTorneos")]
        public IActionResult ConsultarTorneos(long TorneoId)
        {

            Console.WriteLine("Valor de TorneoId recibido: " + TorneoId);

            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
            {
                var result = context.Query<TorneoModel>("ConsultarTorneos",
                    new { TorneoId });

                Console.WriteLine("Cantidad de registros encontrados: " + result.Count());

                var respuesta = new RespuestaModel();

                if (result != null && result.Any())
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
