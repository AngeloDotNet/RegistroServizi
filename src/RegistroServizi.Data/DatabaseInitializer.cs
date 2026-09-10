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

            // Seed data for Applicazione
            await SeedDataApplicazioneAsync(scope.ServiceProvider, logger);

            // Seed data for TipologiaServizio
            await SeedDataTipologiaServizioAsync(scope.ServiceProvider, logger);

            // Seed data for PrezzoServizio
            await SeedDataPrezziServiziAsync(scope.ServiceProvider, logger);
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
            Id = Guid.Parse("b5e6d4a8-a0e4-4f1a-a295-2cf1f53cd1c3"),
            //Id = Guid.NewGuid(),
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
            new TipologiaServizio { Id = Guid.Parse("a86ed006-70ba-4465-90fd-f549ab470343"), TipoServizio = "118" },
            new TipologiaServizio { Id = Guid.Parse("918ae96a-6f8b-4fdc-89ea-fab93759046c"), TipoServizio = "Automedica" },
            new TipologiaServizio { Id = Guid.Parse("381e7054-5e58-4a52-9b77-807e1946a89f"), TipoServizio = "CMR" },
            new TipologiaServizio { Id = Guid.Parse("a6bff4fd-04a6-43f2-956e-6a2b3fc13228"), TipoServizio = "Guardia Medica" },
            new TipologiaServizio { Id = Guid.Parse("96bcc2d1-a283-4856-8af0-9cd1c8172569"), TipoServizio = "Stazionamento" },
            new TipologiaServizio { Id = Guid.Parse("205b3142-5f80-4dfb-8239-66b29b7c5328"), TipoServizio = "Trasporto" },
            new TipologiaServizio { Id = Guid.Parse("5c85a7b8-0bac-45a5-b726-e65ae536d23a"), TipoServizio = "Trasporto Ambulanza" },
            new TipologiaServizio { Id = Guid.Parse("f598d2ea-f744-4f60-8d98-bf90093cfd4b"), TipoServizio = "Trasporto Disabili" },
            new TipologiaServizio { Id = Guid.Parse("03b5e62a-0850-4721-8f00-49c190f573e2"), TipoServizio = "Trasporto Speciale" }
        };

        dbContext.TipologieServizio.AddRange(tipologieServizio);
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedDataPrezziServiziAsync(IServiceProvider services, ILogger logger)
    {
        var dbContext = services.GetRequiredService<RegistroServiziDbContext>();
        var existingPrezziServizi = await dbContext.PrezziServizi.FirstOrDefaultAsync();

        if (existingPrezziServizi != null)
        {
            logger.LogInformation("Prezzi Servizi data already exists. Skipping seeding.");
            return;
        }

        var prezziServizi = new List<PrezzoServizio>
        {
            new PrezzoServizio
            {
                TipologiaServizioId = Guid.Parse("a86ed006-70ba-4465-90fd-f549ab470343"),
                CostoFisso = 0.0m,
                CostoKm = 0.0m,
                SecondoTrasportato = 0.0m,
                FermoMacchina = 0.0m,
                Accompagnatore = 0.0m,
                ScontoSocio = 0
            },
            new PrezzoServizio
            {
                TipologiaServizioId = Guid.Parse("918ae96a-6f8b-4fdc-89ea-fab93759046c"),
                CostoFisso = 0.0m,
                CostoKm = 0.0m,
                SecondoTrasportato = 0.0m,
                FermoMacchina = 0.0m,
                Accompagnatore = 0.0m,
                ScontoSocio = 0
            },
            new PrezzoServizio
            {
                TipologiaServizioId = Guid.Parse("381e7054-5e58-4a52-9b77-807e1946a89f"),
                CostoFisso = 0.0m,
                CostoKm = 0.0m,
                SecondoTrasportato = 0.0m,
                FermoMacchina = 0.0m,
                Accompagnatore = 0.0m,
                ScontoSocio = 0
            },
            new PrezzoServizio
            {
                TipologiaServizioId = Guid.Parse("a6bff4fd-04a6-43f2-956e-6a2b3fc13228"),
                CostoFisso = 0.0m,
                CostoKm = 0.0m,
                SecondoTrasportato = 0.0m,
                FermoMacchina = 0.0m,
                Accompagnatore = 0.0m,
                ScontoSocio = 0
            },
            new PrezzoServizio
            {
                TipologiaServizioId = Guid.Parse("96bcc2d1-a283-4856-8af0-9cd1c8172569"),
                CostoFisso = 0.0m,
                CostoKm = 0.0m,
                SecondoTrasportato = 0.0m,
                FermoMacchina = 0.0m,
                Accompagnatore = 0.0m,
                ScontoSocio = 0
            },
            new PrezzoServizio
            {
                TipologiaServizioId = Guid.Parse("205b3142-5f80-4dfb-8239-66b29b7c5328"),
                CostoFisso = 0.0m,
                CostoKm = 0.0m,
                SecondoTrasportato = 0.0m,
                FermoMacchina = 0.0m,
                Accompagnatore = 0.0m,
                ScontoSocio = 0
            },
            new PrezzoServizio
            {
                TipologiaServizioId = Guid.Parse("5c85a7b8-0bac-45a5-b726-e65ae536d23a"),
                CostoFisso = 0.0m,
                CostoKm = 0.0m,
                SecondoTrasportato = 0.0m,
                FermoMacchina = 0.0m,
                Accompagnatore = 0.0m,
                ScontoSocio = 0
            },
            new PrezzoServizio
            {
                TipologiaServizioId = Guid.Parse("f598d2ea-f744-4f60-8d98-bf90093cfd4b"),
                CostoFisso = 0.0m,
                CostoKm = 0.0m,
                SecondoTrasportato = 0.0m,
                FermoMacchina = 0.0m,
                Accompagnatore = 0.0m,
                ScontoSocio = 0
            },
            new PrezzoServizio
            {
                TipologiaServizioId = Guid.Parse("03b5e62a-0850-4721-8f00-49c190f573e2"),
                CostoFisso = 0.0m,
                CostoKm = 0.0m,
                SecondoTrasportato = 0.0m,
                FermoMacchina = 0.0m,
                Accompagnatore = 0.0m,
                ScontoSocio = 0
            }
        };

        dbContext.PrezziServizi.AddRange(prezziServizi);
        await dbContext.SaveChangesAsync();
    }
}