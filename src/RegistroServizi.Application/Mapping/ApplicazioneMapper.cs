namespace RegistroServizi.Application.Mapping;

/// <summary>
/// Provides static mapping helpers to convert Applicazione domain entities to DTOs and to create updated Applicazione
/// entities from update DTOs, and exposes a compiled regular expression for validating simple numeric version strings.
/// </summary>
/// <remarks>Stateless and thread-safe. VersionRegex is a compiled, anchored pattern that matches only
/// dot-separated integer segments (for example, '1', '1.2', '10.0.3') and does not allow whitespace, pre-release
/// labels, build metadata, or non-numeric identifiers; it does not enforce numeric range or semantic-version
/// rules.</remarks>
public static class ApplicazioneMapper
{
    /// <summary>
    /// Regular expression that matches simple numeric version strings composed of one or more dot-separated integer
    /// segments (for example, "1", "1.2", "10.0.3").
    /// </summary>
    /// <remarks>Compiled for performance. Pattern is anchored and does not allow whitespace, pre-release
    /// labels, build metadata, or non-numeric identifiers; it does not enforce numeric range or semantic-version
    /// rules.</remarks>
    public static readonly Regex VersionRegex = new(@"^\d+(\.\d+)*$", RegexOptions.Compiled);

    /// <summary>
    /// Creates an ApplicazioneDto with values copied from the specified Applicazione.
    /// </summary>
    /// <param name="applicazione">The source Applicazione from which to copy values.</param>
    /// <returns>An ApplicazioneDto with Id, NomeApplicazione, Versione, and TimeZone set from the source.</returns>
    public static ApplicazioneDto MapApplicazioneToDto(Applicazione applicazione) => new ApplicazioneDto
    {
        Id = applicazione.Id,
        NomeApplicazione = applicazione.NomeApplicazione,
        Versione = applicazione.Versione,
        TimeZone = applicazione.TimeZone
    };

    /// <summary>
    /// Map an UpdateApplicazioneDto to an Applicazione entity for update.
    /// </summary>
    /// <param name="dtoUpdate">Update DTO containing Id, NomeApplicazione, Versione, and TimeZone to apply to the entity.</param>
    /// <returns>An Applicazione entity populated with values from the DTO.</returns>
    public static Applicazione MapApplicazioneToEntityUpdate(UpdateApplicazioneDto dtoUpdate) => new()
    {
        Id = dtoUpdate.Id,
        NomeApplicazione = dtoUpdate.NomeApplicazione,
        Versione = dtoUpdate.Versione,
        TimeZone = dtoUpdate.TimeZone
    };
}