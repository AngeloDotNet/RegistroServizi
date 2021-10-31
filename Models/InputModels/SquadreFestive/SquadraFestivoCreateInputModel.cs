using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.InputModels.SquadreFestive
{
    public class SquadraFestivoCreateInputModel
    {
        [Required(ErrorMessage = "Il nome della squadra feriale è obbligatorio"),
         Display(Name = "Nome Squadra")]
        public string NomeSquadra { get; set; }

        [Required(ErrorMessage = "La descrizione della squadra feriale è obbligatoria"),
         Display(Name = "Descrizione Squadra")]
        public string DescrizioneSquadra { get; set; }
    }
}