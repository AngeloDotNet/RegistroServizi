namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents a hospital with a name, postal address, and geographic coordinates.
/// </summary>
/// <remarks>Inherits from BaseEntity. NomeOspedale defaults to an empty string; Indirizzo and Coordinate are
/// initialized to non-null defaults and represent the hospital's address and location.</remarks>
public class Ospedale : BaseEntity
{
    public string NomeOspedale { get; set; } = string.Empty;
    public Indirizzo Indirizzo { get; set; } = default!;
    public Coordinate Coordinate { get; set; } = default!;
}