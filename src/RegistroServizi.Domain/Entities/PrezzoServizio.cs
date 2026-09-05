namespace RegistroServizi.Domain.Entities;

public class PrezzoServizio : BaseEntity
{
    public TipologiaServizio TipologiaServizio { get; set; }
    public decimal CostoFisso { get; set; }
    public decimal CostoKm { get; set; }
    public decimal SecondoTrasportato { get; set; }
    public decimal FermoMacchina { get; set; }
    public decimal? Accompagnatore { get; set; }
    public int? ScontoSocio { get; set; }
}