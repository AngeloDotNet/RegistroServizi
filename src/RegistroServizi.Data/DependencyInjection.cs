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
    public static IServiceCollection AddRegistroServiziData(this IServiceCollection services, IConfiguration configuration, string sqlConnection = "sqlConnection")
    {
        var connectionString = configuration.GetConnectionString(sqlConnection)
            ?? throw new InvalidOperationException($"Connection string '{sqlConnection}' was not found.");

        services.AddDbContext<RegistroServiziDbContext>(options => options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.CommandTimeout(60);
            sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
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

        return services;
    }
}