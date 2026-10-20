namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents a hospital that can be registered and located within the service domain.
/// </summary>
/// <remarks>
/// A hospital is identified by the <see cref="BaseEntity.Id"/> inherited from
/// <see cref="BaseEntity"/> and combines its descriptive name, postal address, and
/// geographic position for use by services that need to display or locate healthcare facilities.
/// <para>
/// The <see cref="Ubicazione"/> and <see cref="Coordinate"/> properties must be assigned
/// before the entity is used in application workflows.
/// </para>
/// </remarks>
public class Ospedale : BaseEntity
{
    /// <summary>
    /// Gets or sets the name by which the hospital is identified.
    /// </summary>
    /// <value>
    /// The hospital name, or an empty string when no name has been assigned.
    /// </value>
    public string NomeOspedale { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hospital's postal address.
    /// </summary>
    /// <value>
    /// An <see cref="Ubicazione"/> containing the street address, municipality,
    /// province, and optional postal code.
    /// </value>
    public Ubicazione Ubicazione { get; set; } = default!;

    /// <summary>
    /// Gets or sets the hospital's geographic position.
    /// </summary>
    /// <value>
    /// A <see cref="Coordinate"/> containing the latitude and longitude in decimal degrees,
    /// using the WGS84 geographic reference system.
    /// </value>
    public Coordinate Coordinate { get; set; } = default!;
}