namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents pricing settings for a service type, including fixed and per-kilometer charges, additional fees and an
/// optional member discount.
/// </summary>
/// <remarks>Monetary amounts are represented as decimal values. Accompagnatore is an optional additional fee and
/// ScontoSocio is an optional percentage discount for members. TipologiaServizioId associates the price with a specific
/// service type and TipologiaServizio provides the related navigation.</remarks>
public class PrezzoServizio : BaseEntity
{
    public Guid TipologiaServizioId { get; set; }
    public decimal CostoFisso { get; set; }
    public decimal CostoKm { get; set; }
    public decimal SecondoTrasportato { get; set; }
    public decimal FermoMacchina { get; set; }
    public decimal? Accompagnatore { get; set; }
    public int? ScontoSocio { get; set; }

    // Navigation property for TipologiaServizio
    public TipologiaServizio TipologiaServizio { get; set; } = null!;
}