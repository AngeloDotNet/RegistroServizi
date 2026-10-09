namespace RegistroServizi.Application.DTOs.Colonnina;

/// <summary>
/// Represents the data required to update an existing charging station.
/// </summary>
/// <param name="Id">The unique identifier of the charging station to update.</param>
/// <param name="NomeColonnina">The updated name of the charging station.</param>
/// <param name="Comune">The updated municipality where the charging station is located.</param>
/// <param name="Provincia">The updated province where the charging station is located.</param>
public record class UpdateColonninaDto(Guid Id, string NomeColonnina, string Comune, string Provincia);