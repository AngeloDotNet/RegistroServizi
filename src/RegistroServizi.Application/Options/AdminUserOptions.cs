namespace RegistroServizi.Application.Options;

/// <summary>
/// Configuration values used to create an initial administrative user.
/// </summary>
/// <remarks>
/// All properties are required and are intended to be bound from configuration or input during setup.
/// Validate values before use, and handle the password securely in accordance with application policy.
/// </remarks>
public class AdminUserOptions
{
    /// <summary>
    /// Gets or sets the administrative user's login name.
    /// </summary>
    /// <remarks>
    /// This value is required and defaults to an empty string to avoid null references during binding.
    /// </remarks>
    [Required] public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the administrative user's email address.
    /// </summary>
    /// <remarks>
    /// This value is required and defaults to an empty string to avoid null references during binding.
    /// </remarks>
    [Required] public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the administrative user's password.
    /// </summary>
    /// <remarks>
    /// This value is required. Do not log it or persist it in plain text; hash or store it securely as soon as possible.
    /// </remarks>
    [Required] public string Password { get; set; } = string.Empty;
}