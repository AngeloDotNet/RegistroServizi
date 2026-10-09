namespace RegistroServizi.Application.DTOs.Indirizzo;

/// <summary>
/// Represents an address data transfer object.
/// </summary>
public class IndirizzoDto
{
    /// <summary>
    /// Gets or sets the street address.
    /// </summary>
    public string Strada { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    public string Citta { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the province or region code.
    /// </summary>
    public string Provincia { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the postal code.
    /// </summary>
    //public int? Cap { get; set; } = null;
    public int Cap { get; set; } = 0;
}

//TODO: Rename Indirizzo To Ubicazione
//TODO: Rename Strada To Indirizzo
//TODO: Rename Citta To Comune