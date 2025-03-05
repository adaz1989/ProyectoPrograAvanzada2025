using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("ActualizarInformacionUsuario")]
        public async Task<IActionResult> ActualizarInformacionUsuario(UsuarioModel model)
        {
            var respuesta = await _usuarioService.ActualizarInformacionUsuario(model);
            return Ok(respuesta);
        }

        [HttpPost]
        [Route("DeshabilitarUsuario")]
        public async Task<IActionResult> Deshabilitarusuario(UsuarioModel model)
        {
            var respuesta = await _usuarioService.DeshabilitarUsuario(model);
            return Ok(respuesta);
        }
    }
}
