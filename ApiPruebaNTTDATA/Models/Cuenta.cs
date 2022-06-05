using System.ComponentModel.DataAnnotations;

namespace ApiPruebaNTTDATA.Models
{
    public class Cuenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor ingresar un id de cliente.")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [StringLength(10, ErrorMessage = "El número de cuenta debe tener máximo 10 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Por favor ingresar un número de cuenta de cuenta con sólo números.")]
        [Required(ErrorMessage = "Por favor ingresar un número de cuenta.")]
        public string NumeroCuenta { get; set; }

        [Required(ErrorMessage = "Por favor ingresar un tipo de cuenta 'AHORRO' o 'CORRIENTE'.")]
        [RegularExpression("(?:AHORRO|CORRIENTE)", ErrorMessage = "Por favor ingresar un tipo de cuenta 'AHORRO' o 'CORRIENTE'.")]
        public string TipoCuenta { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Por favor ingresar una cantidad entre 0 y 1000000.")]
        public double SaldoInicial { get; set; }
        
        [Required]
        public bool Estado { get; set; }
    }
}
