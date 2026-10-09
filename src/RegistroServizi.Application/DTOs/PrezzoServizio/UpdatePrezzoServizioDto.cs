namespace RegistroServizi.Application.DTOs.PrezzoServizio;

/// <summary>
/// Represents the data required to update a <c>PrezzoServizio</c> record.
/// </summary>
/// <param name="Id">The unique identifier of the price record to update.</param>
/// <param name="TipologiaServizioId">The identifier of the related service type.</param>
/// <param name="CostoFisso">The fixed cost for the service.</param>
/// <param name="CostoKm">The cost per kilometre.</param>
/// <param name="SecondoTrasportato">The cost per transported second.</param>
/// <param name="FermoMacchina">The cost for vehicle idle time.</param>
/// <param name="Accompagnatore">The optional additional cost for an accompanying person.</param>
/// <param name="ScontoSocio">The optional discount percentage or amount reserved for members.</param>
public record class UpdatePrezzoServizioDto(
    Guid Id,
    Guid TipologiaServizioId,
    decimal CostoFisso,
    decimal CostoKm,
    decimal SecondoTrasportato,
    decimal FermoMacchina,
    decimal? Accompagnatore,
    int? ScontoSocio);