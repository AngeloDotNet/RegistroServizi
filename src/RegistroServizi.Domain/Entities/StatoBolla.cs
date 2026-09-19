namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents the state of a delivery note.
/// </summary>
/// <remarks>Inherits from BaseEntity and exposes a Descrizione property for a human-readable label.</remarks>
public class StatoBolla : BaseEntity
{
    public string Descrizione { get; set; } = string.Empty;
}