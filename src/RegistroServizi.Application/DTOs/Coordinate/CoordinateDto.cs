namespace RegistroServizi.Application.DTOs.Coordinate;

/// <summary>
/// Represents a geographic coordinate with latitude and longitude values.
/// </summary>
public class CoordinateDto
{
    /// <summary>
    /// Gets or sets the latitude component of the coordinate.
    /// </summary>
    public double Latitudine { get; set; } = 0;

    /// <summary>
    /// Gets or sets the longitude component of the coordinate.
    /// </summary>
    public double Longitudine { get; set; } = 0;
}