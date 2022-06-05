using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPruebaNTTDATA.Models
{
    [Table("Movimientos")]
    public class Movimiento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor ingresar un id de cuenta.")]
        public int CuentaId { get; set; }
        public Cuenta Cuenta { get; set; }

        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Por favor ingresar un tipo de movimiento 'RETIRO' o 'DEPOSITO'.")]
        [RegularExpression("(?:RETIRO|DEPOSITO)", ErrorMessage = "Por favor ingresar un tipo de movimiento 'RETIRO' o 'DEPOSITO'.")]
        public string TipoMovimiento { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Por favor ingresar una cantidad entre 0 y 1000000.")]
        public double Valor { get; set; }

        public double Saldo { get; set; }
    }
}
