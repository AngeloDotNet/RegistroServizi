namespace RegistroServizi.Application.Options;

/// <summary>
/// Represents options for creating an administrative user, including username, email, and password.
/// </summary>
/// <remarks>All properties are required. Validate inputs and handle the password securely according to
/// application password policies.</remarks>
public class AdminUserOptions
{
    [Required] public string UserName { get; set; } = string.Empty;

    [Required] public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;
}