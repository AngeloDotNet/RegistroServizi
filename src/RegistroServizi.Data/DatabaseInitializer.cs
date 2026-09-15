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

            // Seed data for roles
            await SeedRolesAsync(scope.ServiceProvider, logger);

            // Seed data for users
            await SeedDefaultUsersAsync(scope.ServiceProvider, logger);

            // Seed data for Applicazione
            await SeedDataApplicazioneAsync(scope.ServiceProvider, logger);

            // Seed data for TipologiaServizio
            await SeedDataTipologiaServizioAsync(scope.ServiceProvider, logger);

            // Seed data for PrezzoServizio
            await SeedDataPrezziServiziAsync(scope.ServiceProvider, logger);

            // Seed data for StatiBolla
            await SeedDataStatiBollaAsync(scope.ServiceProvider, logger);

            // Seed data for TitoliStudio
            await SeedDataTitoliStudioAsync(scope.ServiceProvider, logger);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying database migrations.");
            throw;
        }
    }

    /// <summary>
    /// Default roles to be seeded into the database.
    /// </summary>
    //private static readonly string[] defaultRoles = { nameof(Role.Admin), nameof(Role.Manager), nameof(Role.Operator) };
    private static readonly string[] defaultRoles = { nameof(Role.Admin), nameof(Role.Manager) };

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

    private static async Task SeedDefaultUsersAsync(IServiceProvider services, ILogger logger)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var adminUserOptions = services.GetRequiredService<IOptions<AdminUserOptions>>().Value;

        var administrator = new ApplicationUser
        {
            UserName = adminUserOptions.UserName,
            Email = adminUserOptions.Email,
            EmailConfirmed = true,
            LockoutEnabled = true
        };

        var existingAdmin = await userManager.FindByEmailAsync(administrator.Email);

        if (existingAdmin == null)
        {
            var result = await userManager.CreateAsync(administrator, adminUserOptions.Password);

            if (result.Succeeded)
            {
                logger.LogInformation("Seeded default administrator user with email '{Email}'.", administrator.Email);

                var adminRoles = new List<string> { nameof(Role.Admin), nameof(Role.Manager) };

                await userManager.AddToRolesAsync(administrator, adminRoles);
                logger.LogInformation("Assigned roles '{Roles}' to administrator user.", string.Join(", ", adminRoles));
            }
        }
    }

    private static async Task SeedDataApplicazioneAsync(IServiceProvider services, ILogger logger)
    {
        var dbContext = services.GetRequiredService<RegistroServiziDbContext>();
        var existingDataApplicazione = await dbContext.Applicazioni.FirstOrDefaultAsync();

        if (existingDataApplicazione != null)
        {
            logger.LogInformation("Data applicazione already exists. Skipping seeding.");
            return;
        }

        var applicazione = new Applicazione
        {
            Id = Guid.NewGuid(),
            NomeApplicazione = "Registro Servizi",
            Versione = "1.0.0"
        };

        dbContext.Applicazioni.Add(applicazione);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedDataTipologiaServizioAsync(IServiceProvider services, ILogger logger)
    {
        var dbContext = services.GetRequiredService<RegistroServiziDbContext>();
        var existingTipologieServizio = await dbContext.TipologieServizio.FirstOrDefaultAsync();

        if (existingTipologieServizio != null)
        {
            logger.LogInformation("Tipologie Servizio data already exists. Skipping seeding.");
            return;
        }

        var tipologieServizio = new List<TipologiaServizio>
        {
            new TipologiaServizio { Id = Guid.NewGuid(), TipoServizio = "118" },
            new TipologiaServizio { Id = Guid.NewGuid(), TipoServizio = "Automedica" },
            new TipologiaServizio { Id = Guid.NewGuid(), TipoServizio = "CMR" },
            new TipologiaServizio { Id = Guid.NewGuid(), TipoServizio = "Guardia Medica" },
            new TipologiaServizio { Id = Guid.NewGuid(), TipoServizio = "Stazionamento" },
            new TipologiaServizio { Id = Guid.NewGuid(), TipoServizio = "Trasporto" },
            new TipologiaServizio { Id = Guid.NewGuid(), TipoServizio = "Trasporto Ambulanza" },
            new TipologiaServizio { Id = Guid.NewGuid(), TipoServizio = "Trasporto Disabili" },
            new TipologiaServizio { Id = Guid.NewGuid(), TipoServizio = "Trasporto Speciale" }
        };

        dbContext.TipologieServizio.AddRange(tipologieServizio);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedDataPrezziServiziAsync(IServiceProvider services, ILogger logger)
    {
        var dbContext = services.GetRequiredService<RegistroServiziDbContext>();

        if (await dbContext.PrezziServizi.AnyAsync())
        {
            logger.LogInformation("Prezzi Servizi data already exists. Skipping seeding.");
            return;
        }

        var tipologieServizio = await dbContext.TipologieServizio.ToListAsync();

        if (tipologieServizio.Count == 0)
        {
            logger.LogWarning("No Tipologie Servizio found. Please seed Tipologie Servizio before seeding Prezzi Servizi.");
            return;
        }

        foreach (var tipoServizio in tipologieServizio)
        {
            var prezzoServizio = new PrezzoServizio
            {
                TipologiaServizioId = tipoServizio.Id,
                CostoFisso = 0.0m,
                CostoKm = 0.0m,
                SecondoTrasportato = 0.0m,
                FermoMacchina = 0.0m,
                Accompagnatore = 0.0m,
                ScontoSocio = 0
            };

            dbContext.PrezziServizi.Add(prezzoServizio);
        }

        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedDataStatiBollaAsync(IServiceProvider services, ILogger logger)
    {
        var dbContext = services.GetRequiredService<RegistroServiziDbContext>();

        if (await dbContext.StatiBolla.AnyAsync())
        {
            logger.LogInformation("Stati Bolla data already exists. Skipping seeding.");
            return;
        }

        var statiBolla = new List<StatoBolla>
        {
            new StatoBolla { Id = Guid.NewGuid(), Descrizione = "Regolare" },
            new StatoBolla { Id = Guid.NewGuid(), Descrizione = "Rifiuto Firmato" },
            new StatoBolla { Id = Guid.NewGuid(), Descrizione = "Rifiuto Non Firmato" },
            new StatoBolla { Id = Guid.NewGuid(), Descrizione = "Si Allontana" },
            new StatoBolla { Id = Guid.NewGuid(), Descrizione = "Vuoto" },
            new StatoBolla { Id = Guid.NewGuid(), Descrizione = "Interrotta" },
            new StatoBolla { Id = Guid.NewGuid(), Descrizione = "Evacuato con Elisoccorso" },
            new StatoBolla { Id = Guid.NewGuid(), Descrizione = "Deceduto" },
            new StatoBolla { Id = Guid.NewGuid(), Descrizione = "Evacuato da altro MSB" }
        };

        await dbContext.StatiBolla.AddRangeAsync(statiBolla);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedDataTitoliStudioAsync(IServiceProvider services, ILogger logger)
    {
        var dbContext = services.GetRequiredService<RegistroServiziDbContext>();

        if (await dbContext.TitoliStudio.AnyAsync())
        {
            logger.LogInformation("Titoli Studio data already exists. Skipping seeding.");
            return;
        }

        var titoliStudio = new List<TitoloStudio>
        {
            new TitoloStudio { Id = Guid.NewGuid(), Descrizione = "Nessuno" },
            new TitoloStudio { Id = Guid.NewGuid(), Descrizione = "Licenza Elementare" },
            new TitoloStudio { Id = Guid.NewGuid(), Descrizione = "Licenza Media" },
            new TitoloStudio { Id = Guid.NewGuid(), Descrizione = "Qualifica Professionale" },
            new TitoloStudio { Id = Guid.NewGuid(), Descrizione = "Diploma" },
            new TitoloStudio { Id = Guid.NewGuid(), Descrizione = "Laurea" },
            new TitoloStudio { Id = Guid.NewGuid(), Descrizione = "Diploma di Laurea" },
            new TitoloStudio { Id = Guid.NewGuid(), Descrizione = "Dottorato" },
            new TitoloStudio { Id = Guid.NewGuid(), Descrizione = "Dottorato di Ricerca" },
            new TitoloStudio { Id = Guid.NewGuid(), Descrizione = "Master" }
        };

        await dbContext.TitoliStudio.AddRangeAsync(titoliStudio);
        await dbContext.SaveChangesAsync();
    }
}