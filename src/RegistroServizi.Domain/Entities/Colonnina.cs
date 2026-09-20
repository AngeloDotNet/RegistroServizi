namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents a charging column with name, municipality, province, and geographic coordinates.
/// </summary>
/// <remarks>Inherits common identity properties from BaseEntity. NomeColonnina, Comune, and Provincia default to
/// empty strings; Coordinate is required and should contain valid location data.</remarks>
public class Colonnina : BaseEntity
{
    public string NomeColonnina { get; set; } = string.Empty;
    public string Comune { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;
    public Coordinate Coordinate { get; set; } = default!;
}