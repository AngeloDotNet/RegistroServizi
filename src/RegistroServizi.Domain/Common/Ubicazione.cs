namespace RegistroServizi.Domain.Common;

/// <summary>
/// Represents a postal address as an immutable value object in the domain model.
/// </summary>
/// <remarks>
/// <para>
/// Use this type when address information is part of an entity's domain state and
/// should be compared by its component values rather than by object identity.
/// </para>
/// <para>
/// Two instances are equal when their street address, municipality, province, and
/// postal code are equal. The postal code is optional because it may be unavailable
/// or not applicable to the address.
/// </para>
/// </remarks>
/// <param name="Indirizzo">
/// The street name and civic number.
/// </param>
/// <param name="Comune">
/// The municipality or city.
/// </param>
/// <param name="Provincia">
/// The province, region, or equivalent territorial subdivision.
/// </param>
/// <param name="Cap">
/// The postal code, or <see langword="null"/> when the value is unavailable or not applicable.
/// </param>
public record class Ubicazione(string Indirizzo, string Comune, string Provincia, int? Cap);