using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.InputModels.Clienti
{
    public class ClienteDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
    }
}