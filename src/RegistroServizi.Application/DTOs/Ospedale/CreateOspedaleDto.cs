namespace RegistroServizi.Application.DTOs.Ospedale;

/// <summary>
/// Represents the data required to create a new hospital.
/// </summary>
/// <param name="NomeOspedale">The name of the hospital.</param>
/// <param name="Ubicazione">The location of the hospital.</param>
public record class CreateOspedaleDto(string NomeOspedale, CreateUbicazioneDto Ubicazione);