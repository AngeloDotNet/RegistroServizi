namespace RegistroServizi.Domain.Entities;

public class TipologiaServizio : BaseEntity
{
    public string TipoServizio { get; set; } = string.Empty;

    // Navigation property for PrezzoServizio
    public ICollection<PrezzoServizio> PrezziServizi { get; set; } = [];
}