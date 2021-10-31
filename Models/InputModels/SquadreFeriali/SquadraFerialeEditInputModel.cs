using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.InputModels.SquadreFeriali
{
    public class SquadraFerialeEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della squadra feriale è obbligatorio"),
         Display(Name = "Nome Squadra")]
        public string NomeSquadra { get; set; }

        [Required(ErrorMessage = "La descrizione della squadra feriale è obbligatoria"),
         Display(Name = "Descrizione Squadra")]
        public string DescrizioneSquadra { get; set; }
    }
}