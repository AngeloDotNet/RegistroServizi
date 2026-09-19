namespace RegistroServizi.Domain.Enums;

/// <summary>
/// Specifies application user roles used for authorization and access control.
/// </summary>
/// <remarks>Intended for role-based authorization. Values include Admin, Manager, and Operator; each value is
/// annotated with a DisplayAttribute to provide a UI-friendly name.</remarks>
public enum Role
{
    [Display(Name = "Admin")]
    Admin,

    [Display(Name = "Manager")]
    Manager,

    [Display(Name = "Operator")]
    Operator
}