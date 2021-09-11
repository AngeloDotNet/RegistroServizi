using System.ComponentModel.DataAnnotations;
using RegistroServizi.Models.Enums;

namespace RegistroServizi.Models.InputModels.Utenti
{
    public class UserRoleInputModel
    {
        [Required(ErrorMessage = "L'indirizzo email è obbligatorio"),
         EmailAddress(ErrorMessage = "L'indirizzo email digitato non è valido"),
         Display(Name = "Indirizzo email")]
        public string Email { get; set; }
            
        [Required(ErrorMessage = "Il ruolo è obbligatorio"),
         Display(Name = "Ruolo")]
        public Roles Role { get; set; }
    }
}