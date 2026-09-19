namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents an educational qualification or academic title.
/// </summary>
/// <remarks>Inherits from BaseEntity. Use the Descrizione property for the human-readable name or
/// label.</remarks>
public class TitoloStudio : BaseEntity
{
    public string Descrizione { get; set; } = string.Empty;
}