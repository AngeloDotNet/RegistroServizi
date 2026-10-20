namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents a category used to classify the services managed by the application.
/// </summary>
/// <remarks>
/// A service category has a human-readable name and can be associated with multiple
/// <see cref="PrezzoServizio"/> records. The inherited <see cref="BaseEntity"/> members
/// provide the entity's common identity and persistence-related metadata.
/// </remarks>
public class TipologiaServizio : BaseEntity
{
    /// <summary>
    /// Gets or sets the human-readable name assigned to this service category.
    /// </summary>
    /// <value>
    /// The category name. The default value is an empty string.
    /// </value>
    public string TipoServizio { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the prices associated with this service category.
    /// </summary>
    /// <value>
    /// A collection of <see cref="PrezzoServizio"/> entities linked to this category.
    /// The collection is initialized empty so the navigation property is available
    /// immediately after the entity is created.
    /// </value>
    public ICollection<PrezzoServizio> PrezziServizi { get; set; } = [];
}