namespace RegistroServizi.Application.Mapping;

/// <summary>
/// Helper class for mapping PrezzoServizio entities to their corresponding DTOs.
/// </summary>
public static class PrezzoServizioMapper
{
    /// <summary>
    /// Maps a PrezzoServizio entity to a PrezzoServizioDto.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
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
    /// Maps an UpdatePrezzoServizioDto to a PrezzoServizio entity for updating purposes.
    /// </summary>
    /// <param name="dtoUpdate"></param>
    /// <returns></returns>
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