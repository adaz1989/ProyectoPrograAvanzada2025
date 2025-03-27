using Dapper;
using ProyectoApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

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

                var table = new DataTable();
                table.Columns.Add("Cedula", typeof(string));
                table.Columns.Add("Rol", typeof(int));

                foreach (var integrante in model.Integrantes)
                {
                    table.Rows.Add(integrante.Cedula, integrante.Rol);
                }

                var result = context.Execute("RegistrarEquipo",
                    new { model.NombreEquipo, 
                          model.TorneoId,
                          Integrantes = table.AsTableValuedParameter("IntegranteEquipoType")
                    });

                var respuesta = new RespuestaModel();

                if (result > 0)
                {
                    /* var equipoId = result;

                    foreach (var integrante in model.Integrantes)
                    {
                        context.Execute("RegistarIntegrante",
                            new { equipoId, integrante.Cedula, integrante.Rol });
                    } */

                    respuesta.Exito = true;
                }
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
