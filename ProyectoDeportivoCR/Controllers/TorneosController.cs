using Microsoft.AspNetCore.Mvc;

namespace ProyectoDeportivoCR.Controllers
{
    public class TorneosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
