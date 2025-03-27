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
    public class EquipoController : Controller
    {
        private readonly IConfiguration _configuration;

        public EquipoController (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("RegistrarEquipo")]
        public IActionResult RegistrarEquipo(EquipoModel model)
        {
            using (var context = new SqlConnection(_configuration.GetSection("ConnectionStrings:BDConnection").Value))
            {
                var result = context.Execute("RegistrarEquipo",
                    new { model.EquipoId, model.Cedula, model.Rol });

                var respuesta = new RespuestaModel();

                if (result > 0)
                    respuesta.Exito = true;
                else
                {
                    respuesta.Exito = false;
                    respuesta.Mensaje = "Su info no pudo registrarse";
                }

                return Ok();
            }

        }
    }
}
