namespace RegistroServizi.Domain.Common;

/// <summary>
/// Represents a postal address consisting of street, city, province, and an optional postal code.
/// </summary>
/// <remarks>Immutable record type that provides value-based equality and deconstruction.</remarks>
/// <param name="Strada">Street address (street name and number).</param>
/// <param name="Citta">City.</param>
/// <param name="Provincia">Province or region.</param>
/// <param name="Cap">Postal code, or null if unknown.</param>
public record class Indirizzo(string Strada, string Citta, string Provincia, int? Cap);