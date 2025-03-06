using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [AllowAnonymous]
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

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion(UsuarioModel model)
        {
            var respuesta = await _usuarioService.AutenticarUsuario(model);
            return Ok(respuesta);
        }
    }
}
