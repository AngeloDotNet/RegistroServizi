using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RegistroServizi.Application.Interfaces.Persistence;

/// <summary>
/// Defines the Entity Framework Core data-access contract for the RegistroServizi domain.
/// </summary>
/// <remarks>
/// Implementations expose the domain <see cref="DbSet{TEntity}"/> collections and the EF Core operations
/// required for persistence and change tracking, enabling dependency injection, unit testing, and mocking.
/// </remarks>
public interface IRegistroServiziDbContext
{
    /// <summary>Gets the set of <see cref="TipologiaServizio"/> entities.</summary>
    DbSet<TipologiaServizio> TipologieServizio { get; }

    /// <summary>Gets the set of <see cref="PrezzoServizio"/> entities.</summary>
    DbSet<PrezzoServizio> PrezziServizi { get; }

    /// <summary>Gets the set of <see cref="StatoBolla"/> entities.</summary>
    DbSet<StatoBolla> StatiBolla { get; }

    /// <summary>Gets the set of <see cref="TitoloStudio"/> entities.</summary>
    DbSet<TitoloStudio> TitoliStudio { get; }

    /// <summary>Gets the set of <see cref="Ospedale"/> entities.</summary>
    DbSet<Ospedale> Ospedali { get; }

    /// <summary>Gets the set of <see cref="Colonnina"/> entities.</summary>
    DbSet<Colonnina> Colonnine { get; }

    /// <summary>
    /// Saves all pending changes to the underlying data store asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task whose result is the number of state entries written to the underlying store.
    /// </returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an <see cref="EntityEntry{TEntity}"/> for the specified entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="entity">The entity to retrieve the entry for.</param>
    /// <returns>An entry that provides access to change-tracking information and operations for the entity.</returns>
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}