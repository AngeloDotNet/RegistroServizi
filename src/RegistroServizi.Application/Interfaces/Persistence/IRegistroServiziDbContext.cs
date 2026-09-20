using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RegistroServizi.Application.Interfaces.Persistence;

/// <summary>
/// Abstraction of an Entity Framework Core DbContext for the Registro servizi domain, exposing DbSet properties for
/// domain entities and EF Core operations for change tracking and persistence.
/// </summary>
/// <remarks>Implementations should provide DbSet<T> properties for TipologiaServizio, PrezzoServizio,
/// Applicazione, StatoBolla, TitoloStudio and Ospedale, and implement SaveChangesAsync and Entry to support dependency
/// injection, unit testing, and mocking of data access.</remarks>
public interface IRegistroServiziDbContext
{
    DbSet<TipologiaServizio> TipologieServizio { get; }
    DbSet<PrezzoServizio> PrezziServizi { get; }
    DbSet<Applicazione> Applicazioni { get; }
    DbSet<StatoBolla> StatiBolla { get; }
    DbSet<TitoloStudio> TitoliStudio { get; }
    DbSet<Ospedale> Ospedali { get; }
    DbSet<Colonnina> Colonnine { get; }

    /// <summary>
    /// Saves all changes made in the context to the underlying data store asynchronously.
    /// </summary>
    /// <remarks>Operations are executed in a single transaction when supported by the provider.</remarks>
    /// <param name="cancellationToken">Token to cancel the asynchronous save operation.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
    /// written to the underlying store.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an EntityEntry for the specified entity that provides access to change-tracking information and operations.
    /// </summary>
    /// <remarks>Throws ArgumentNullException if entity is null. If the entity is not tracked, the returned
    /// entry can be used to begin tracking and to set the entity state.</remarks>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="entity">The entity for which to obtain the EntityEntry.</param>
    /// <returns>An EntityEntry<TEntity> that provides access to change-tracking information and operations for the entity.</returns>
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}