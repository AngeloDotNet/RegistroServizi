namespace RegistroServizi.Application.Services.Common;

/// <summary>
/// Query extension methods for eager-loading related <see cref="PrezzoServizio"/> data.
/// </summary>
/// <remarks>
/// These extensions wrap Entity Framework Core <c>Include</c> calls to keep query composition fluent.
/// </remarks>
internal static class QueryableExtensions
{
    /// <summary>
    /// Adds the <see cref="PrezzoServizio.TipologiaServizio"/> navigation property to the query.
    /// </summary>
    /// <param name="query">
    /// The source query for <see cref="PrezzoServizio"/> entities.
    /// </param>
    /// <returns>
    /// A query that eagerly loads <see cref="PrezzoServizio.TipologiaServizio"/>.
    /// </returns>
    /// <remarks>
    /// Use this when the related <see cref="TipologiaServizio"/> data is required and should be loaded in the same query.
    /// </remarks>
    internal static IQueryable<PrezzoServizio> IncludeTipoServizioDetails(this IQueryable<PrezzoServizio> query)
        => query.Include(x => x.TipologiaServizio);
}