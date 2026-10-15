namespace RegistroServizi.Application.Services.Common;

/// <summary>
/// Provides reusable query extensions for loading related <see cref="PrezzoServizio"/> data.
/// </summary>
/// <remarks>
/// The extensions configure Entity Framework Core queries without executing them, allowing callers
/// to continue composing filters, ordering, and projections before the query is evaluated.
/// </remarks>
internal static class QueryableExtensions
{
    /// <summary>
    /// Configures the query to eagerly load the
    /// <see cref="PrezzoServizio.TipologiaServizio"/> navigation property.
    /// </summary>
    /// <param name="query">
    /// The source query for <see cref="PrezzoServizio"/> entities.
    /// </param>
    /// <returns>
    /// The original query with <see cref="PrezzoServizio.TipologiaServizio"/> configured for eager loading.
    /// </returns>
    /// <remarks>
    /// The related <see cref="TipologiaServizio"/> entities are loaded when the query is executed.
    /// This extension is equivalent to applying Entity Framework Core's
    /// <see href="https://learn.microsoft.com/ef/core/querying/related-data/eager">eager-loading</see>
    /// <c>Include</c> operation while keeping query composition fluent.
    /// </remarks>
    internal static IQueryable<PrezzoServizio> IncludeTipoServizioDetails(this IQueryable<PrezzoServizio> query)
        => query.Include(x => x.TipologiaServizio);
}