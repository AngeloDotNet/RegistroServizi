namespace RegistroServizi.Application.DTOs.Ospedale;

/// <summary>
/// Represents an hospital data transfer object.
/// </summary>
public class OspedaleDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the hospital.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the hospital name.
    /// </summary>
    public string NomeOspedale { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address associated with the hospital.
    /// </summary>
    public UbicazioneDto Ubicazione { get; set; } = new UbicazioneDto();

    //public CoordinateDto Coordinate { get; set; } = new CoordinateDto();
}