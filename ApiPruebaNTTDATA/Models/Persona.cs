using System.ComponentModel.DataAnnotations;

namespace ApiPruebaNTTDATA.Models
{
    public class Persona
    {

        [Required(ErrorMessage = "Por favor ingresar una identificación.")]
        public string Identificacion { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Por favor ingresar un nombre.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor ingresar un género.")]
        [StringLength(1)]
        [RegularExpression("M|F", ErrorMessage = "Por favor ingresar 'F' para femenino o 'M' para masculino.")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Por favor ingresar la edad.")]
        [Range(10, 100, ErrorMessage = "Por favor ingresar una edad entre 10 y 100 años")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "Por favor ingresar la dirección.")]
        [StringLength(255)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Por favor ingresar un número de teléfono.")]
        [StringLength(10, ErrorMessage = "El número de teléfono debe tener máximo 10 dígitos.")]
        public string Telefono { get; set; }
    }
}
