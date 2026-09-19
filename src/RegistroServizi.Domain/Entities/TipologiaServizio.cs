namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents a service type with a human-readable name and a collection of associated PrezzoServizio entities.
/// </summary>
/// <remarks>Persistent entity derived from BaseEntity. The PrezziServizi navigation property models a one-to-many
/// relationship to PrezzoServizio; TipoServizio holds the service name.</remarks>
public class TipologiaServizio : BaseEntity
{
    public string TipoServizio { get; set; } = string.Empty;

    // Navigation property for PrezzoServizio
    public ICollection<PrezzoServizio> PrezziServizi { get; set; } = [];
}