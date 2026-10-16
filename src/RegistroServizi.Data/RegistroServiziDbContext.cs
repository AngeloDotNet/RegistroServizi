namespace RegistroServizi.Data;

/// <summary>
/// Provides the Entity Framework Core database context for the Registro Servizi application.
/// </summary>
/// <remarks>
/// In addition to the application's domain entities, this context includes the ASP.NET Core
/// Identity model through <see cref="IdentityDbContext{TUser, TRole, TKey}"/>. Entity-specific
/// mapping configurations are discovered and applied automatically from the assembly containing
/// <see cref="RegistroServiziDbContext"/>.
/// </remarks>
/// <param name="options">
/// The options used to configure this context, normally supplied by dependency injection.
/// </param>
public class RegistroServiziDbContext(DbContextOptions<RegistroServiziDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options), IRegistroServiziDbContext
{
    /// <summary>
    /// Gets the queryable set of service type definitions.
    /// </summary>
    /// <remarks>
    /// Service types classify the services available in the application and are used by related
    /// service pricing records.
    /// </remarks>
    public virtual DbSet<TipologiaServizio> TipologieServizio => Set<TipologiaServizio>();

    /// <summary>
    /// Gets the queryable set of service price records.
    /// </summary>
    /// <remarks>
    /// Price records associate monetary values with the corresponding service types.
    /// </remarks>
    public virtual DbSet<PrezzoServizio> PrezziServizi => Set<PrezzoServizio>();

    /// <summary>
    /// Gets the queryable set of shipment status definitions.
    /// </summary>
    /// <remarks>
    /// These definitions provide the controlled set of statuses that can be assigned to shipments.
    /// </remarks>
    public virtual DbSet<StatoBolla> StatiBolla => Set<StatoBolla>();

    /// <summary>
    /// Gets the queryable set of education title records.
    /// </summary>
    /// <remarks>
    /// Education titles provide reusable reference data for users or other application entities
    /// that record educational qualifications.
    /// </remarks>
    public virtual DbSet<TitoloStudio> TitoliStudio => Set<TitoloStudio>();

    /// <summary>
    /// Gets the queryable set of hospital records.
    /// </summary>
    /// <remarks>
    /// Hospital records represent the healthcare facilities managed by the application.
    /// </remarks>
    public virtual DbSet<Ospedale> Ospedali => Set<Ospedale>();

    /// <summary>
    /// Gets the queryable set of charging station records.
    /// </summary>
    /// <remarks>
    /// Charging station records represent the charging infrastructure managed by the application.
    /// </remarks>
    public virtual DbSet<Colonnina> Colonnine => Set<Colonnina>();

    /// <inheritdoc />
    /// <remarks>
    /// The base implementation is called first so that ASP.NET Core Identity and Entity Framework
    /// Core conventions remain configured. Application-specific entity mappings are then loaded
    /// automatically from the current assembly through
    /// <see cref="ModelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly)"/>.
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