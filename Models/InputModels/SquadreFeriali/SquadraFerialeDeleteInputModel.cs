using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.InputModels.SquadreFeriali
{
    public class SquadraFerialeDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}