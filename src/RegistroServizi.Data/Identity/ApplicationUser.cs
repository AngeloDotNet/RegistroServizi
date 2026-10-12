namespace RegistroServizi.Data.Identity;

/// <summary>
/// Represents the application's concrete Identity user type.
/// </summary>
/// <remarks>
/// Use this class as the user entity for ASP.NET Core Identity when the application needs to extend the
/// built-in <see cref="IdentityUser"/> model with custom profile, preference, or domain-specific properties.
/// </remarks>
public class ApplicationUser : IdentityUser
{
    // Add custom user properties here as needed.
}