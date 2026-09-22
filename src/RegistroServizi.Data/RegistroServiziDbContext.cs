namespace RegistroServizi.Data;

/// <summary>
/// Represents the Entity Framework Core DbContext for the Registro Servizi application, exposing DbSet properties for
/// domain entities and integrating ASP.NET Core Identity.
/// </summary>
/// <remarks>Applies IEntityTypeConfiguration implementations from the assembly containing
/// RegistroServiziDbContext and calls the base OnModelCreating to preserve Identity and EF Core conventions.</remarks>
/// <param name="options">The DbContextOptions<RegistroServiziDbContext> used to configure the context, typically provided by dependency
/// injection.</param>
public class RegistroServiziDbContext(DbContextOptions<RegistroServiziDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options), IRegistroServiziDbContext
{
    public virtual DbSet<TipologiaServizio> TipologieServizio => Set<TipologiaServizio>();
    public virtual DbSet<PrezzoServizio> PrezziServizi => Set<PrezzoServizio>();
    public virtual DbSet<StatoBolla> StatiBolla => Set<StatoBolla>();
    public virtual DbSet<TitoloStudio> TitoliStudio => Set<TitoloStudio>();
    public virtual DbSet<Ospedale> Ospedali => Set<Ospedale>();
    public virtual DbSet<Colonnina> Colonnine => Set<Colonnina>();

    /// <summary>
    /// Configures the EF Core model by applying entity configurations from the context assembly and invoking the base
    /// implementation.
    /// </summary>
    /// <remarks>Applies IEntityTypeConfiguration implementations from the assembly containing
    /// RegistroServiziDbContext. The call to the base implementation preserves default conventions and
    /// behaviors.</remarks>
    /// <param name="modelBuilder">The ModelBuilder used to configure entity types, relationships, and other model metadata for the context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RegistroServiziDbContext).Assembly);
    }
}