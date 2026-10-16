namespace RegistroServizi.Domain.Common;

/// <summary>
/// Represents a geographic coordinate expressed as latitude and longitude in decimal degrees.
/// </summary>
/// <remarks>
/// Coordinates are interpreted using the WGS84 geographic reference system.
/// A coordinate may be partially known; either component can be <see langword="null" />.
/// <para>
/// When specified, <paramref name="Latitudine" /> should be within the inclusive range
/// from -90 to 90, while <paramref name="Longitudine" /> should be within the inclusive
/// range from -180 to 180.
/// </para>
/// <para>
/// This type stores coordinate values but does not validate their ranges at construction time.
/// </para>
/// </remarks>
/// <param name="Latitudine">
/// The latitude in decimal degrees. Positive values indicate locations north of the equator.
/// </param>
/// <param name="Longitudine">
/// The longitude in decimal degrees. Positive values indicate locations east of the prime meridian.
/// </param>
public record class Coordinate(double? Latitudine, double? Longitudine);