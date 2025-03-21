using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ProyectoDeportivoCR.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEncriptacionService _encriptacion;


        public LoginController(IUsuarioService usuarioService, IEncriptacionService encriptacion)
        {
            _usuarioService = usuarioService;
            _encriptacion = encriptacion;
        }


        //[HttpGet]
        //public IActionResult RegistrarUsuario()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> RegistrarUsuario(UsuarioModel model)
        //{
        //    var resultado = await _usuarioService.RegistrarUsuario(model);

        //    if(resultado)
        //    {
        //        ViewBag.mensaje = "Usuario registrado correctamente";
        //        return RedirectToAction("IniciarSesion");
        //    }

        //    ViewBag.mensaje = "Error al registrar el usuario";
        //    return View();
        //}

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


        public IActionResult Principal(string username, string password)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
