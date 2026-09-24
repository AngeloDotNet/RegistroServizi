namespace RegistroServizi.Application.DTOs.Colonnina;

public class ColonninaDto
{
    public Guid Id { get; set; }
    public string NomeColonnina { get; set; } = string.Empty;
    public string Comune { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;

    //public CoordinateDto Coordinate { get; set; } = new CoordinateDto();
}