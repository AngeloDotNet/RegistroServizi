namespace RegistroServizi.Application.Options;

/// <summary>
/// Represents configuration values used by the application at startup and runtime.
/// </summary>
/// <remarks>
/// This type is intended to be bound from configuration using the options pattern,
/// for example through `IConfiguration` or `IOptions<ApplicazioneOptions>`.
/// The properties are required and should be populated with valid non-empty values
/// before the options instance is consumed by the application.
/// </remarks>
public class ApplicazioneOptions
{
    /// <summary>
    /// Gets or sets the application name.
    /// </summary>
    /// <remarks>
    /// This value identifies the application in configuration, logs, and user-facing
    /// diagnostics. It is required and must not be empty.
    /// </remarks>
    [Required] public string NomeApplicazione { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the application version.
    /// </summary>
    /// <remarks>
    /// This value should contain a meaningful version identifier, such as a semantic
    /// version or build number. It is required and must not be empty.
    /// </remarks>
    [Required] public string VersioneApplicazione { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the time zone identifier used by the application.
    /// </summary>
    /// <remarks>
    /// The value should be a valid time zone identifier supported by the application,
    /// such as an IANA identifier or a Windows time zone ID, depending on the runtime
    /// environment and configuration strategy.
    /// </remarks>
    [Required] public string TimeZone { get; set; } = string.Empty;
}