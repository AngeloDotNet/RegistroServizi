namespace RegistroServizi.Application.Options;

/// <summary>
/// Represents options for creating an administrative user, including username, email, and password.
/// </summary>
/// <remarks>All properties are required. Validate inputs and handle the password securely according to
/// application password policies.</remarks>
public class AdminUserOptions
{
    /// <summary>
    /// The username of the user account.
    /// </summary>
    /// <remarks>Decorated with [Required] for model validation. Initialized to an empty string to avoid null;
    /// model validation will treat empty strings as invalid unless AllowEmptyStrings is true.</remarks>
    [Required] public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    /// <remarks>Marked with [Required]. Initialized to an empty string.</remarks>
    [Required] public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The account password.
    /// </summary>
    /// <remarks>Required. Handle securely: avoid logging, minimize time in memory, and persist only after
    /// hashing or using a secure credential store.</remarks>
    [Required] public string Password { get; set; } = string.Empty;
}