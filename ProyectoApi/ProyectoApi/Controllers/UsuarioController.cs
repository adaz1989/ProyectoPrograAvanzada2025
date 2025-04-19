using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJwtService _jwtService;

        public UsuarioController(IUsuarioService usuarioService, IJwtService jwtService)
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService;
        }

        [HttpPut]
        [Route("ActualizarInformacionUsuario")]
        public async Task<IActionResult> ActualizarInformacionUsuario(UsuarioModel model)
        {
            var respuesta = await _usuarioService.ActualizarInformacionUsuario(model);
            return Ok(respuesta);
        }

        [HttpPut]
        // Es buena practica incluir {parametro} en el Route cuando es por la URL
        [Route("DeshabilitarUsuario/{usuarioId}")]
        public async Task<IActionResult> Deshabilitarusuario(int usuarioId)
        {
            var respuesta = await _usuarioService.DeshabilitarUsuario(usuarioId);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("ObtenerInformacionUsuario")]
        public async Task<IActionResult> ObtenerInformacionUsuario()
        {
            var respuesta = await _usuarioService.ObtenerInformacionUsuario(HttpContext);
            return Ok(respuesta);



            // Obtener el ID del usuario del token
            //var userId = User.FindFirst("userId")?.Value;
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return Unauthorized();
            //}

            //var respuesta = await _usuarioService.ObtenerInformacionUsuario(int.Parse(userId));
            //return Ok(respuesta);
        }
    }
}
