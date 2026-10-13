namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents a hospital in the domain model.
/// </summary>
/// <remarks>
/// Inherits from <see cref="BaseEntity"/>. The <see cref="NomeOspedale"/> property defaults to an empty string,
/// while <see cref="Ubicazione"/> and <see cref="Coordinate"/> are expected to be assigned before use.
/// </remarks>
public class Ospedale : BaseEntity
{
    /// <summary>
    /// Gets or sets the hospital name.
    /// </summary>
    public string NomeOspedale { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hospital postal address.
    /// </summary>
    public Ubicazione Ubicazione { get; set; } = default!;

    /// <summary>
    /// Gets or sets the hospital geographic coordinates.
    /// </summary>
    public Coordinate Coordinate { get; set; } = default!;
}