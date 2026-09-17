namespace RegistroServizi.Application.Mapping;

/// <summary>
/// Provides mapping methods for converting between Applicazione entities and DTOs.
/// </summary>
public static class ApplicazioneMapper
{
    /// <summary>
    /// Validates the version string format.
    /// </summary>
    public static readonly Regex VersionRegex = new(@"^\d+(\.\d+)*$", RegexOptions.Compiled);

    /// <summary>
    /// Maps an Applicazione entity to an ApplicazioneDto.
    /// </summary>
    /// <param name="applicazione"></param>
    /// <returns></returns>
    public static ApplicazioneDto MapApplicazioneToDto(Applicazione applicazione) => new ApplicazioneDto
    {
        Id = applicazione.Id,
        NomeApplicazione = applicazione.NomeApplicazione,
        Versione = applicazione.Versione,
        TimeZone = applicazione.TimeZone
    };

    /// <summary>
    /// Maps an UpdateApplicazioneDto to an Applicazione entity for update operations.
    /// </summary>
    /// <param name="dtoUpdate"></param>
    /// <returns></returns>
    public static Applicazione MapApplicazioneToEntityUpdate(UpdateApplicazioneDto dtoUpdate) => new()
    {
        Id = dtoUpdate.Id,
        NomeApplicazione = dtoUpdate.NomeApplicazione,
        Versione = dtoUpdate.Versione,
        TimeZone = dtoUpdate.TimeZone
    };
}