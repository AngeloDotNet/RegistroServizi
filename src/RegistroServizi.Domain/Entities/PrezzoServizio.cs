namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents the pricing configuration used to calculate the cost of a specific service type.
/// </summary>
/// <remarks>
/// The configuration centralizes the fixed service charge, distance-based pricing, passenger-related
/// charges, vehicle standby costs, and any applicable member discount for a
/// <see cref="TipologiaServizio"/>.
/// <para>
/// Monetary amounts are expressed in the applicable currency, while
/// <see cref="ScontoSocio"/> represents a percentage.
/// </para>
/// </remarks>
public class PrezzoServizio : BaseEntity
{
    /// <summary>
    /// Gets or sets the identifier of the service type to which this pricing configuration applies.
    /// </summary>
    public Guid TipologiaServizioId { get; set; }

    /// <summary>
    /// Gets or sets the fixed charge applied to the service before variable charges are calculated.
    /// </summary>
    public decimal CostoFisso { get; set; }

    /// <summary>
    /// Gets or sets the charge applied for each kilometer traveled.
    /// </summary>
    public decimal CostoKm { get; set; }

    /// <summary>
    /// Gets or sets the additional charge for transporting a second passenger.
    /// </summary>
    public decimal SecondoTrasportato { get; set; }

    /// <summary>
    /// Gets or sets the charge applied when the vehicle remains idle during the service.
    /// </summary>
    public decimal FermoMacchina { get; set; }

    /// <summary>
    /// Gets or sets the optional additional charge for an accompanying person.
    /// </summary>
    /// <value>
    /// The additional charge, or <see langword="null"/> when no accompanying-person charge applies.
    /// </value>
    public decimal? Accompagnatore { get; set; }

    /// <summary>
    /// Gets or sets the optional percentage discount applied to eligible members.
    /// </summary>
    /// <value>
    /// The member discount percentage, or <see langword="null"/> when no member discount applies.
    /// </value>
    public int? ScontoSocio { get; set; }

    /// <summary>
    /// Gets or sets the service type associated with this pricing configuration.
    /// </summary>
    public TipologiaServizio TipologiaServizio { get; set; } = null!;
}