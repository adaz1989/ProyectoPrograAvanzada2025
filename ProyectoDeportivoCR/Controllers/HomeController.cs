using Microsoft.AspNetCore.Mvc;
using ProyectoDeportivoCR.Models;
using System.Diagnostics;

namespace ProyectoDeportivoCR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SobreNosotros()
        {
            return View();
        }
    }
}
