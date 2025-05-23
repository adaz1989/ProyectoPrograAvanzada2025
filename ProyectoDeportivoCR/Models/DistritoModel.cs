﻿using System.ComponentModel.DataAnnotations;

namespace ProyectoDeportivoCR.Models
{
    public class DistritoModel
    {
        public long DistritoId { get; set; }
        [StringLength(50)]
        public string? NombreDistrito { get; set; }
        public long CantonId { get; set; }
    }
}
