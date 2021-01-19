using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.InputModels.SociFamiliari
{
    public class SocioFamiliareDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
        public int SocioId { get; set; }
    }
}