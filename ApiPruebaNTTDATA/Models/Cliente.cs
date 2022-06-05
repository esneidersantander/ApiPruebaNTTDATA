using System.ComponentModel.DataAnnotations;


namespace ApiPruebaNTTDATA.Models
{
    public class Cliente:Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor ingresar una contraseña.")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Ingrese un estado al cliente.")]
        public bool Estado { get; set; }
    }
}
