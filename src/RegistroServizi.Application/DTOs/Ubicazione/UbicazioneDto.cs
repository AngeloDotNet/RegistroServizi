namespace RegistroServizi.Application.DTOs.Ubicazione;

/// <summary>
/// Represents an address data transfer object.
/// </summary>
public class UbicazioneDto
{
    /// <summary>
    /// Gets or sets the street address.
    /// </summary>
    public string Indirizzo { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    public string Comune { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the province or region code.
    /// </summary>
    public string Provincia { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the postal code.
    /// </summary>
    public int Cap { get; set; } = 0;
}