namespace RegistroServizi.Domain.Common;

/// <summary>
/// Geographic coordinate with nullable latitude and longitude in decimal degrees.
/// </summary>
/// <remarks>Values are expressed in the WGS84 geographic coordinate system. Latitude range: -90 to 90; longitude
/// range: -180 to 180.</remarks>
/// <param name="Latitudine">Latitude in decimal degrees; positive north. Null if unknown.</param>
/// <param name="Longitudine">Longitude in decimal degrees; positive east. Null if unknown.</param>
public record class Coordinate(double? Latitudine, double? Longitudine);