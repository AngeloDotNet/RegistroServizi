using System;
using RegistroServizi.Models.Enums;

namespace RegistroServizi.Models.ViewModels.Missioni
{
    public class MissioneViewModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Titolo { get; set; }
        public TipologiaMissione Tipologia { get; set; }
    }
}