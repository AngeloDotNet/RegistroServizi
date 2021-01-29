using System.ComponentModel.DataAnnotations;
using RegistroServizi.Models.ValueTypes;

namespace RegistroServizi.Models.InputModels.SociRinnovi
{
    public class SocioRinnovoCreateInputModel
    {
        [Required]
        public int SocioId { get; set; }

        [Required(ErrorMessage = "L'anno è obbligatorio"),
         Display(Name = "Anno rinnovo")]
        public string Anno { get; set; }

        [Required(ErrorMessage = "La quota del rinnovo è obbligatoria"),
         Display(Name = "Quota rinnovo")]
        public Money Quota { get; set; }

        [Required(ErrorMessage = "La data di rinnovo è obbligatoria"),
         Display(Name = "Data rinnovo")]
        public string DataRinnovo { get; set; }
    }
}