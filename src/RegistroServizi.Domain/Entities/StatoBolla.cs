namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents a delivery note status used to classify its current state within the domain.
/// </summary>
/// <remarks>
/// The status combines the identity inherited from <see cref="BaseEntity"/> with a
/// human-readable label that can be displayed in the user interface.
/// </remarks>
public class StatoBolla : BaseEntity
{
    /// <summary>
    /// Gets or sets the human-readable label associated with the delivery note status.
    /// </summary>
    /// <remarks>
    /// The value is initialized to an empty string so that a newly created status
    /// always has a non-null description.
    /// </remarks>
    public string Descrizione { get; set; } = string.Empty;
}