namespace RegistroServizi.Application.DTOs.Applicazione;

//public record class ApplicazioneDto(Guid Id, string NomeApplicazione, string Versione);

/// <summary>
/// DTO per la visualizzazione di un'applicazione
/// </summary>
public class ApplicazioneDto
{
    public Guid Id { get; set; }
    public string NomeApplicazione { get; set; } = string.Empty;
    public string Versione { get; set; } = string.Empty;
}
