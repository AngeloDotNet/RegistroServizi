using System.ComponentModel.DataAnnotations;

namespace RegistroServizi.Domain.Enums;

public enum Role
{
    [Display(Name = "Admin")]
    Admin,
    [Display(Name = "Manager")]
    Manager,
    [Display(Name = "Operator")]
    Operator
}