using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.InputModels.Ospedali
{
    public class OspedaleDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}