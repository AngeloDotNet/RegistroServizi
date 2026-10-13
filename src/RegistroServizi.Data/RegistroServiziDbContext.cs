namespace RegistroServizi.Data;

/// <summary>
/// Represents the Entity Framework Core database context for the Registro Servizi application.
/// </summary>
/// <remarks>
/// This context integrates ASP.NET Core Identity through <see cref="IdentityDbContext{TUser,TRole,TKey}"/> and exposes
/// <see cref="DbSet{TEntity}"/> properties for the application's domain entities. Entity configurations are applied from
/// the assembly containing <see cref="RegistroServiziDbContext"/>.
/// </remarks>
/// <param name="options">
/// The <see cref="DbContextOptions{TContext}"/> used to configure the context, typically provided by dependency injection.
/// </param>
public class RegistroServiziDbContext(DbContextOptions<RegistroServiziDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options), IRegistroServiziDbContext
{
    /// <summary>Gets the set of service types.</summary>
    public virtual DbSet<TipologiaServizio> TipologieServizio => Set<TipologiaServizio>();

    /// <summary>Gets the set of service prices.</summary>
    public virtual DbSet<PrezzoServizio> PrezziServizi => Set<PrezzoServizio>();

    /// <summary>Gets the set of shipment statuses.</summary>
    public virtual DbSet<StatoBolla> StatiBolla => Set<StatoBolla>();

    /// <summary>Gets the set of education titles.</summary>
    public virtual DbSet<TitoloStudio> TitoliStudio => Set<TitoloStudio>();

    /// <summary>Gets the set of hospitals.</summary>
    public virtual DbSet<Ospedale> Ospedali => Set<Ospedale>();

    /// <summary>Gets the set of charging stations.</summary>
    public virtual DbSet<Colonnina> Colonnine => Set<Colonnina>();

    /// <summary>
    /// Configures the EF Core model for this context.
    /// </summary>
    /// <remarks>
    /// Applies all <see cref="IEntityTypeConfiguration{TEntity}"/> implementations found in the current assembly and
    /// calls the base implementation to preserve Identity and EF Core conventions.
    /// </remarks>
    /// <param name="modelBuilder">
    /// The builder used to configure entity types, relationships, keys, indexes, and other model metadata.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RegistroServiziDbContext).Assembly);
    }
}