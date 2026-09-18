namespace RegistroServizi.Application.DTOs.Ospedale;

public record class CreateOspedaleDto(string NomeOspedale, CreateIndirizzoDto Indirizzo);