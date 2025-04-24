using Microsoft.AspNetCore.Mvc;

namespace ProyectoDeportivoCR.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> PerfilUsuario()
        {
            var resultado = await _usuarioService.ObtenerInformacionUsuario();

            if (resultado.Exito && resultado.Datos != null)
            {
                var usuario = resultado.Datos;
                return View(usuario);
            }

            return RedirectToAction("IniciarSesion", "Login");
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarInformacionUsuario()
        {
            var resultado = await _usuarioService.ObtenerInformacionUsuario();

            if (resultado.Exito && resultado.Datos != null)
            {
                var usuario = resultado.Datos;
                return View(usuario);
            }

            return RedirectToAction("IniciarSesion", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarInformacionUsuario(UsuarioModel model)
        {
            var resultado = await _usuarioService.ActualizarInformacionUsuario(model); 

            if (!resultado.Exito)
            {
                ViewBag.Mensaje = resultado.Mensaje;
                return View(model);
            }            

            return RedirectToAction("PerfilUsuario");
        }

    }
}

