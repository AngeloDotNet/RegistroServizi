namespace RegistroServizi.Domain.Enums;

/// <summary>
/// Defines the roles that determine a user's authorization level within the application.
/// </summary>
/// <remarks>
/// Use these values when assigning roles to users or enforcing role-based authorization.
/// Each member is decorated with a
/// <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute"/> that provides
/// a user-facing name for UI components and other presentation scenarios.
/// </remarks>
public enum Role
{
    /// <summary>
    /// Grants the highest level of access intended for application administration.
    /// </summary>
    [Display(Name = "Admin")]
    Admin,

    /// <summary>
    /// Grants elevated access intended for users responsible for managing operational workflows.
    /// </summary>
    [Display(Name = "Manager")]
    Manager,

    /// <summary>
    /// Grants the standard access required to perform day-to-day operational tasks.
    /// </summary>
    [Display(Name = "Operator")]
    Operator
}