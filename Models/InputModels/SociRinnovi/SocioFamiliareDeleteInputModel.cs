using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.InputModels.SociRinnovi
{
    public class SocioRinnovoDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
        public int SocioId { get; set; }
    }
}