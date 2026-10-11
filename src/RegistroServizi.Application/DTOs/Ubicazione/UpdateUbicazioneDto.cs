namespace RegistroServizi.Application.DTOs.Ubicazione;

/// <summary>
/// Represents the data required to update an existing address.
/// </summary>
/// <param name="Indirizzo">The street name.</param>
/// <param name="Comune">The city name.</param>
/// <param name="Provincia">The province abbreviation or name.</param>
/// <param name="Cap">The postal code.</param>
public record class UpdateUbicazioneDto(string Indirizzo, string Comune, string Provincia, int Cap);