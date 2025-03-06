﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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
        [Route("ObtenerInformacionUsuario/{usuarioId}")]
        public async Task<IActionResult> ObtenerInformacionUsuario(int usuarioId)
        {
            var respuesta = await _usuarioService.ObtenerInformacionUsuario(usuarioId);
            return Ok(respuesta);
        }
    }
}
