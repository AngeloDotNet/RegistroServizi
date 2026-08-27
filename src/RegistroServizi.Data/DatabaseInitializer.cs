using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RegistroServizi.Data;

public static class DatabaseInitializer
{
    //public static async Task MigrateAsync(IServiceProvider services, bool seedDevelopmentData = false)
    public static async Task MigrateAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<RegistroServiziDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<RegistroServiziDbContext>>();

        try
        {
            var pending = await db.Database.GetPendingMigrationsAsync();

            if (pending.Any())
            {
                logger.LogInformation("Applying {Count} pending migration(s)…", pending.Count());
                await db.Database.MigrateAsync();
                logger.LogInformation("Database migrations applied successfully.");
            }
            else
            {
                logger.LogDebug("Database schema is up to date — no migrations needed.");
            }

            //if (seedDevelopmentData)
            //{
            //    await DevelopmentDataSeeder.SeedAsync(scope.ServiceProvider);
            //}
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying database migrations.");
            throw;
        }
    }
}