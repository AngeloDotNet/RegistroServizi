namespace RegistroServizi.Domain.Common;

/// <summary>
/// Provides a common base type for domain entities that are identified by a <see cref="Guid"/>.
/// </summary>
/// <remarks>
/// Inherit from this class for entities that share a single identifier property.
/// The value of <see cref="Id"/> is typically assigned either by the application or by persistence
/// infrastructure, depending on the entity lifecycle and storage strategy.
/// </remarks>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public Guid Id { get; set; }
}