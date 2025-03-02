using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProyectoApi.Models;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionController : ControllerBase
    {
        //Inyeccion de dependencias
        private readonly IUsuarioService _usuarioService;
        public SesionController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("RegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario(UsuarioModel model)
        {
            var respuesta = await _usuarioService.RegistrarUsuario(model);
            return Ok(respuesta);
        }
    }
}
