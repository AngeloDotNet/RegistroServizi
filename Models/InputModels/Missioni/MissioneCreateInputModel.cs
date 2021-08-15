using System.ComponentModel.DataAnnotations;
using RegistroServizi.Models.Enums;

namespace RegistroServizi.Models.InputModels.Missioni
{
    public class MissioneCreateInputModel
    {
        [Required]
        [Display(Name = "Tipologia di missione")]
        public TipologiaMissione Tipologia { get; set; }
    }
}