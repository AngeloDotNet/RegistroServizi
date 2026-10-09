namespace RegistroServizi.Application.DTOs.Colonnina;

/// <summary>
/// Represents a charging station DTO used to transfer station details within the application layer.
/// </summary>
public class ColonninaDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the charging station.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the charging station.
    /// </summary>
    public string NomeColonnina { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the municipality where the charging station is located.
    /// </summary>
    public string Comune { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the province where the charging station is located.
    /// </summary>
    public string Provincia { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the geographic coordinates of the charging station.
    /// </summary>
    //public CoordinateDto Coordinate { get; set; } = new CoordinateDto();
}