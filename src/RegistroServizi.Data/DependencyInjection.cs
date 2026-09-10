namespace RegistroServizi.Data;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Add RegistroServizi.Data services to the specified IServiceCollection.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="sqlConnection">The name of the SQL connection string.</param>
        /// <returns>The updated IServiceCollection.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the connection string is not found.</exception>
        public IServiceCollection AddRegistroServiziData(IConfiguration configuration, string sqlConnection = "SqlServerConnection")
        {
            var connectionString = configuration.GetConnectionString(sqlConnection) ?? throw new InvalidOperationException($"Connection string '{sqlConnection}' was not found.");

            services.AddDbContext<RegistroServiziDbContext>(options => options.UseSqlServer(connectionString, sqlOptions =>
            {
                //sqlOptions.CommandTimeout(60);
                //sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                sqlOptions.EnableRetryOnFailure();

                sqlOptions.MigrationsAssembly(typeof(RegistroServiziDbContext).Assembly.FullName);
                sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName);

                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                sqlOptions.UseCompatibilityLevel(160);
            })
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors(false)
            .EnableSensitiveDataLogging(false)
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddDbContextFactory<RegistroServiziDbContext>(options => options.UseSqlServer(connectionString, sqlOptions =>
            {
                //sqlOptions.CommandTimeout(60);
                //sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                sqlOptions.EnableRetryOnFailure();

                sqlOptions.MigrationsAssembly(typeof(RegistroServiziDbContext).Assembly.FullName);
                sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName);

                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                sqlOptions.UseCompatibilityLevel(160);
            })
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors(false)
            .EnableSensitiveDataLogging(false)
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Scoped);

            services.AddScoped<IRegistroServiziDbContext>(provider => provider.GetRequiredService<IDbContextFactory<RegistroServiziDbContext>>().CreateDbContext());

            return services;
        }
    }
}