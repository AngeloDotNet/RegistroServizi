namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents an academic qualification or educational title.
/// </summary>
/// <remarks>
/// This entity inherits from <see cref="BaseEntity"/> and is used to store the
/// human-readable description of a study title.
/// </remarks>
public class TitoloStudio : BaseEntity
{
    /// <summary>
    /// Gets or sets the descriptive name of the study title.
    /// </summary>
    /// <remarks>
    /// Use this property to store the display label shown to users.
    /// </remarks>
    public string Descrizione { get; set; } = string.Empty;
}