namespace RegistroServizi.Domain.Entities;

/// <summary>
/// Represents a team assigned to provide service during a holiday period.
/// </summary>
/// <remarks>
/// The inherited identifier allows the holiday team assignment to be persisted
/// and referenced consistently across the domain model.
/// </remarks>
public class SquadraFestivo : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the team assigned to the holiday service.
    /// </summary>
    public string Squadra { get; set; } = string.Empty;
}