namespace RegistroServizi.Application.DTOs.PrezzoServizio;

/// <summary>
/// Represents the pricing details for a service type.
/// </summary>
public class PrezzoServizioDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the price record.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated service type.
    /// </summary>
    public Guid TipologiaServizioId { get; set; }

    /// <summary>
    /// Gets or sets the name of the service type.
    /// </summary>
    public string TipoServizio { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the fixed cost component.
    /// </summary>
    public decimal CostoFisso { get; set; }

    /// <summary>
    /// Gets or sets the cost per kilometer.
    /// </summary>
    public decimal CostoKm { get; set; }

    /// <summary>
    /// Gets or sets the cost per transported second.
    /// </summary>
    public decimal SecondoTrasportato { get; set; }

    /// <summary>
    /// Gets or sets the cost for vehicle standby time.
    /// </summary>
    public decimal FermoMacchina { get; set; }

    /// <summary>
    /// Gets or sets the optional accompaniment cost.
    /// </summary>
    public decimal? Accompagnatore { get; set; }

    /// <summary>
    /// Gets or sets the optional discount percentage for members.
    /// </summary>
    public int? ScontoSocio { get; set; }
}