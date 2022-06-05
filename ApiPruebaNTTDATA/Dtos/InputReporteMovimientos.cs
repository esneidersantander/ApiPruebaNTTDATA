using System;
using System.ComponentModel.DataAnnotations;

namespace ApiPruebaNTTDATA.Dtos
{
    public class InputReporteMovimientos
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }
    }
}