using System.Text.RegularExpressions;

namespace RegistroServizi.Application.Helpers;

/// <summary>
/// Helper class for mapping Applicazione entities to their corresponding DTOs.
/// </summary>
public static class ApplicazioneHelper
{
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
    /// Validates the version string format.
    /// </summary>
    public static readonly Regex VersionRegex = new(@"^\d+(\.\d+)*$", RegexOptions.Compiled);
}