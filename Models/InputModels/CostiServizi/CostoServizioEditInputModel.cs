using System.ComponentModel.DataAnnotations;
using RegistroServizi.Models.ValueTypes;

namespace RegistroServizi.Models.InputModels.CostiServizi
{
    public class CostoServizioEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Il tipo servizio è obbligatorio"),
         Display(Name = "Tipo servizio")]
        public string TipoServizio { get; set; }

        [Required(ErrorMessage = "Il costo fisso è obbligatorio"),
         Display(Name = "Costo fisso")]
        public Money CostoFisso { get; set; }

        [Required(ErrorMessage = "Il costo al km è obbligatorio"),
         Display(Name = "Costo al km")]
        public Money CostoKm { get; set; }

        [Required(ErrorMessage = "Il prezzo del secondo trasportato è obbligatorio"),
         Display(Name = "Secondo trasportato")]
        public Money SecondoTrasportato { get; set; }

        [Required(ErrorMessage = "Il prezzo del fermo macchina è obbligatorio"),
         Display(Name = "Fermo macchina")]
        public Money FermoMacchina { get; set; }

        [Required(ErrorMessage = "Il prezzo dell'accompagnatore è obbligatorio"),
         Display(Name = "Accompagnatore")]
        public Money Accompagnatore { get; set; }

        [Required(ErrorMessage = "Lo sconto soci è obbligatorio"),
         Display(Name = "Sconto soci")]
        public int ScontoSoci { get; set; }
    }
}