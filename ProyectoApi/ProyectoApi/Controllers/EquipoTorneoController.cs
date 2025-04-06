using Dapper;
using ProyectoApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ProyectoApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoTorneoController : Controller
    {
        private readonly IConfiguration _configuration;

        public EquipoTorneoController (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("RegistrarEquipo")]
        public IActionResult RegistrarEquipo(EquipoTorneo model)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
            {
                var result = context.Execute("RegistrarEquipo",
                    new { model.NombreEquipo, model.TorneoId, model.Cedula, model.Rol });

                var respuesta = new RespuestaModel();

                if (result > 0)
                    respuesta.Exito = true;
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = "No se pudo registrar el equipo.";
                }

                return Ok(respuesta);
            }

        }
    }
}
