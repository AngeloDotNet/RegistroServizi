namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents the status of a delivery note.
/// </summary>
/// <remarks>
/// Inherits from <see cref="BaseEntity"/> and exposes a descriptive label for display purposes.
/// </remarks>
public class StatoBolla : BaseEntity
{
    /// <summary>
    /// Gets or sets the human-readable description of the delivery note status.
    /// </summary>
    public string Descrizione { get; set; } = string.Empty;
}