namespace RegistroServizi.Data;

/// <summary>
/// Represents the database context for the RegistroServizi application, inheriting from IdentityDbContext to include identity management features.
/// </summary>
/// <param name="options"></param>
public class RegistroServiziDbContext(DbContextOptions<RegistroServiziDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options), IRegistroServiziDbContext
{
    public virtual DbSet<TipologiaServizio> TipologieServizio => Set<TipologiaServizio>();
    public virtual DbSet<PrezzoServizio> PrezziServizi => Set<PrezzoServizio>();
    public virtual DbSet<Applicazione> Applicazioni => Set<Applicazione>();
    public virtual DbSet<Personalizzazione> Personalizzazioni => Set<Personalizzazione>();

    /// <summary>
    /// Override the OnModelCreating method to apply entity configurations from the assembly containing the RegistroServiziDbContext class.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RegistroServiziDbContext).Assembly);
    }
}