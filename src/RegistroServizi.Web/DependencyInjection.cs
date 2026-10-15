using RegistroServizi.Web.Configuration.Options;

namespace RegistroServizi.Web;

/// <summary>
/// Provides extension methods for registering application configuration, authentication, and proxy-related services.
/// </summary>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Binds an options type to a configuration section, validates its data annotations, and validates it when the
        /// application starts.
        /// </summary>
        /// <typeparam name="TOptions">The options type to bind and validate.</typeparam>
        /// <param name="configuration">The application configuration source.</param>
        /// <param name="sectionName">The name of the configuration section that contains the options values.</param>
        /// <returns>
        /// An <see cref="OptionsBuilder{TOptions}"/> that can be used to apply additional configuration or validation.
        /// </returns>
        /// <remarks>
        /// Startup validation prevents the application from running with invalid configuration and makes configuration
        /// errors visible during application startup rather than during the first use of the options.
        /// </remarks>
        public OptionsBuilder<TOptions> ConfigureAndValidate<TOptions>(IConfiguration configuration, string sectionName) where TOptions : class
        {
            return services.AddOptions<TOptions>()
                .Bind(configuration.GetSection(sectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }

        /// <summary>
        /// Registers and validates the application configuration options used by the web application.
        /// </summary>
        /// <param name="configuration">The application configuration source.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        /// <remarks>
        /// The following options are bound from configuration and validated when the application starts:
        /// <list type="bullet">
        /// <item><description><see cref="ApplicazioneOptions"/></description></item>
        /// <item><description><see cref="AdminUserOptions"/></description></item>
        /// <item><description><see cref="AssociazioneOptions"/></description></item>
        /// <item><description><see cref="MudBlazorDataGridOptions"/></description></item>
        /// <item><description><see cref="LocalizationCustomOptions"/></description></item>
        /// </list>
        /// </remarks>
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
        /// <param name="configuration">The application configuration source.</param>
        /// <param name="isDevelopment">
        /// <see langword="true"/> when the application is running in a development environment; otherwise,
        /// <see langword="false"/>.
        /// </param>
        /// <param name="identitySection">
        /// The configuration section containing ASP.NET Core Identity settings.
        /// </param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        /// <remarks>
        /// Identity settings are read from the supplied configuration section. The method registers
        /// <see cref="ApplicationUser"/> with <see cref="IdentityRole"/>, persists Identity data through
        /// <see cref="RegistroServiziDbContext"/>, and adds the default sign-in manager and token providers.
        /// <para>
        /// Application cookies use <see cref="SameSiteMode.Lax"/> and are HTTP-only. Secure cookies are required
        /// outside development environments to prevent authentication cookies from being transmitted over an
        /// unencrypted connection.
        /// </para>
        /// </remarks>
        public IServiceCollection AddRegistroServiziAuth(
            IConfiguration configuration,
            bool isDevelopment,
            IConfigurationSection identitySection)
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
        /// <param name="configuration">The application configuration source.</param>
        /// <param name="isDevelopment">
        /// <see langword="true"/> when the application is running in a development environment; otherwise,
        /// <see langword="false"/>.
        /// </param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        /// <remarks>
        /// This configuration enables support for <c>X-Forwarded-For</c> and <c>X-Forwarded-Proto</c>, allowing
        /// ASP.NET Core to reconstruct the original client address and request scheme when requests pass through
        /// a reverse proxy.
        /// <para>
        /// Known proxies and networks are cleared so the deployment can configure trusted proxy infrastructure
        /// externally. The hosting environment must ensure that forwarded headers are accepted only from trusted
        /// proxies.
        /// </para>
        /// <para>
        /// HSTS is configured with a maximum age of 60 days. Antiforgery cookies use relaxed secure-cookie behavior
        /// during development and require HTTPS elsewhere.
        /// </para>
        /// <para>
        /// For additional details, see the
        /// <see href="https://learn.microsoft.com/aspnet/core/host-and-deploy/proxy-load-balancer">
        /// ASP.NET Core proxy and load-balancer guidance
        /// </see>.
        /// </para>
        /// </remarks>
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