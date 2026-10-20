namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents an electric vehicle charging station managed by the domain.
/// </summary>
/// <remarks>
/// The entity combines identifying information inherited from <see cref="BaseEntity"/> with
/// the station's display name, municipality, province, and geographic location.
/// <see cref="Coordinate"/> must be assigned before the entity is used for location-based
/// operations. Its latitude and longitude are expressed in decimal degrees using WGS84.
/// </remarks>
public class Colonnina : BaseEntity
{
    /// <summary>
    /// Gets or sets the name used to identify the charging station.
    /// </summary>
    public string NomeColonnina { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the municipality in which the charging station is located.
    /// </summary>
    public string Comune { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the province in which the charging station is located.
    /// </summary>
    public string Provincia { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographic location of the charging station.
    /// </summary>
    /// <remarks>
    /// The coordinate is expressed using latitude and longitude in decimal degrees.
    /// Individual coordinate components may be unknown, but the coordinate object itself
    /// should be assigned before the station is used.
    /// </remarks>
    public Coordinate Coordinate { get; set; } = default!;
}