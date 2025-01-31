using Microsoft.AspNetCore.Mvc;

namespace ProyectoDeportivoCR.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult IniciarSesion()
        {
            return View();
        }

        public IActionResult Principal(string username, string password)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
