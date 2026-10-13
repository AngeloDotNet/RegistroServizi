namespace RegistroServizi.Application.Options;

/// <summary>
/// Represents configuration settings for an association.
/// </summary>
/// <remarks>
/// These options are typically bound from configuration and require both the full name
/// and the abbreviation to be provided.
/// </remarks>
public class AssociazioneOptions
{
    /// <summary>
    /// Gets or sets the full name of the association.
    /// </summary>
    /// <remarks>
    /// This value is required and defaults to an empty string until configuration is bound.
    /// </remarks>
    [Required] public string NomeAssociazione { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the association abbreviation.
    /// </summary>
    /// <remarks>
    /// This value is required and defaults to an empty string until configuration is bound.
    /// </remarks>
    [Required] public string SiglaAssociazione { get; set; } = string.Empty;
}