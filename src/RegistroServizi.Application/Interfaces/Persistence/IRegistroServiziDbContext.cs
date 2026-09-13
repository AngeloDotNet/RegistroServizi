namespace RegistroServizi.Application.Interfaces.Persistence;

/// <summary>
/// Represents the database context for the RegistroServizi application.
/// This interface defines the contract for interacting with the underlying database, including access to the various DbSet properties representing the entities in the application.
/// </summary>
public interface IRegistroServiziDbContext
{
    DbSet<TipologiaServizio> TipologieServizio { get; }
    DbSet<PrezzoServizio> PrezziServizi { get; }
    DbSet<Applicazione> Applicazioni { get; }
    DbSet<Personalizzazione> Personalizzazioni { get; }
    DbSet<StatoBolla> StatiBolla { get; }

    /// <summary>
    /// Saves all changes made in this context to the database asynchronously.
    /// This method will automatically detect changes made to the tracked entities and generate the appropriate SQL commands to persist those changes to the database.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}