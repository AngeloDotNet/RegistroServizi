using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RegistroServizi.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddRegistroServiziData(this IServiceCollection services, IConfiguration configuration, string connectionStringName = "DefaultConnection")
    {
        var connectionString = configuration.GetConnectionString(connectionStringName)
            ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' was not found.");

        //// EnableRetryOnFailure handles transient errors, including the brief
        //// connection failures that occur while the serverless database resumes
        //// from auto-pause.
        //services.AddDbContext<RegistroServiziDbContext>(options =>
        //    options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));

        services.AddDbContext<RegistroServiziDbContext>(options
            => options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.CommandTimeout(60); // Set the command timeout to 60 seconds
                sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); // Retry up to 5 times with a 10-second delay between retries
                sqlOptions.MigrationsAssembly(typeof(RegistroServiziDbContext).Assembly.FullName);
                sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName);
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                sqlOptions.UseCompatibilityLevel(160);
            })
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableDetailedErrors(false)
            .EnableSensitiveDataLogging(false)
            .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning))
            .UseExceptionProcessor()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        //// Register a scoped factory so individual DbContext instances can be
        //// created on demand, independent of the scoped context used by Identity
        //// stores.  A scoped lifetime avoids the lifetime conflict with the
        //// scoped DbContextOptions registered by AddDbContext above.
        //services.AddDbContextFactory<RegistroServiziDbContext>(options =>
        //    options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()), ServiceLifetime.Scoped);

        //// In Blazor Server, the DI scope lives for the entire circuit lifetime.
        //// Multiple components can concurrently await DB operations, which would
        //// cause EF Core's "second operation started" error if they all share one
        //// scoped DbContext.  Registering IApplicationDbContext as transient gives
        //// each injected service its own DbContext instance, eliminating that race.
        //services.AddTransient<IApplicationDbContext>(
        //    sp => sp.GetRequiredService<IDbContextFactory<RegistroServiziDbContext>>().CreateDbContext());

        return services;
    }
}