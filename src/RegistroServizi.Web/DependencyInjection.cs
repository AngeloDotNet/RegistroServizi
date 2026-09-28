namespace RegistroServizi.Web;

/// <summary>
/// Provides extension methods for registering application options, localization, authentication, and proxy-related services.
/// </summary>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Binds <typeparamref name="TOptions"/> to the specified configuration section, validates its data annotations,
        /// and enables validation when the application starts.
        /// </summary>
        /// <typeparam name="TOptions">The options type to configure.</typeparam>
        /// <param name="configuration">The configuration source used to read option values.</param>
        /// <param name="sectionName">The configuration section name that contains the option values.</param>
        /// <returns>An <see cref="OptionsBuilder{TOptions}"/> that can be further configured.</returns>
        public OptionsBuilder<TOptions> ConfigureAndValidate<TOptions>(IConfiguration configuration, string sectionName) where TOptions : class
        {
            return services.AddOptions<TOptions>()
                .Bind(configuration.GetSection(sectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }

        /// <summary>
        /// Registers localization services and configures request localization using the <c>SupportedCultures</c>
        /// configuration section.
        /// </summary>
        /// <remarks>
        /// The default culture is set to <c>it-IT</c>, and a <see cref="CookieRequestCultureProvider"/> is inserted
        /// at the beginning of the request culture provider list.
        /// </remarks>
        /// <param name="configuration">The configuration source used to read the <c>SupportedCultures</c> array.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance to support method chaining.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the <c>SupportedCultures</c> section is missing or cannot be read.
        /// </exception>
        public IServiceCollection AddRegistroServiziLocalization(IConfiguration configuration)
        {
            // List of supported cultures: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-10.0
            var supportedCultures = configuration.GetSection("SupportedCultures").Get<string[]>()
                ?? throw new InvalidOperationException("SupportedCultures configuration section is missing.");

            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SetDefaultCulture("it-IT")
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);

                options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
            });

            return services;
        }

        /// <summary>
        /// Registers application configuration options and validates them on startup.
        /// </summary>
        /// <remarks>
        /// This method configures the following option types:
        /// <list type="bullet">
        /// <item><description><see cref="ApplicazioneOptions"/></description></item>
        /// <item><description><see cref="AdminUserOptions"/></description></item>
        /// <item><description><see cref="AssociazioneOptions"/></description></item>
        /// <item><description><see cref="MudBlazorDataGridOptions"/></description></item>
        /// </list>
        /// </remarks>
        /// <param name="configuration">The configuration source used to bind and validate option values.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance to support method chaining.</returns>
        public IServiceCollection AddConfigurationOptions(IConfiguration configuration)
        {
            services.ConfigureAndValidate<ApplicazioneOptions>(configuration, nameof(ApplicazioneOptions));
            services.ConfigureAndValidate<AdminUserOptions>(configuration, nameof(AdminUserOptions));

            services.ConfigureAndValidate<AssociazioneOptions>(configuration, nameof(AssociazioneOptions));
            services.ConfigureAndValidate<MudBlazorDataGridOptions>(configuration, MudBlazorDataGridOptions.SectionName);

            return services;
        }

        /// <summary>
        /// Registers authentication and ASP.NET Core Identity services and configures application cookie behavior.
        /// </summary>
        /// <remarks>
        /// Identity settings are read from the <c>Identity</c> configuration section.
        /// Cookie security is relaxed for development environments and enforced otherwise.
        /// </remarks>
        /// <param name="configuration">The configuration source used to read Identity and environment settings.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance to support method chaining.</returns>
        public IServiceCollection AddRegistroServiziAuth(IConfiguration configuration)
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
                options.Cookie.SecurePolicy = configuration.GetValue("Environment:IsDevelopment", false) ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
            });

            var identityConfig = configuration.GetSection("Identity");

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = identityConfig.GetValue("RequireConfirmedAccount", false);
                options.Lockout.AllowedForNewUsers = identityConfig.GetValue("AllowedForNewUsers", true);
                options.Lockout.MaxFailedAccessAttempts = identityConfig.GetValue("MaxFailedAccessAttempts", 5);
                options.Lockout.DefaultLockoutTimeSpan = identityConfig.GetValue("DefaultLockoutTimeSpan", TimeSpan.FromMinutes(15));
                options.Password.RequiredLength = identityConfig.GetValue("Password:RequiredLength", 10);
                options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
            })
            .AddEntityFrameworkStores<RegistroServiziDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

            return services;
        }

        /// <summary>
        /// Registers proxy-related infrastructure services, including forwarded headers, HSTS, and antiforgery settings.
        /// </summary>
        /// <remarks>
        /// Forwarded headers are enabled for <c>X-Forwarded-For</c> and <c>X-Forwarded-Proto</c>, and known proxies
        /// and networks are cleared to allow proxy-aware deployment scenarios.
        /// </remarks>
        /// <param name="configuration">The configuration source used to determine environment-specific security settings.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance to support method chaining.</returns>
        public IServiceCollection AddRegistroServiziProxy(IConfiguration configuration)
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
                options.Cookie.SecurePolicy = configuration.GetValue("Environment:IsDevelopment", false) ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
            });

            return services;
        }
    }
}