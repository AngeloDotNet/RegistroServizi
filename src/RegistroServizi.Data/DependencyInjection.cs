namespace RegistroServizi.Data;

/// <summary>
/// Provides extension methods to register and configure RegistroServizi data services and related persistence
/// components in an IServiceCollection.
/// </summary>
/// <remarks>Registers RegistroServiziDbContext, a DbContextFactory, and IRegistroServiziDbContext. Configures SQL
/// Server options (retry on failure, migrations assembly and history table, query splitting, compatibility level),
/// logging, error and sensitive-data settings, warning configuration, and no-tracking query behavior. Uses the
/// connection string name 'SqlServerConnection' by default and will throw InvalidOperationException if the connection
/// string is not found.</remarks>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds and configures RegistroServiziDbContext, its factory, and related services using a SQL Server
        /// connection string.
        /// </summary>
        /// <remarks>Registers RegistroServiziDbContext and a DbContextFactory with scoped lifetimes, maps
        /// IRegistroServiziDbContext to RegistroServiziDbContext, and configures SQL Server options including retry on
        /// failure, migrations assembly and history table, query splitting behavior, compatibility level, logging,
        /// detailed error and sensitive data logging settings, warning suppression for pending model changes, and
        /// NoTracking query behavior.</remarks>
        /// <param name="configuration">Application configuration used to retrieve the connection string.</param>
        /// <param name="sqlConnection">Name of the connection string in configuration. Defaults to 'SqlServerConnection'.</param>
        /// <returns>The IServiceCollection after registering the database context and related services.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the specified connection string is not found in the configuration.</exception>
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