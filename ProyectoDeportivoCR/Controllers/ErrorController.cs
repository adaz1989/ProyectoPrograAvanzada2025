﻿using Microsoft.AspNetCore.Mvc;

namespace ProyectoDeportivoCR.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult CapturarError()
        {
            return View("Error");
        }
    }
}
