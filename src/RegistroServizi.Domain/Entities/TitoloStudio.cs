using RegistroServizi.Domain.Common;

namespace RegistroServizi.Domain.Entities;

public class TitoloStudio : BaseEntity
{
    public string Descrizione { get; set; } = string.Empty;
}