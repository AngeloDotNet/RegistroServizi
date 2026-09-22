namespace RegistroServizi.Data;

/// <summary>
/// Applies pending EF Core migrations and seeds required initial data (roles, default administrator, application
/// metadata, service types, service prices, statuses, education titles, and hospitals) using a provided
/// IServiceProvider.
/// </summary>
/// <remarks>Creates a scoped service provider to resolve the DbContext, UserManager, RoleManager, and ILogger.
/// Operations are idempotent and skip seeding when data already exists; migrations are applied only when pending. Logs
/// progress and throws on unrecoverable errors such as failed migrations or role creation.</remarks>
public static class DatabaseInitializer
{
    /// <summary>
    /// Applies pending Entity Framework Core migrations and seeds initial application data using the provided service
    /// provider.
    /// </summary>
    /// <remarks>Creates a service scope, resolves RegistroServiziDbContext and
    /// ILogger<RegistroServiziDbContext>, applies pending migrations if any, invokes seed methods for roles, default
    /// users, Applicazione, TipologiaServizio, PrezzoServizio, StatiBolla, TitoliStudio, Ospedali, and Colonnine, 
    /// logs progress and logs and rethrows exceptions.</remarks>
    /// <param name="services">Service provider used to create a scope and resolve required services for database migration and data seeding.</param>
    /// <returns>A task that completes when migrations and seeding are finished.</returns>
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

            // Seed data for TipologiaServizio
            await SeedDataTipologiaServizioAsync(scope.ServiceProvider, logger);

            // Seed data for PrezzoServizio
            await SeedDataPrezziServiziAsync(scope.ServiceProvider, logger);

            // Seed data for StatiBolla
            await SeedDataStatiBollaAsync(scope.ServiceProvider, logger);

            // Seed data for TitoliStudio
            await SeedDataTitoliStudioAsync(scope.ServiceProvider, logger);

            // Seed data for Ospedali
            await SeedDataOspedaliAsync(scope.ServiceProvider, logger);

            // Seed data for Colonnine
            //await SeedDataColonnineAsync(scope.ServiceProvider, logger); //TODO: Aggiungere nuove colonnine (Da Cairoli in poi)
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while applying database migrations.");
            throw;
        }
    }

    /// <summary>
    /// Default role names used for initial role assignment.
    /// </summary>
    /// <remarks>Contains two role names: Admin and Manager. Values are obtained via nameof(Role.*) to stay
    /// synchronized with the Role enum.</remarks>
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

    private static async Task SeedDataOspedaliAsync(IServiceProvider services, ILogger logger)
    {
        var dbContext = services.GetRequiredService<RegistroServiziDbContext>();

        if (await dbContext.Ospedali.AnyAsync())
        {
            logger.LogInformation("Ospedali data already exists. Skipping seeding.");
            return;
        }

        var ospedali = new List<Ospedale>
        {
            new Ospedale {
                Id = Guid.NewGuid(),
                NomeOspedale = "H. Galmarini",
                Indirizzo = new Indirizzo("Via Angelo Zanaboni 1", "Tradate", "VA", 21049),
                Coordinate = new Coordinate(45.722205, 8.900592)
            }
        };

        await dbContext.Ospedali.AddRangeAsync(ospedali);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedDataColonnineAsync(IServiceProvider services, ILogger logger)
    {
        var dbContext = services.GetRequiredService<RegistroServiziDbContext>();
        if (await dbContext.Colonnine.AnyAsync())
        {
            logger.LogInformation("Colonnine data already exists. Skipping seeding.");
            return;
        }

        var colonnine = new List<Colonnina>
        {
            new Colonnina { Id = Guid.NewGuid(), NomeColonnina = "Certosa / Laghi", Comune = "Milano", Provincia = "MI", Coordinate = new Coordinate(45.49972, 9.13072) },
            new Colonnina { Id = Guid.NewGuid(), NomeColonnina = "Rubicone", Comune = "Milano", Provincia = "MI", Coordinate = new Coordinate(45.53158, 9.16177) },
            new Colonnina { Id = Guid.NewGuid(), NomeColonnina = "Nigra", Comune = "Milano", Provincia = "MI", Coordinate = new Coordinate(45.49667, 9.17107) },
            new Colonnina { Id = Guid.NewGuid(), NomeColonnina = "Maciachini", Comune = "Milano", Provincia = "MI", Coordinate = new Coordinate(45.49750, 9.18611) },
            new Colonnina { Id = Guid.NewGuid(), NomeColonnina = "Loreto", Comune = "Milano", Provincia = "MI", Coordinate = new Coordinate(45.48556, 9.21694) },
            new Colonnina { Id = Guid.NewGuid(), NomeColonnina = "Testi / Rodi", Comune = "Milano", Provincia = "MI", Coordinate = new Coordinate(45.51500, 9.20611) },
            new Colonnina { Id = Guid.NewGuid(), NomeColonnina = "Baiamonti", Comune = "Milano", Provincia = "MI", Coordinate = new Coordinate(45.48194, 9.18167) },
            new Colonnina { Id = Guid.NewGuid(), NomeColonnina = "Baracca", Comune = "Milano", Provincia = "MI", Coordinate = new Coordinate(45.46611, 9.16528) },
            new Colonnina { Id = Guid.NewGuid(), NomeColonnina = "Fontana / Duomo", Comune = "Milano", Provincia = "MI", Coordinate = new Coordinate(45.46361, 9.19389) },
        };

        await dbContext.Colonnine.AddRangeAsync(colonnine);
        await dbContext.SaveChangesAsync();
    }
}