using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Controllers;

namespace RegistroServizi.Models.InputModels.Ospedali
{
    public class OspedaleEditInputModel
    {
        [Required]
        public int Id { get; set; }   

        [Required(ErrorMessage = "L'ospedale è obbligatoria"),
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