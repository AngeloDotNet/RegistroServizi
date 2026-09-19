namespace RegistroServizi.Domain.Common;

/// <summary>
/// Represents an abstract base class for domain entities that exposes a unique identifier.
/// </summary>
/// <remarks>Intended to be inherited by concrete entity types. The Id property is a Guid that serves as the
/// entity identifier; assignment and lifecycle semantics are determined by the persistence strategy (client-side
/// generation or database-generated). Override equality and hashing as needed to reflect entity identity.</remarks>
public abstract class BaseEntity
{
    public Guid Id { get; set; }
}
