using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using Dapper;
using ProyectoApi.Data;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaController : ControllerBase
    {
        private readonly IDapperContext _context;

        public DiaController(IDapperContext dapperContext)
        {
            _context = dapperContext;
        }

        [HttpGet]
        [Route("ObtenerDias")]
        public async Task<IActionResult> ObtenerDias()
        {
            using var conexion = _context.CrearConexion();

            var resultado = await conexion.QueryAsync<DiaModel>(
                "ObtenerDias",
                commandType: CommandType.StoredProcedure
            );

            var respuesta = new RespuestaModel();

            if (resultado != null)
            {
                respuesta.Exito = true;
                respuesta.Datos = resultado;
            }
            else
            {
                respuesta.Exito = false;
                respuesta.Mensaje = "No se encontraron días disponibles";
            }

            return Ok(respuesta);
        }
    }
}
