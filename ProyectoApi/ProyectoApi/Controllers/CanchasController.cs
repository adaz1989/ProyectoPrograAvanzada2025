﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoApi.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CanchasController : ControllerBase
    {
        private readonly ICanchasService _canchasService;
        private readonly IJwtService _jwtService;

        public CanchasController(ICanchasService canchasService, IJwtService jwtService)
        {
            _canchasService = canchasService;
            _jwtService = jwtService;
        }


        [HttpPost]
        [Route("RegistrarCancha")]
        public async Task<IActionResult> RegistrarCancha(CanchaModel model)
        {
            var respuesta = await _canchasService.RegistrarCanchas(model);
            return Ok(respuesta);
        }

        [HttpPut]
        [Route("ActualizarInformacionCanchas")]
        public async Task<IActionResult> ActualizarInformacionCanchas(CanchaModel model)
        {
            var respuesta = await _canchasService.ActualizarInformacionCanchas(model);
            return Ok(respuesta);
        }


        [HttpPut]
        // Es buena practica incluir {parametro} en el Route cuando es por la URL
        [Route("DeshabilitarCanchas/{canchaId}")]
        public async Task<IActionResult> DeshabilitarCanchas(long canchaId)
        {
            var respuesta = await _canchasService.DeshabilitarCanchas(canchaId);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("ObtenerInformacionCanchas/{canchaId}")]
        public async Task<IActionResult> ObtenerInformacionCanchas(long canchaId)
        {
            var respuesta = await _canchasService.ObtenerInformacionCanchas(canchaId);
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("ObtenerTodasLasCanchas")]
        public async Task<IActionResult> ObtenerTodasLasCanchas()
        {
            var canchas = await _canchasService.ObtenerTodasLasCanchas();
            return Ok(canchas); // Devuelve la lista directamente, no un objeto envoltorio.
        }



    }
}
