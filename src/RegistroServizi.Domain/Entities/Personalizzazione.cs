namespace RegistroServizi.Domain.Entities;

public class Personalizzazione : BaseEntity
{
    public string NomeAssociazione { get; set; } = string.Empty;
    public string SiglaAssociazione { get; set; } = string.Empty;
}