namespace RegistroServizi.Application.DTOs.Applicazione;

/// <summary>
/// Data Transfer Object (DTO) for detailed information about an application.
/// </summary>
/// <param name="Id"></param>
/// <param name="NomeApplicazione"></param>
/// <param name="Versione"></param>
public record class DetailApplicazioneDto(Guid Id, string NomeApplicazione, string Versione);