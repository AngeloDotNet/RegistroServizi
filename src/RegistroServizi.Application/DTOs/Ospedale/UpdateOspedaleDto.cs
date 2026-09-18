namespace RegistroServizi.Application.DTOs.Ospedale;

public record class UpdateOspedaleDto(Guid Id, string NomeOspedale, UpdateIndirizzoDto Indirizzo);