using System.ComponentModel.DataAnnotations;
namespace RegistroServizi.Models.InputModels.Associazioni
{
    public class AssociazioneEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome associazione è obbligatorio"),
         Display(Name = "Nome Associazione")]
        public string Denominazione { get; set; }

        [Required(ErrorMessage = "La sigla dell'associazione è obbligatoria"),
         Display(Name = "Sigla")]
        public string Sigla { get; set; }

        [Required(ErrorMessage = "L'indirizzo è obbligatorio"),
         Display(Name = "Indirizzo")]
        public string Indirizzo { get; set; }

        [Required(ErrorMessage = "Il cap è obbligatorio"),
         Display(Name = "Cap")]
        public string Cap { get; set; }

        [Required(ErrorMessage = "Il comune è obbligatorio"),
         Display(Name = "Comune")]
        public string Comune { get; set; }

        [Required(ErrorMessage = "La provincia è obbligatoria"),
         Display(Name = "Provincia")]
        public string Provincia { get; set; }
    }
}