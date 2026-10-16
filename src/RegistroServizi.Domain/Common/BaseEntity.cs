namespace RegistroServizi.Domain.Common;

/// <summary>
/// Defines the shared identity required by domain entities.
/// </summary>
/// <remarks>
/// Derive domain entities from this type when they are uniquely identified by a single
/// <see cref="Guid"/> value. Centralizing the identifier keeps entity contracts consistent
/// across the domain model and allows persistence infrastructure to address entities uniformly.
/// </remarks>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier assigned to the entity.
    /// </summary>
    /// <remarks>
    /// A newly created entity may contain <see cref="Guid.Empty"/> until the identifier is
    /// assigned by the application or persistence infrastructure.
    /// </remarks>
    public Guid Id { get; set; }
}