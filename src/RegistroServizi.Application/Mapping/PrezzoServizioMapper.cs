namespace RegistroServizi.Application.Mapping;

/// <summary>
/// Provides mapping helpers for <see cref="PrezzoServizio"/> and its DTOs.
/// </summary>
/// <remarks>
/// This class contains stateless helper methods that create new instances and do not perform validation.
/// </remarks>
public static class PrezzoServizioMapper
{
    /// <summary>
    /// Maps a <see cref="PrezzoServizio"/> entity to a <see cref="PrezzoServizioDto"/>.
    /// </summary>
    /// <remarks>
    /// The entity is not validated. Passing <see langword="null"/> results in a <see cref="NullReferenceException"/>.
    /// If <see cref="PrezzoServizio.TipologiaServizio"/> is <see langword="null"/>, <see cref="PrezzoServizioDto.TipoServizio"/>
    /// is set to <see cref="string.Empty"/>.
    /// </remarks>
    /// <param name="entity">The source entity to map.</param>
    /// <returns>A new <see cref="PrezzoServizioDto"/> populated from the entity.</returns>
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
    /// Maps an <see cref="UpdatePrezzoServizioDto"/> to a <see cref="PrezzoServizio"/> entity for update operations.
    /// </summary>
    /// <remarks>
    /// The DTO is not validated. Passing <see langword="null"/> results in a <see cref="NullReferenceException"/>.
    /// </remarks>
    /// <param name="dtoUpdate">The DTO containing the identifier and pricing values to apply.</param>
    /// <returns>A new <see cref="PrezzoServizio"/> populated from the DTO.</returns>
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