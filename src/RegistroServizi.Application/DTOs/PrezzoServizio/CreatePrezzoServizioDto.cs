namespace RegistroServizi.Application.DTOs.PrezzoServizio;

/// <summary>
/// Rappresenta i dati necessari per creare un nuovo prezzo di servizio.
/// </summary>
/// <param name="TipologiaServizio">Tipologia di servizio associata al prezzo.</param>
/// <param name="CostoFisso">Costo fisso del servizio.</param>
/// <param name="CostoKm">Costo applicato per chilometro.</param>
/// <param name="SecondoTrasportato">Costo applicato per ogni secondo trasportato.</param>
/// <param name="FermoMacchina">Costo applicato per il fermo macchina.</param>
/// <param name="Accompagnatore">Costo opzionale per l'accompagnatore.</param>
/// <param name="ScontoSocio">Sconto opzionale riservato al socio.</param>
public record class CreatePrezzoServizioDto(
    TipologiaServizio TipologiaServizio,
    decimal CostoFisso,
    decimal CostoKm,
    decimal SecondoTrasportato,
    decimal FermoMacchina,
    decimal? Accompagnatore,
    int? ScontoSocio);