namespace RegistroServizi.Application.DTOs.Indirizzo;

public class IndirizzoDto
{
    public string Strada { get; set; } = string.Empty;
    public string Citta { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;
    public int Cap { get; set; } = 0;
}