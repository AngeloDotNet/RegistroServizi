using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RegistroServizi.Domain.Enums;

namespace RegistroServizi.Data;

public static class DatabaseInitializer
{
    public static async Task MigrateAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<RegistroServiziDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<RegistroServiziDbContext>>();

        try
        {
            var pendingMigrations = (await dbContext.Database.GetPendingMigrationsAsync()).ToList();

            if (pendingMigrations.Count > 0)
            {
                var pendingCounter = pendingMigrations.Count;
                logger.LogInformation("Applying {PendingCounter} pending migrations...", pendingCounter);
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("Database migrations applied successfully.");
            }
            else
            {
                logger.LogInformation("No pending migrations found.");
            }

            //var pending = await db.Database.GetPendingMigrationsAsync();

            //if (pending.Any())
            //{
            //    logger.LogInformation("Applying {Count} pending migration(s)…", pending.Count());
            //    await db.Database.MigrateAsync();
            //    logger.LogInformation("Database migrations applied successfully.");
            //}
            //else
            //{
            //    logger.LogDebug("Database schema is up to date — no migrations needed.");
            //}

            await SeedRolesAsync(scope.ServiceProvider, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying database migrations.");
            throw;
        }
    }

    private static readonly string[] defaultRoles = { nameof(Role.Admin), nameof(Role.Manager), nameof(Role.Operator) }; // Manager: gestione volontari / soci, Operator: gestione servizi

    private static async Task SeedRolesAsync(IServiceProvider services, ILogger logger)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var roleName in defaultRoles)
        {
            if (await roleManager.RoleExistsAsync(roleName))
            {
                continue;
            }

            var result = await roleManager.CreateAsync(new IdentityRole(roleName));

            if (result.Succeeded)
            {
                logger.LogInformation("Seeded role '{RoleName}'.", roleName);
                continue;
            }

            var errors = string.Join("; ", result.Errors.Select(error => error.Description));
            throw new InvalidOperationException($"Unable to seed role '{roleName}': {errors}");
        }
    }
}