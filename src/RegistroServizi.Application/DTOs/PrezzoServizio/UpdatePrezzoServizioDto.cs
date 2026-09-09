namespace RegistroServizi.Application.DTOs.PrezzoServizio;

/// <summary>
/// DTO per l'aggiornamento di un prezzo servizio
/// </summary>
/// <param name="Id">Identificativo del prezzo servizio</param>
/// <param name="TipologiaServizioId">Identificativo della tipologia di servizio</param>
/// <param name="CostoFisso">Costo fisso del servizio</param>
/// <param name="CostoKm">Costo per chilometro del servizio</param>
/// <param name="SecondoTrasportato">Costo per secondo trasportato del servizio</param>
/// <param name="FermoMacchina">Costo per fermo macchina del servizio</param>
/// <param name="Accompagnatore">Costo per accompagnatore del servizio</param>
/// <param name="ScontoSocio">Sconto per i soci del servizio</param>
public record class UpdatePrezzoServizioDto(
    Guid Id,
    Guid TipologiaServizioId,
    decimal CostoFisso,
    decimal CostoKm,
    decimal SecondoTrasportato,
    decimal FermoMacchina,
    decimal? Accompagnatore,
    int? ScontoSocio);