namespace RegistroServizi.Web;

/// <summary>
/// Provides extension methods for registering application configuration, authentication, and proxy-related services.
/// </summary>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Binds an options type to a configuration section, validates its data annotations, and fails fast on startup if invalid.
        /// </summary>
        /// <typeparam name="TOptions">The options type to bind and validate.</typeparam>
        /// <param name="configuration">The application configuration source.</param>
        /// <param name="sectionName">The name of the configuration section that contains the options values.</param>
        /// <returns>An <see cref="OptionsBuilder{TOptions}"/> that can be further configured.</returns>
        public OptionsBuilder<TOptions> ConfigureAndValidate<TOptions>(IConfiguration configuration, string sectionName) where TOptions : class
        {
            return services.AddOptions<TOptions>()
                .Bind(configuration.GetSection(sectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }

        /// <summary>
        /// Registers and validates application configuration options.
        /// </summary>
        /// <remarks>
        /// The following options are bound from configuration and validated at startup:
        /// <list type="bullet">
        /// <item><description><see cref="ApplicazioneOptions"/></description></item>
        /// <item><description><see cref="AdminUserOptions"/></description></item>
        /// <item><description><see cref="AssociazioneOptions"/></description></item>
        /// <item><description><see cref="MudBlazorDataGridOptions"/></description></item>
        /// </list>
        /// </remarks>
        /// <param name="configuration">The application configuration source.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public IServiceCollection AddConfigurationOptions(IConfiguration configuration)
        {
            services.ConfigureAndValidate<ApplicazioneOptions>(configuration, nameof(ApplicazioneOptions));
            services.ConfigureAndValidate<AdminUserOptions>(configuration, nameof(AdminUserOptions));

            services.ConfigureAndValidate<AssociazioneOptions>(configuration, nameof(AssociazioneOptions));
            services.ConfigureAndValidate<MudBlazorDataGridOptions>(configuration, MudBlazorDataGridOptions.SectionName);

            services.ConfigureAndValidate<LocalizationCustomOptions>(configuration, LocalizationCustomOptions.SectionName);

            return services;
        }

        /// <summary>
        /// Registers authentication, ASP.NET Core Identity, and application cookie settings.
        /// </summary>
        /// <remarks>
        /// Identity settings are read from the <c>Identity</c> configuration section.
        /// Cookie security is relaxed for development environments and enforced in non-development environments.
        /// </remarks>
        /// <param name="configuration">The application configuration source.</param>
        /// <param name="isDevelopment">Indicates whether the application is running in a development environment.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public IServiceCollection AddRegistroServiziAuth(IConfiguration configuration, bool isDevelopment, IConfigurationSection identitySection)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                //options.Cookie.SecurePolicy = configuration.GetValue("Environment:IsDevelopment", false) ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
                options.Cookie.SecurePolicy = isDevelopment ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
            });

            //var identityConfig = configuration.GetSection("Identity");

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //options.SignIn.RequireConfirmedAccount = identityConfig.GetValue("RequireConfirmedAccount", false);
                //options.Lockout.AllowedForNewUsers = identityConfig.GetValue("AllowedForNewUsers", true);
                //options.Lockout.MaxFailedAccessAttempts = identityConfig.GetValue("MaxFailedAccessAttempts", 5);
                //options.Lockout.DefaultLockoutTimeSpan = identityConfig.GetValue("DefaultLockoutTimeSpan", TimeSpan.FromMinutes(15));
                //options.Password.RequiredLength = identityConfig.GetValue("Password:RequiredLength", 10);
                options.SignIn.RequireConfirmedAccount = identitySection.GetValue("RequireConfirmedAccount", false);
                options.Lockout.AllowedForNewUsers = identitySection.GetValue("AllowedForNewUsers", true);
                options.Lockout.MaxFailedAccessAttempts = identitySection.GetValue("MaxFailedAccessAttempts", 3);
                options.Lockout.DefaultLockoutTimeSpan = identitySection.GetValue("DefaultLockoutTimeSpan", TimeSpan.FromMinutes(5));
                options.Password.RequiredLength = identitySection.GetValue("Password:RequiredLength", 6);
                options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
            })
            .AddEntityFrameworkStores<RegistroServiziDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

            return services;
        }

        /// <summary>
        /// Registers proxy-related infrastructure services used when the application runs behind a reverse proxy.
        /// </summary>
        /// <remarks>
        /// This configuration enables support for <c>X-Forwarded-For</c> and <c>X-Forwarded-Proto</c>,
        /// clears known proxies and networks so proxy-aware deployments can be configured externally,
        /// configures HSTS, and applies antiforgery cookie security settings based on the current environment.
        /// </remarks>
        /// <param name="configuration">The application configuration source.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        public IServiceCollection AddRegistroServiziProxy(IConfiguration configuration, bool isDevelopment)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownIPNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddHsts(options =>
            {
                options.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddAntiforgery(options =>
            {
                //options.Cookie.SecurePolicy = configuration.GetValue("Environment:IsDevelopment", false) ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
                options.Cookie.SecurePolicy = isDevelopment ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
            });

            return services;
        }
    }
}