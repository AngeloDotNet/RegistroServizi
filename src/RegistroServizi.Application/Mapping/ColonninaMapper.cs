namespace RegistroServizi.Application.Mapping;

/// <summary>
/// Provides stateless mapping helpers for converting between <see cref="Colonnina"/> entities and their DTOs.
/// </summary>
/// <remarks>
/// This mapper supports:
/// <list type="bullet">
/// <item><description>Mapping a <see cref="Colonnina"/> to <see cref="ColonninaDto"/>.</description></item>
/// <item><description>Creating a new <see cref="Colonnina"/> from <see cref="CreateColonninaDto"/>.</description></item>
/// <item><description>Updating a <see cref="Colonnina"/> from <see cref="UpdateColonninaDto"/>.</description></item>
/// </list>
/// <para>
/// The create mapping generates a new <see cref="Guid"/> for <see cref="Colonnina.Id"/>.
/// Both create and update mappings initialize <see cref="Colonnina.Coordinate"/> to <c>(0, 0)</c>.
/// </para>
/// </remarks>
public static class ColonninaMapper
{
    /// <summary>
    /// Maps a <see cref="Colonnina"/> entity to a <see cref="ColonninaDto"/>.
    /// </summary>
    /// <remarks>
    /// This method performs a direct property-to-property copy for the supported fields.
    /// Nested objects are not cloned and no validation or normalization is performed.
    /// </remarks>
    /// <param name="colonnina">The source entity to map.</param>
    /// <returns>
    /// A new <see cref="ColonninaDto"/> containing the <see cref="Colonnina.Id"/>,
    /// <see cref="Colonnina.NomeColonnina"/>, <see cref="Colonnina.Comune"/>, and
    /// <see cref="Colonnina.Provincia"/> values from the source entity.
    /// </returns>
    public static ColonninaDto MapColonninaToDto(Colonnina colonnina) => new ColonninaDto
    {
        Id = colonnina.Id,
        NomeColonnina = colonnina.NomeColonnina,
        Comune = colonnina.Comune,
        Provincia = colonnina.Provincia
    };

    /// <summary>
    /// Creates a new <see cref="Colonnina"/> entity from a <see cref="CreateColonninaDto"/>.
    /// </summary>
    /// <remarks>
    /// A new <see cref="Guid"/> is generated for <see cref="Colonnina.Id"/>.
    /// The <see cref="Colonnina.Coordinate"/> property is initialized to <c>(0, 0)</c>.
    /// No validation or normalization is performed.
    /// </remarks>
    /// <param name="dtoCreate">The DTO containing the values used to populate the new entity.</param>
    /// <returns>A new <see cref="Colonnina"/> instance built from the provided DTO.</returns>
    public static Colonnina MapColonninaToEntityCreate(CreateColonninaDto dtoCreate) => new Colonnina
    {
        Id = Guid.NewGuid(),
        NomeColonnina = dtoCreate.NomeColonnina,
        Comune = dtoCreate.Comune,
        Provincia = dtoCreate.Provincia,
        Coordinate = new Coordinate(0, 0)
    };

    /// <summary>
    /// Updates a <see cref="Colonnina"/> entity from an <see cref="UpdateColonninaDto"/>.
    /// </summary>
    /// <remarks>
    /// The entity identifier is taken from the DTO. The <see cref="Colonnina.Coordinate"/> property
    /// is initialized to <c>(0, 0)</c>. No validation or normalization is performed.
    /// </remarks>
    /// <param name="dtoUpdate">The DTO containing the updated values.</param>
    /// <returns>A <see cref="Colonnina"/> instance populated from the provided DTO.</returns>
    public static Colonnina MapColonninaToEntityUpdate(UpdateColonninaDto dtoUpdate) => new Colonnina()
    {
        Id = dtoUpdate.Id,
        NomeColonnina = dtoUpdate.NomeColonnina,
        Comune = dtoUpdate.Comune,
        Provincia = dtoUpdate.Provincia,
        Coordinate = new Coordinate(0, 0)
    };
}