namespace RegistroServizi.Application.Mapping;

/// <summary>
/// Provides static mapping methods to convert between PrezzoServizio entity and related DTO types.
/// </summary>
/// <remarks>Stateless helper methods that create new instances and do not perform validation. Includes
/// MapPrezzoServizioToDto to produce a PrezzoServizioDto and MapPrezzoServizioToEntityUpdate to produce a
/// PrezzoServizio for update operations.</remarks>
public static class PrezzoServizioMapper
{
    /// <summary>
    /// Map a PrezzoServizio entity to a PrezzoServizioDto.
    /// </summary>
    /// <remarks>Does not validate the entity; passing null will cause a NullReferenceException.
    /// TipologiaServizio?.TipoServizio is used with a fallback to an empty string.</remarks>
    /// <param name="entity">PrezzoServizio instance to map; TipologiaServizio may be null and its TipoServizio defaults to an empty string.</param>
    /// <returns>A PrezzoServizioDto populated from the entity's properties.</returns>
    public static PrezzoServizioDto MapPrezzoServizioToDto(PrezzoServizio entity) => new()
    {
        Id = entity.Id,
        TipologiaServizioId = entity.TipologiaServizioId,
        TipoServizio = entity.TipologiaServizio?.TipoServizio ?? string.Empty,
        CostoFisso = entity.CostoFisso,
        CostoKm = entity.CostoKm,
        SecondoTrasportato = entity.SecondoTrasportato,
        FermoMacchina = entity.FermoMacchina,
        Accompagnatore = entity.Accompagnatore,
        ScontoSocio = entity.ScontoSocio
    };

    /// <summary>
    /// Creates a PrezzoServizio entity from an UpdatePrezzoServizioDto for update operations.
    /// </summary>
    /// <remarks>Does not perform validation; dtoUpdate must not be null.</remarks>
    /// <param name="dtoUpdate">UpdatePrezzoServizioDto containing the identifier and pricing fields to apply to the entity.</param>
    /// <returns>PrezzoServizio populated with values from dtoUpdate.</returns>
    public static PrezzoServizio MapPrezzoServizioToEntityUpdate(UpdatePrezzoServizioDto dtoUpdate) => new()
    {
        Id = dtoUpdate.Id,
        TipologiaServizioId = dtoUpdate.TipologiaServizioId,
        CostoFisso = dtoUpdate.CostoFisso,
        CostoKm = dtoUpdate.CostoKm,
        SecondoTrasportato = dtoUpdate.SecondoTrasportato,
        FermoMacchina = dtoUpdate.FermoMacchina,
        Accompagnatore = dtoUpdate.Accompagnatore,
        ScontoSocio = dtoUpdate.ScontoSocio
    };
}