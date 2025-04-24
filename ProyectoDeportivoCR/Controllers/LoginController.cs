using Microsoft.AspNetCore.Mvc;

namespace ProyectoDeportivoCR.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEncriptacionService _encriptacion;


        public LoginController(IUsuarioService usuarioService, IEncriptacionService encriptacion)
        {
            _usuarioService = usuarioService;
            _encriptacion = encriptacion;
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(UsuarioModel model)
        {
            var resultado = await _usuarioService.RegistrarUsuario(model);

            ViewBag.Mensaje = resultado.Mensaje;

            if (resultado.Exito)
                return RedirectToAction("IniciarSesion");


            return View(model);
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(UsuarioModel model)
        {
            var resultado = await _usuarioService.IniciarSesion(model);

            if (resultado.Exito && resultado.Datos != null)
            {
                HttpContext.Session.SetString("Token", resultado.Datos.Token!);
                HttpContext.Session.SetString("Nombre", resultado.Datos.NombreUsuario!);
                HttpContext.Session.SetString("DescripcionTipoUsuario", resultado.Datos.DescripcionTipoUsuario!);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensaje = resultado.Mensaje;
            return View();
        }


        [HttpGet]
        public IActionResult CerrarSesion()
        {
            // Limpia toda la sesión
            HttpContext.Session.Clear();
            // Redirige al login
            return RedirectToAction("IniciarSesion");
        }

        //public IActionResult Principal(string username, string password)
        //{
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
