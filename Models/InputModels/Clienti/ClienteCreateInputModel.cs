using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Controllers;

namespace RegistroServizi.Models.InputModels.Clienti
{
    public class ClienteCreateInputModel
    {
        [Required(ErrorMessage = "La ragione sociale è obbligatoria"),
         Remote(action: nameof(ClientiController.IsClienteAvailable), controller: "Clienti", ErrorMessage = "La ragione sociale indicata esiste già"),
         Display(Name = "Ragione Sociale")]
        public string RagioneSociale { get; set; }

        [Required(ErrorMessage = "L'indirizzo è obbligatorio"),
         Display(Name = "Indirizzo")]
        public string Indirizzo { get; set; }

        [Required(ErrorMessage = "Il comune è obbligatorio"),
         Display(Name = "Comune")]
        public string Comune { get; set; }
        
        [Required(ErrorMessage = "Il cap è obbligatorio"),
         Display(Name = "Cap")]
        public string Cap { get; set; }
        
        [Required(ErrorMessage = "La provincia è obbligatoria"),
         Display(Name = "Provincia")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Il telefono è obbligatorio"),
         Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La partita Iva è obbligatoria"),
         Display(Name = "Partita Iva")]
        public string PartitaIva { get; set; }

        [Required(ErrorMessage = "Il codice fiscale è obbligatorio"),
         Display(Name = "Codice Fiscale")]
        public string CodiceFiscale { get; set; }

        [Required(ErrorMessage = "Il fax è obbligatorio"),
         Display(Name = "Fax")]
        public string Fax { get; set; }

        public int Spese { get; set; }
    }
}