using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Controllers;

namespace RegistroServizi.Models.InputModels.Soci
{
    public class SocioCreateInputModel
    {
        [Required(ErrorMessage = "Il numero della tessera è obbligatorio"),
         Remote(action: nameof(SociController.IsTesseraAvailable), controller: "Soci", ErrorMessage = "La tessera indicata esiste già"),
         Display(Name = "Numero Tessera")]
        public string Tessera { get; set; }
        
        [Required(ErrorMessage = "Il nominativo è obbligatorio"),
         Remote(action: nameof(SociController.IsSocioAvailable), controller: "Soci", ErrorMessage = "Il nominativo indicato esiste già"),
         Display(Name = "Nominativo")]
        public string Nominativo { get; set; }

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

        [Required(ErrorMessage = "Il luogo di nascita è obbligatorio"),
         Display(Name = "Luogo di nascita")]
        public string LuogoNascita { get; set; }

        [Required(ErrorMessage = "La data di nascita è obbligatoria"),
         Display(Name = "Data di nascita")]
        public string DataNascita { get; set; }

        [Required(ErrorMessage = "Il codice fiscale è obbligatorio"),
         Display(Name = "Codice Fiscale")]
        public string CodiceFiscale { get; set; }
        
        [Required(ErrorMessage = "Il telefono è obbligatorio"),
         Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "L'indirizzo email è obbligatorio"),
         Display(Name = "Indirizzo email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La data di tesseramento è obbligatoria"),
         Display(Name = "Data di tesseramento")]
        public string DataTesseramento { get; set; }

        [Required(ErrorMessage = "Il trattamento dei dati è obbligatorio"),
         Display(Name = "Trattamento dei dati")]
        public string TrattamentoDati { get; set; }

        [Required(ErrorMessage = "La professione è obbligatoria"),
         Display(Name = "Professione")]
        public string Professione { get; set; }

        [Required(ErrorMessage = "La zona è obbligatoria"),
         Display(Name = "Zona")]
        public string Zona { get; set; }
    }
}