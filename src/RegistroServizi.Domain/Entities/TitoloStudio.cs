namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents an educational qualification that can be associated with a person
/// or used as a reference value within the registry.
/// </summary>
/// <remarks>
/// The entity inherits its unique identifier from <see cref="BaseEntity"/>.
/// </remarks>
public class TitoloStudio : BaseEntity
{
    /// <summary>
    /// Gets or sets the human-readable name of the educational qualification.
    /// </summary>
    /// <remarks>
    /// This value is intended for display in lists, forms, and other user-facing
    /// parts of the application.
    /// </remarks>
    public string Descrizione { get; set; } = string.Empty;
}