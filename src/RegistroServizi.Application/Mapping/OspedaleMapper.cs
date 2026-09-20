namespace RegistroServizi.Application.Mapping;

/// <summary>
/// Provides methods to map between Ospedale domain entities and their DTO representations.
/// </summary>
/// <remarks>Stateless mapping helpers for converting an Ospedale to OspedaleDto and for creating or updating
/// Ospedale entities from CreateOspedaleDto and UpdateOspedaleDto. Create mapping generates a new Guid for Id; address
/// postal code (Cap) defaults to 0 when null; Coordinate is initialized to (0, 0).</remarks>
public static class OspedaleMapper
{
    /// <summary>
    /// Maps an Ospedale to an OspedaleDto, including its Indirizzo mapped to an IndirizzoDto.
    /// </summary>
    /// <remarks>Does not validate input; will throw a NullReferenceException if ospedale or
    /// ospedale.Indirizzo is null.</remarks>
    /// <param name="ospedale">Ospedale to map to a DTO.</param>
    /// <returns>An OspedaleDto with Id, NomeOspedale and Indirizzo mapped; Indirizzo.Cap defaults to 0 when null.</returns>
    public static OspedaleDto MapOspedaleToDto(Ospedale ospedale) => new OspedaleDto
    {
        Id = ospedale.Id,
        NomeOspedale = ospedale.NomeOspedale,
        Indirizzo = new IndirizzoDto
        {
            Strada = ospedale.Indirizzo.Strada,
            Citta = ospedale.Indirizzo.Citta,
            Provincia = ospedale.Indirizzo.Provincia,
            Cap = ospedale.Indirizzo.Cap ?? 0
        }
    };

    /// <summary>
    /// Creates an Ospedale entity from a CreateOspedaleDto, generating a new Id, mapping the name and address, and
    /// initializing coordinates to (0,0).
    /// </summary>
    /// <remarks>Indirizzo.Cap defaults to 0 when null; Coordinate is initialized to (0,0).</remarks>
    /// <param name="dtoCreate">CreateOspedaleDto containing hospital name and address information used to populate the entity.</param>
    /// <returns>A new Ospedale instance with a generated Id, mapped NomeOspedale and Indirizzo, and default Coordinate values.</returns>
    public static Ospedale MapOspedaleToEntityCreate(CreateOspedaleDto dtoCreate) => new Ospedale
    {
        Id = Guid.NewGuid(),
        NomeOspedale = dtoCreate.NomeOspedale,
        Indirizzo = new Indirizzo(dtoCreate.Indirizzo.Strada, dtoCreate.Indirizzo.Citta, dtoCreate.Indirizzo.Provincia, dtoCreate.Indirizzo.Cap ?? 0),
        Coordinate = new Coordinate(0, 0)
    };

    /// <summary>
    /// Creates a new Ospedale entity from an UpdateOspedaleDto, copying Id and NomeOspedale and constructing Indirizzo
    /// from the DTO.
    /// </summary>
    /// <remarks>Indirizzo.Cap is coalesced to 0 if null; Coordinate is initialized to (0, 0).</remarks>
    /// <param name="dtoUpdate">DTO containing Id, NomeOspedale and Indirizzo; Indirizzo.Cap may be null and is defaulted to 0 when constructing
    /// the entity.</param>
    /// <returns>A new Ospedale instance populated from dtoUpdate for update operations.</returns>
    public static Ospedale MapOspedaleToEntityUpdate(UpdateOspedaleDto dtoUpdate) => new Ospedale
    {
        Id = dtoUpdate.Id,
        NomeOspedale = dtoUpdate.NomeOspedale,
        Indirizzo = new Indirizzo(dtoUpdate.Indirizzo.Strada, dtoUpdate.Indirizzo.Citta, dtoUpdate.Indirizzo.Provincia, dtoUpdate.Indirizzo.Cap ?? 0),
        Coordinate = new Coordinate(0, 0)
    };
}