using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.InputModels.Soci
{
    public class SocioDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}