namespace RegistroServizi.Application.DTOs.Applicazione;

/// <summary>
/// Data Transfer Object (DTO) for updating an existing application.
/// </summary>
/// <param name="Id"></param>
/// <param name="NomeApplicazione"></param>
/// <param name="Versione"></param>
/// <param name="TimeZone"></param>
public record class UpdateApplicazioneDto(Guid Id, string NomeApplicazione, string Versione, string TimeZone);
