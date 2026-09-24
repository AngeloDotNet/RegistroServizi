namespace RegistroServizi.Application.DTOs.Colonnina;

public record class UpdateColonninaDto(Guid Id, string NomeColonnina, string Comune, string Provincia);