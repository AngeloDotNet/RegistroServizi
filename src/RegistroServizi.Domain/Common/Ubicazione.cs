namespace RegistroServizi.Domain.Common;

/// <summary>
/// Represents a postal address value object in the domain model.
/// </summary>
/// <remarks>
/// This type is immutable and uses value-based equality across all properties.
/// Use it to model address data such as street, city, province, and an optional postal code.
/// </remarks>
/// <param name="Indirizzo">The street name and civic number.</param>
/// <param name="Comune">The municipality or city.</param>
/// <param name="Provincia">The province, region, or equivalent territorial subdivision.</param>
/// <param name="Cap">
/// The postal code, or <see langword="null"/> when the value is unavailable.
/// </param>
public record class Ubicazione(string Indirizzo, string Comune, string Provincia, int? Cap);