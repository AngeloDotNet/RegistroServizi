namespace RegistroServizi.Application.Mapping;

/// <summary>
/// Maps <see cref="Ospedale"/> domain entities to DTOs and creates <see cref="Ospedale"/> entities from DTOs.
/// </summary>
/// <remarks>
/// This mapper is stateless and contains only conversion helpers for:
/// <list type="bullet">
/// <item><description>Mapping <see cref="Ospedale"/> to <see cref="OspedaleDto"/>.</description></item>
/// <item><description>Creating <see cref="Ospedale"/> from <see cref="CreateOspedaleDto"/>.</description></item>
/// <item><description>Creating <see cref="Ospedale"/> from <see cref="UpdateOspedaleDto"/>.</description></item>
/// </list>
/// <para>
/// When mapping from DTOs, <c>Cap</c> defaults to <c>0</c> when null and <see cref="Coordinate"/> is initialized to
/// <c>(0, 0)</c>.
/// </para>
/// </remarks>
public static class OspedaleMapper
{
    /// <summary>
    /// Maps an <see cref="Ospedale"/> to an <see cref="OspedaleDto"/>.
    /// </summary>
    /// <param name="ospedale">The hospital entity to map.</param>
    /// <returns>A DTO containing the mapped hospital data, including the nested address.</returns>
    /// <remarks>
    /// <paramref name="ospedale"/> and <c>ospedale.Indirizzo</c> are expected to be non-null.
    /// <c>Cap</c> is set to <c>0</c> when the source value is null.
    /// </remarks>
    public static OspedaleDto MapOspedaleToDto(Ospedale ospedale) => new OspedaleDto
    {
        Id = ospedale.Id,
        NomeOspedale = ospedale.NomeOspedale,
        Ubicazione = new UbicazioneDto
        {
            Indirizzo = ospedale.Ubicazione.Indirizzo,
            Comune = ospedale.Ubicazione.Comune,
            Provincia = ospedale.Ubicazione.Provincia,
            Cap = ospedale.Ubicazione.Cap ?? 0
        }
    };

    /// <summary>
    /// Creates a new <see cref="Ospedale"/> from a <see cref="CreateOspedaleDto"/>.
    /// </summary>
    /// <param name="dtoCreate">The DTO containing the data used to create the entity.</param>
    /// <returns>
    /// A new <see cref="Ospedale"/> with a generated <see cref="Ospedale.Id"/>,
    /// mapped address values, and default coordinates.
    /// </returns>
    /// <remarks>
    /// <c>Cap</c> defaults to <c>0</c> when null.
    /// </remarks>
    public static Ospedale MapOspedaleToEntityCreate(CreateOspedaleDto dtoCreate) => new Ospedale
    {
        Id = Guid.NewGuid(),
        NomeOspedale = dtoCreate.NomeOspedale,
        Ubicazione = new Ubicazione(dtoCreate.Ubicazione.Indirizzo, dtoCreate.Ubicazione.Comune, dtoCreate.Ubicazione.Provincia, dtoCreate.Ubicazione.Cap),
        Coordinate = new Coordinate(0, 0)
    };

    /// <summary>
    /// Creates a new <see cref="Ospedale"/> from an <see cref="UpdateOspedaleDto"/>.
    /// </summary>
    /// <param name="dtoUpdate">The DTO containing the updated hospital data.</param>
    /// <returns>
    /// A new <see cref="Ospedale"/> populated with the DTO values, including the original identifier.
    /// </returns>
    /// <remarks>
    /// <c>Cap</c> defaults to <c>0</c> when null.
    /// </remarks>
    public static Ospedale MapOspedaleToEntityUpdate(UpdateOspedaleDto dtoUpdate) => new Ospedale
    {
        Id = dtoUpdate.Id,
        NomeOspedale = dtoUpdate.NomeOspedale,
        Ubicazione = new Ubicazione(dtoUpdate.Ubicazione.Indirizzo, dtoUpdate.Ubicazione.Comune, dtoUpdate.Ubicazione.Provincia, dtoUpdate.Ubicazione.Cap),
        Coordinate = new Coordinate(0, 0)
    };
}