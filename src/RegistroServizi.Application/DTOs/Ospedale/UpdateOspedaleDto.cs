namespace RegistroServizi.Application.DTOs.Ospedale;

/// <summary>
/// Represents the data required to update an existing hospital.
/// </summary>
/// <param name="Id">The unique identifier of the hospital to update.</param>
/// <param name="NomeOspedale">The updated hospital name.</param>
/// <param name="Ubicazione">The updated address information.</param>
public record class UpdateOspedaleDto(Guid Id, string NomeOspedale, UpdateUbicazioneDto Ubicazione);