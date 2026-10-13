namespace RegistroServizi.Data;

/// <summary>
/// Registers the data access services for the RegistroServizi application.
/// </summary>
/// <remarks>
/// Adds the Entity Framework Core <see cref="RegistroServiziDbContext"/>,
/// a scoped <see cref="IDbContextFactory{TContext}"/>, and the
/// <see cref="IRegistroServiziDbContext"/> abstraction to the service container.
/// Also configures SQL Server persistence with retry handling, split-query behavior,
/// compatibility level settings, logging, and no-tracking queries.
/// </remarks>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds and configures the RegistroServizi data layer services.
        /// </summary>
        /// <param name="configuration">
        /// The application configuration used to resolve the database connection string.
        /// </param>
        /// <param name="sqlConnection">
        /// The name of the connection string to read from configuration.
        /// The default value is <c>SqlServerConnection</c>.
        /// </param>
        /// <returns>
        /// The same <see cref="IServiceCollection"/> instance so additional registrations can be chained.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the specified connection string cannot be found in configuration.
        /// </exception>
        /// <remarks>
        /// This method:
        /// <list type="bullet">
        /// <item><description>Registers <see cref="RegistroServiziDbContext"/> as a scoped service.</description></item>
        /// <item><description>Registers a scoped <see cref="IDbContextFactory{TContext}"/> for the same context.</description></item>
        /// <item><description>Maps <see cref="IRegistroServiziDbContext"/> to the concrete context implementation.</description></item>
        /// <item><description>Configures SQL Server retry behavior, migrations metadata, query splitting, and compatibility level.</description></item>
        /// <item><description>Enables logging and configures queries to use no-tracking by default.</description></item>
        /// </list>
        /// </remarks>
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