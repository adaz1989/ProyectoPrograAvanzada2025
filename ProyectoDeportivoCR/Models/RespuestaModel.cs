﻿namespace ProyectoDeportivoCR.Models
{
    public class RespuestaModel
    {
        public bool Indicador { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public object? Datos { get; set; }
    }
}
