namespace RegistroServizi.Application.DTOs.Colonnina;

/// <summary>
/// Represents the data required to create a charging station.
/// </summary>
/// <param name="NomeColonnina">The name of the charging station.</param>
/// <param name="Comune">The municipality where the charging station is located.</param>
/// <param name="Provincia">The province where the charging station is located.</param>
public record class CreateColonninaDto(string NomeColonnina, string Comune, string Provincia);