namespace RegistroServizi.Application.Helpers;

/// <summary>
/// Helper class for mapping PrezzoServizio entities to DTOs.
/// </summary>
public static class PrezzoServizioHelper
{
    /// <summary>
    /// Maps a PrezzoServizio entity to a PrezzoServizioDto.
    /// </summary>
    /// <param name="prezzoServizio"></param>
    /// <returns></returns>
    public static PrezzoServizioDto MapPrezzoServizioToDto(PrezzoServizio prezzoServizio) => new PrezzoServizioDto
    {
        Id = prezzoServizio.Id,
        TipologiaServizio = prezzoServizio.TipologiaServizio,
        CostoFisso = prezzoServizio.CostoFisso,
        CostoKm = prezzoServizio.CostoKm,
        SecondoTrasportato = prezzoServizio.SecondoTrasportato,
        FermoMacchina = prezzoServizio.FermoMacchina,
        Accompagnatore = prezzoServizio.Accompagnatore,
        ScontoSocio = prezzoServizio.ScontoSocio
    };

    /// <summary>
    /// Maps a PrezzoServizio entity to a PrezzoServizioDetailDto.
    /// </summary>
    /// <param name="prezzoServizio"></param>
    /// <returns></returns>
    //public static DetailPrezzoServizioDto MapPrezzoServizioToDetailDto(PrezzoServizio prezzoServizio) => new DetailPrezzoServizioDto(prezzoServizio.Id,
    //    prezzoServizio.TipologiaServizio, prezzoServizio.CostoFisso, prezzoServizio.CostoKm, prezzoServizio.SecondoTrasportato,
    //    prezzoServizio.FermoMacchina, prezzoServizio.Accompagnatore, prezzoServizio.ScontoSocio);
}