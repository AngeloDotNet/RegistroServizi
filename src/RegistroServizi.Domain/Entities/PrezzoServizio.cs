namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents the pricing configuration for a specific service type.
/// </summary>
/// <remarks>
/// This entity stores the fixed fee, per-kilometer cost, additional charges, and an optional member discount
/// associated with a <see cref="TipologiaServizio"/>. Monetary values are expressed as <see cref="decimal"/>.
/// </remarks>
public class PrezzoServizio : BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier of the related service type.
    /// </summary>
    public Guid TipologiaServizioId { get; set; }

    /// <summary>
    /// Gets or sets the fixed base cost for the service.
    /// </summary>
    public decimal CostoFisso { get; set; }

    /// <summary>
    /// Gets or sets the cost applied for each kilometer traveled.
    /// </summary>
    public decimal CostoKm { get; set; }

    /// <summary>
    /// Gets or sets the cost applied for the second transported passenger.
    /// </summary>
    public decimal SecondoTrasportato { get; set; }

    /// <summary>
    /// Gets or sets the cost applied when the vehicle remains idle.
    /// </summary>
    public decimal FermoMacchina { get; set; }

    /// <summary>
    /// Gets or sets the optional additional fee for an accompanying person.
    /// </summary>
    public decimal? Accompagnatore { get; set; }

    /// <summary>
    /// Gets or sets the optional discount percentage reserved for members.
    /// </summary>
    public int? ScontoSocio { get; set; }

    /// <summary>
    /// Gets or sets the related service type.
    /// </summary>
    public TipologiaServizio TipologiaServizio { get; set; } = null!;
}