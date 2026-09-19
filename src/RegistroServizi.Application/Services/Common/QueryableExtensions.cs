namespace RegistroServizi.Application.Services.Common;

/// <summary>
/// Includes the TipologiaServizio navigation property to eager-load related entities.
/// </summary>
/// <remarks>Uses Entity Framework Core's Include for eager loading and supports fluent query
/// composition.</remarks>
internal static class QueryableExtensions
{
    /// <summary>
    /// Includes the TipologiaServizio navigation property to eager-load related entities.
    /// </summary>
    /// <remarks>Uses Entity Framework Core's Include for eager loading and supports fluent query
    /// composition.</remarks>
    /// <param name="query">The queryable sequence of PrezzoServizio to include related TipologiaServizio entities.</param>
    /// <returns>An IQueryable<PrezzoServizio> that includes the TipologiaServizio navigation property.</returns>
    internal static IQueryable<PrezzoServizio> IncludeTipoServizioDetails(this IQueryable<PrezzoServizio> query)
        => query.Include(x => x.TipologiaServizio);
}