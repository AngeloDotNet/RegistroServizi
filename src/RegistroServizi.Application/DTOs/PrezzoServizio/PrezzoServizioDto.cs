namespace RegistroServizi.Application.DTOs.PrezzoServizio;

/// <summary>
/// Data Transfer Object (DTO) representing a PrezzoServizio.
/// </summary>
/// <param name="Id"></param>
/// <param name="TipologiaServizio"></param>
/// <param name="CostoFisso"></param>
/// <param name="CostoKm"></param>
/// <param name="SecondoTrasportato"></param>
/// <param name="FermoMacchina"></param>
/// <param name="Accompagnatore"></param>
/// <param name="ScontoSocio"></param>
public record class PrezzoServizioDto(
    Guid Id,
    TipologiaServizio TipologiaServizio,
    decimal CostoFisso,
    decimal CostoKm,
    decimal SecondoTrasportato,
    decimal FermoMacchina,
    decimal? Accompagnatore,
    int? ScontoSocio);