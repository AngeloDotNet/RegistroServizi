using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Models.Enums
{
    public enum Privacy
    {
        [Display(Name = "Autorizzato")]
        SI,

        [Display(Name = "Non Autorizzato")]
        NO
    }
}