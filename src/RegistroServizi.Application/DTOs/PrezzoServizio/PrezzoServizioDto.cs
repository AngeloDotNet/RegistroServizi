namespace RegistroServizi.Application.DTOs.PrezzoServizio;

/// <summary>
/// DTO per la rappresentazione dei prezzi dei servizi.
/// </summary>
//public class PrezzoServizioDto
//{
//    public Guid Id { get; set; }

//    public Guid TipologiaServizioId { get; set; }
//    public TipologiaServizio TipologiaServizio { get; set; } = default!;

//    public decimal CostoFisso { get; set; }
//    public decimal CostoKm { get; set; }
//    public decimal SecondoTrasportato { get; set; }
//    public decimal FermoMacchina { get; set; }
//    public decimal? Accompagnatore { get; set; }
//    public int? ScontoSocio { get; set; }
//}
public class PrezzoServizioDto
{
    public Guid Id { get; set; }
    public Guid TipologiaServizioId { get; set; }
    public string TipoServizio { get; set; } = string.Empty;

    public decimal CostoFisso { get; set; }
    public decimal CostoKm { get; set; }
    public decimal SecondoTrasportato { get; set; }
    public decimal FermoMacchina { get; set; }
    public decimal? Accompagnatore { get; set; }
    public int? ScontoSocio { get; set; }
}