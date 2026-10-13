namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents a service category identified by a human-readable name.
/// </summary>
/// <remarks>
/// This entity derives from <see cref="BaseEntity"/> and is related to
/// multiple <see cref="PrezzoServizio"/> entries through the
/// <see cref="PrezziServizi"/> navigation property.
/// </remarks>
public class TipologiaServizio : BaseEntity
{
    /// <summary>
    /// Gets or sets the display name of the service type.
    /// </summary>
    public string TipoServizio { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of prices associated with this service type.
    /// </summary>
    public ICollection<PrezzoServizio> PrezziServizi { get; set; } = [];
}