namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents a weekday service team or shift used to organize service assignments.
/// </summary>
/// <remarks>
/// The inherited <see cref="BaseEntity.Id"/> uniquely identifies the team, while
/// <see cref="Squadra"/> stores its human-readable schedule label.
/// </remarks>
public class SquadraFeriale : BaseEntity
{
    /// <summary>
    /// Gets or sets the human-readable name of the weekday team or service shift.
    /// </summary>
    public string Squadra { get; set; } = string.Empty;
}