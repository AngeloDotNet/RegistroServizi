namespace RegistroServizi.Application.Mapping;

/// <summary>
/// Provides static mapping methods to convert between Colonnina entities and their DTO representations.
/// </summary>
/// <remarks>Contains mappings to produce a ColonninaDto from a Colonnina and to create or update Colonnina
/// entities from DTOs. The create mapping generates a new Guid for Id and initializes Coordinate to (0, 0); the update
/// mapping uses the supplied Id and also initializes Coordinate to (0, 0). Stateless utility class.</remarks>
public static class ColonninaMapper
{
    /// <summary>
    /// Maps a Colonnina instance to a ColonninaDto.
    /// </summary>
    /// <remarks>Performs a shallow mapping of the specified properties; nested objects are not cloned and no
    /// validation is performed.</remarks>
    /// <param name="colonnina">The source Colonnina to map; must not be null.</param>
    /// <returns>A ColonninaDto with Id, NomeColonnina, Comune, and Provincia copied from the source.</returns>
    public static ColonninaDto MapColonninaToDto(Colonnina colonnina) => new ColonninaDto
    {
        Id = colonnina.Id,
        NomeColonnina = colonnina.NomeColonnina,
        Comune = colonnina.Comune,
        Provincia = colonnina.Provincia
    };

    /// <summary>
    /// Create a Colonnina entity from the provided CreateColonninaDto.
    /// </summary>
    /// <remarks>Generates a new Id (Guid.NewGuid()) and sets Coordinate to (0, 0); no validation or
    /// normalization is performed.</remarks>
    /// <param name="dtoCreate">CreateColonninaDto containing NomeColonnina, Comune, and Provincia used to populate the new entity.</param>
    /// <returns>A new Colonnina instance with a generated Id and Coordinate initialized to (0, 0).</returns>
    public static Colonnina MapColonninaToEntityCreate(CreateColonninaDto dtoCreate) => new Colonnina
    {
        Id = Guid.NewGuid(),
        NomeColonnina = dtoCreate.NomeColonnina,
        Comune = dtoCreate.Comune,
        Provincia = dtoCreate.Provincia,
        Coordinate = new Coordinate(0, 0)
    };

    /// <summary>
    /// Update a Colonnina entity from the provided UpdateColonninaDto.
    /// </summary>
    /// <remarks>Uses the Id from the DTO and sets Coordinate to (0, 0); no validation or normalization is performed.</remarks>
    /// <param name="dtoUpdate">UpdateColonninaDto containing Id, NomeColonnina, Comune, and Provincia used to update the entity.</param>
    /// <returns>A Colonnina instance with the specified Id and Coordinate initialized to (0, 0).</returns>
    public static Colonnina MapColonninaToEntityUpdate(UpdateColonninaDto dtoUpdate) => new Colonnina()
    {
        Id = dtoUpdate.Id,
        NomeColonnina = dtoUpdate.NomeColonnina,
        Comune = dtoUpdate.Comune,
        Provincia = dtoUpdate.Provincia,
        Coordinate = new Coordinate(0, 0)
    };
}
