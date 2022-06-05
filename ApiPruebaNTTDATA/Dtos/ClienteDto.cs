using ApiPruebaNTTDATA.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiPruebaNTTDATA.Dtos
{
    public class ClienteDto : Persona
    {
        [Required(ErrorMessage = "Por favor ingresar una contraseña.")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Ingrese un estado al cliente.")]
        public bool Estado { get; set; }
    }
}