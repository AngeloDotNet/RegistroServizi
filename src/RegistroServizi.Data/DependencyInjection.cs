namespace RegistroServizi.Data;

/// <summary>
/// Provides dependency-injection registration for the RegistroServizi data-access layer.
/// </summary>
/// <remarks>
/// The registered services use Entity Framework Core with SQL Server and are configured
/// for resilient connections, split queries, no-tracking reads, and centralized migration
/// metadata. This keeps database configuration consistent across application components
/// while exposing <see cref="IRegistroServiziDbContext"/> to consumers that do not need
/// to depend on the concrete context type.
/// <para>
/// For additional information, see the
/// <a href="https://learn.microsoft.com/ef/core/dbcontext-configuration/">Entity Framework Core DbContext configuration documentation</a>
/// and the
/// <a href="https://learn.microsoft.com/aspnet/core/fundamentals/dependency-injection">ASP.NET Core dependency injection documentation</a>.
/// </para>
/// </remarks>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers and configures the RegistroServizi Entity Framework Core services.
        /// </summary>
        /// <param name="configuration">
        /// The application configuration containing the connection string.
        /// </param>
        /// <param name="sqlConnection">
        /// The configuration key of the SQL Server connection string.
        /// The default key is <c>SqlServerConnection</c>.
        /// </param>
        /// <returns>
        /// The current <see cref="IServiceCollection"/> instance, allowing additional
        /// service registrations to be chained.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <paramref name="sqlConnection"/> does not identify a connection
        /// string in <paramref name="configuration"/>.
        /// </exception>
        /// <remarks>
        /// This method registers the following services:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// <see cref="RegistroServiziDbContext"/> as a scoped Entity Framework Core context.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// A scoped <see cref="IDbContextFactory{TContext}"/> for creating
        /// <see cref="RegistroServiziDbContext"/> instances.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <see cref="IRegistroServiziDbContext"/> mapped to the registered
        /// <see cref="RegistroServiziDbContext"/> instance.
        /// </description>
        /// </item>
        /// </list>
        /// SQL Server is configured with transient-failure retries, split-query behavior,
        /// compatibility level 160, migration history metadata, informational logging,
        /// disabled sensitive-data logging, and no-tracking queries by default.
        /// </remarks>
        /// <example>
        /// The data layer can be registered during application startup:
        /// <code language="csharp">
        /// builder.Services.AddRegistroServiziData(builder.Configuration);
        /// </code>
        /// </example>
        public IServiceCollection AddRegistroServiziData(IConfiguration configuration, string sqlConnection = "SqlServerConnection")
        {
            var connectionString = configuration.GetConnectionString(sqlConnection) ?? throw new InvalidOperationException($"Connection string '{sqlConnection}' was not found.");

            Action<SqlServerDbContextOptionsBuilder> configureSqlServer = sqlOptions =>
            {
                //sqlOptions.CommandTimeout(60);
                //sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                sqlOptions.EnableRetryOnFailure();

                sqlOptions.MigrationsAssembly(typeof(RegistroServiziDbContext).Assembly.FullName);
                sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName);

                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                sqlOptions.UseCompatibilityLevel(160);
            };

            Action<DbContextOptionsBuilder> commonOptions = options => options.UseSqlServer(connectionString, configureSqlServer)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableDetailedErrors(false)
                .EnableSensitiveDataLogging(false)
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            services.AddDbContext<RegistroServiziDbContext>(commonOptions);

            services.AddDbContextFactory<RegistroServiziDbContext>(commonOptions, ServiceLifetime.Scoped);

            services.AddScoped<IRegistroServiziDbContext>(provider => provider.GetRequiredService<RegistroServiziDbContext>());

            return services;
        }
    }
}