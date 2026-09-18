namespace RegistroServizi.Application.DTOs.Ospedale;

public class OspedaleDto
{
    public Guid Id { get; set; }
    public string NomeOspedale { get; set; } = string.Empty;
    public IndirizzoDto Indirizzo { get; set; } = new IndirizzoDto();
    //public CoordinateDto Coordinate { get; set; } = new CoordinateDto();
}