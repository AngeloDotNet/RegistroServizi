namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents an electric vehicle charging station.
/// </summary>
/// <remarks>
/// Inherits the common identity properties from <see cref="BaseEntity"/>.
/// <see cref="NomeColonnina"/>, <see cref="Comune"/>, and <see cref="Provincia"/> default to empty strings.
/// <see cref="Coordinate"/> is required and should contain a valid location in decimal degrees.
/// </remarks>
public class Colonnina : BaseEntity
{
    /// <summary>
    /// Gets or sets the charging station name.
    /// </summary>
    public string NomeColonnina { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the municipality where the charging station is located.
    /// </summary>
    public string Comune { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the province where the charging station is located.
    /// </summary>
    public string Provincia { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographic coordinate of the charging station.
    /// </summary>
    public Coordinate Coordinate { get; set; } = default!;
}