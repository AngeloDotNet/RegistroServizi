namespace RegistroServizi.Application.DTOs.Ospedale;

/// <summary>
/// Represents the data required to create a new hospital.
/// </summary>
/// <param name="NomeOspedale">The name of the hospital.</param>
/// <param name="Indirizzo">The address of the hospital.</param>
public record class CreateOspedaleDto(string NomeOspedale, CreateIndirizzoDto Indirizzo);

//TODO: Rename Indirizzo To Ubicazione