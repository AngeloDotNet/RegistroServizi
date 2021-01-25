using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Controllers;

namespace RegistroServizi.Models.InputModels.Ospedali
{
    public class OspedaleCreateInputModel
    {
        [Required(ErrorMessage = "L'ospedale è obbligatorio"),
         Remote(action: nameof(OspedaliController.IsOspedaleAvailable), controller: "Ospedali", ErrorMessage = "L'ospedale indicato esiste già"),
         Display(Name = "Ospedale")]
        public string Clinica { get; set; }

        [Required(ErrorMessage = "Il comune è obbligatorio"),
         Display(Name = "Comune")]
        public string Comune { get; set; }
        
        public string Latitudine { get; set; }

        public string Longitudine { get; set; }
    }
}