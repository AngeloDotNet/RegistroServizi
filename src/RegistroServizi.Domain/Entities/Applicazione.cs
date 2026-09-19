namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents application metadata including name, version, and time zone.
/// </summary>
/// <remarks>Inherits from BaseEntity and serves as a simple data model for storing application configuration and
/// metadata.</remarks>
public class Applicazione : BaseEntity
{
    public string NomeApplicazione { get; set; } = string.Empty;
    public string Versione { get; set; } = string.Empty;
    public string TimeZone { get; set; } = string.Empty;
}