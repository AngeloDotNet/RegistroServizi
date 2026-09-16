namespace RegistroServizi.Domain.Entities;

public class Applicazione : BaseEntity
{
    public string NomeApplicazione { get; set; } = string.Empty;
    public string Versione { get; set; } = string.Empty;
    public string TimeZone { get; set; } = string.Empty;
}