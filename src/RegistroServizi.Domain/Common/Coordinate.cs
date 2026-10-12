namespace RegistroServizi.Domain.Common;

/// <summary>
/// Represents a geographic coordinate expressed as latitude and longitude in decimal degrees.
/// </summary>
/// <remarks>
/// The coordinate uses the WGS84 geographic reference system.
/// <list type="bullet">
/// <item><description><c>Latitudine</c> must be in the range -90 to 90 when specified.</description></item>
/// <item><description><c>Longitudine</c> must be in the range -180 to 180 when specified.</description></item>
/// <item><description>Either value may be <see langword="null" /> when the coordinate is partially unknown.</description></item>
/// </list>
/// </remarks>
/// <param name="Latitudine">Latitude in decimal degrees; positive values indicate north.</param>
/// <param name="Longitudine">Longitude in decimal degrees; positive values indicate east.</param>
public record class Coordinate(double? Latitudine, double? Longitudine);