namespace RegistroServizi.Application.DTOs.Applicazione;

/// <summary>
/// Data Transfer Object (DTO) for creating a new application.
/// </summary>
/// <param name="NomeApplicazione"></param>
/// <param name="Versione"></param>
/// <param name="TimeZone"></param>
public record class CreateApplicazioneDto(string NomeApplicazione, string Versione, string TimeZone);