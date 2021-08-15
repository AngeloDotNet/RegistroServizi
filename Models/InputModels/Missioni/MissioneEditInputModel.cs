using System;
using System.ComponentModel.DataAnnotations;
using RegistroServizi.Models.Enums;

namespace RegistroServizi.Models.InputModels.Missioni
{
    public class MissioneEditInputModel
    {
        public int Id { get; set; }
        [Required]
        public string Titolo { get; set; }
        public DateTime Data { get; set; }
        public TipologiaMissione Tipologia { get; set; }
    }
}