namespace RegistroServizi.Domain.Enums;

/// <summary>
/// Represents the application roles that can be assigned to users for authorization and access control.
/// </summary>
/// <remarks>
/// Use these values to enforce role-based behavior across the application.
/// Each member includes a <see cref="DisplayAttribute"/> name for UI-friendly rendering.
/// </remarks>
public enum Role
{
    /// <summary>
    /// Identifies an administrator with full access to the application.
    /// </summary>
    [Display(Name = "Admin")]
    Admin,

    /// <summary>
    /// Identifies a manager with elevated permissions for administrative workflows.
    /// </summary>
    [Display(Name = "Manager")]
    Manager,

    /// <summary>
    /// Identifies an operator with standard operational permissions.
    /// </summary>
    [Display(Name = "Operator")]
    Operator
}