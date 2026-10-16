namespace RegistroServizi.Data.Identity;

/// <summary>
/// Represents the user entity used by the application's ASP.NET Core Identity configuration.
/// </summary>
/// <remarks>
/// This type provides the application's extension point for user-specific profile, preference, and
/// domain properties while retaining the standard Identity behavior supplied by <see cref="IdentityUser"/>.
/// </remarks>
public class ApplicationUser : IdentityUser
{
    // Add custom user properties here as needed.
}