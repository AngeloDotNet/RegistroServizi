using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RegistroServizi.Data;

/// <summary>
/// Provides extension methods for configuring and registering the RegistroServizi data services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds the RegistroServizi data services to the specified IServiceCollection, configuring the RegistroServiziDbContext with SQL Server and related options.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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

        //public IServiceCollection AddRegistroServiziData(IConfiguration configuration, string sqlConnection = "SqlServerConnection")
        //{
        //    var connectionString = configuration.GetConnectionString(sqlConnection) ?? throw new InvalidOperationException($"Connection string '{sqlConnection}' was not found.");

        //    services.AddDbContext<RegistroServiziDbContext>(options => options.UseSqlServer(connectionString, sqlOptions =>
        //    {
        //        //sqlOptions.CommandTimeout(60);
        //        //sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        //        sqlOptions.EnableRetryOnFailure();

        //        sqlOptions.MigrationsAssembly(typeof(RegistroServiziDbContext).Assembly.FullName);
        //        sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName);

        //        sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        //        sqlOptions.UseCompatibilityLevel(160);
        //    })
        //    .LogTo(Console.WriteLine, LogLevel.Information)
        //    .EnableDetailedErrors(false)
        //    .EnableSensitiveDataLogging(false)
        //    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
        //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        //    services.AddDbContextFactory<RegistroServiziDbContext>(options => options.UseSqlServer(connectionString, sqlOptions =>
        //    {
        //        //sqlOptions.CommandTimeout(60);
        //        //sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        //        sqlOptions.EnableRetryOnFailure();

        //        sqlOptions.MigrationsAssembly(typeof(RegistroServiziDbContext).Assembly.FullName);
        //        sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName);

        //        sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        //        sqlOptions.UseCompatibilityLevel(160);
        //    })
        //    .LogTo(Console.WriteLine, LogLevel.Information)
        //    .EnableDetailedErrors(false)
        //    .EnableSensitiveDataLogging(false)
        //    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
        //    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking), ServiceLifetime.Scoped);

        //    services.AddScoped<IRegistroServiziDbContext>(provider => provider.GetRequiredService<IDbContextFactory<RegistroServiziDbContext>>().CreateDbContext());

        //    return services;
        //}
    }
}