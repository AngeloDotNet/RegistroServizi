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
//public record class PrezzoServizioDto(
//    Guid Id,
//    TipologiaServizio TipologiaServizio,
//    decimal CostoFisso,
//    decimal CostoKm,
//    decimal SecondoTrasportato,
//    decimal FermoMacchina,
//    decimal? Accompagnatore,
//    int? ScontoSocio);

public record class PrezzoServizioDto
{
    public Guid Id { get; set; }
    public TipologiaServizio TipologiaServizio { get; set; } = default!;
    public decimal CostoFisso { get; set; }
    public decimal CostoKm { get; set; }
    public decimal SecondoTrasportato { get; set; }
    public decimal FermoMacchina { get; set; }
    public decimal? Accompagnatore { get; set; }
    public int? ScontoSocio { get; set; }
}