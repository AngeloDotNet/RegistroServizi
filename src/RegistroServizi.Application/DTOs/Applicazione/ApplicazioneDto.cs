namespace RegistroServizi.Application.DTOs.Applicazione;

/// <summary>
/// Data Transfer Object (DTO) representing an application.
/// </summary>
/// <param name="Id"></param>
/// <param name="NomeApplicazione"></param>
/// <param name="Versione"></param>
public record class ApplicazioneDto(Guid Id, string NomeApplicazione, string Versione);
