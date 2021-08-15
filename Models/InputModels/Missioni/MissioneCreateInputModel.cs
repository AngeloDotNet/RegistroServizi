using System.ComponentModel.DataAnnotations;
using RegistroServizi.Models.Enums;

namespace RegistroServizi.Models.InputModels.Missioni
{
    public class MissioneCreateInputModel
    {
        [Required]
        public TipologiaMissione Tipologia { get; set; }
    }
}