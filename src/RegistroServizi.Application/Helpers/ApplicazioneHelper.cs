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
    public static ApplicazioneDto MapApplicazioneToDto(Applicazione applicazione) => new ApplicazioneDto(applicazione.Id, applicazione.NomeApplicazione, applicazione.Versione);

    /// <summary>
    /// Maps an Applicazione entity to a DetailApplicazioneDto.
    /// </summary>
    /// <param name="applicazione"></param>
    /// <returns></returns>
    //public static DetailApplicazioneDto MapApplicazioneToDetailDto(Applicazione applicazione) => new DetailApplicazioneDto(applicazione.Id, applicazione.NomeApplicazione, applicazione.Versione);
}
