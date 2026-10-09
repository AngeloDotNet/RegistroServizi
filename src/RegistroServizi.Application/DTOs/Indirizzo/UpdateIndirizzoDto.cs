namespace RegistroServizi.Application.DTOs.Indirizzo;

/// <summary>
/// Represents the data required to update an existing address.
/// </summary>
/// <param name="Strada">The street name.</param>
/// <param name="Citta">The city name.</param>
/// <param name="Provincia">The province abbreviation or name.</param>
/// <param name="Cap">The postal code.</param>
//public record class UpdateIndirizzoDto(string Strada, string Citta, string Provincia, int? Cap);
public record class UpdateIndirizzoDto(string Strada, string Citta, string Provincia, int Cap);

//TODO: Rename Indirizzo To Ubicazione
//TODO: Rename Strada To Indirizzo
//TODO: Rename Citta To Comune