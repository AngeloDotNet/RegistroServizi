namespace RegistroServizi.Data.Identity;

/// <summary>
/// Represents an application user that extends IdentityUser and can be extended with additional user-specific
/// properties for authentication and authorization.
/// </summary>
/// <remarks>Use as the concrete Identity user type for the application; add profile, preference, or domain
/// properties as needed.</remarks>
public class ApplicationUser : IdentityUser
{
    // Additional properties can be added here if needed
}