namespace RegistroServizi.Application.Options;

/// <summary>
/// Options for configuring the application, including name, version, and time zone.
/// </summary>
/// <remarks>Designed for use with the options pattern; typically bound from configuration (for example via
/// IConfiguration or IOptions<ApplicazioneOptions>).</remarks>
public class ApplicazioneOptions
{
    /// <summary>
    /// Gets or sets the application name.
    /// </summary>
    /// <remarks>Required for model validation. Initialized to an empty string.</remarks>
    [Required] public string NomeApplicazione { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the application version.
    /// </summary>
    /// <remarks>Marked as required for validation. Defaults to an empty string; should contain a non-empty
    /// version identifier when the model is validated.</remarks>
    [Required] public string VersioneApplicazione { get; set; } = string.Empty;

    /// <summary>
    /// Time zone identifier for the entity, such as an IANA identifier (e.g., "America/New_York") or a Windows
    /// identifier (e.g., "Pacific Standard Time").
    /// </summary>
    /// <remarks>Required. Must be a valid time-zone identifier; validation should enforce the supported
    /// format(s) for the application and handle normalization or mapping between IANA and Windows IDs if
    /// needed.</remarks>
    [Required] public string TimeZone { get; set; } = string.Empty;
}
