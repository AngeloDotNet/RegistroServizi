using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Controllers;

namespace RegistroServizi.Models.InputModels.SociFamiliari
{
    public class SocioFamiliareCreateInputModel
    {
        [Required]
        public int SocioId { get; set; }

        [Required(ErrorMessage = "Il familiare è obbligatorio"),
         Remote(action: nameof(SociFamiliariController.IsSocioFamiliareAvailable), controller: "SociFamiliari", ErrorMessage = "Il familiare indicato esiste già"),
         Display(Name = "Familiare")]
        public string Familiare { get; set; }
    }
}