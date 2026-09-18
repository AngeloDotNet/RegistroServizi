namespace RegistroServizi.Application.DTOs.Indirizzo;

public record class CreateIndirizzoDto(string Strada, string Citta, string Provincia, int? Cap);