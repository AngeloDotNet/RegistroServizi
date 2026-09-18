namespace RegistroServizi.Application.Services.Common;

/// <summary>
/// Contiene metodi di estensione per le query su IQueryable.
/// </summary>
internal static class QueryableExtensions
{
    /// <summary>
    /// Estende una query di PrezzoServizio per includere i dettagli della TipologiaServizio associata.
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    internal static IQueryable<PrezzoServizio> IncludeTipoServizioDetails(this IQueryable<PrezzoServizio> query)
        => query.Include(x => x.TipologiaServizio);
}