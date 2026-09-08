namespace RegistroServizi.Application.Interfaces.Persistence;

public interface IRegistroServiziDbContext
{
    DbSet<PrezzoServizio> PrezziServizi { get; }
    DbSet<Applicazione> Applicazioni { get; }
    DbSet<Personalizzazione> Personalizzazioni { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}