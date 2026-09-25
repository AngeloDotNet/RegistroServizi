namespace RegistroServizi.Web;

/// <summary>
/// Provides extension methods to register and configure options and localization services.
/// </summary>
/// <remarks>Includes ConfigureAndValidate<TOptions> to bind an options type to a configuration section, enable
/// data-annotation validation, and validate options at host startup; and AddRegistroServiziLocalization to read
/// SupportedCultures from configuration, call AddLocalization, set the default culture to it, register supported
/// cultures and UI cultures, and insert a CookieRequestCultureProvider as the first RequestCultureProvider.</remarks>
public static class DependencyInjection
{
    /// <summary>
    /// Adds and configures options of type TOptions by binding to the specified configuration section, enabling data
    /// annotation validation, and validating options on application start.
    /// </summary>
    /// <remarks>Validation uses data annotations and is performed during host startup. TOptions must be a
    /// reference type.</remarks>
    /// <typeparam name="TOptions">The options type to configure.</typeparam>
    /// <param name="services">The IServiceCollection to register the configured options with.</param>
    /// <param name="configuration">The IConfiguration that provides the option values.</param>
    /// <param name="sectionName">The configuration section name to bind to TOptions.</param>
    /// <returns>An OptionsBuilder<TOptions> for further configuration.</returns>
    public static OptionsBuilder<TOptions> ConfigureAndValidate<TOptions>(this IServiceCollection services, IConfiguration configuration, string sectionName) where TOptions : class
    {
        return services.AddOptions<TOptions>()
            .Bind(configuration.GetSection(sectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    /// <summary>
    /// Adds localization services and configures RequestLocalizationOptions using the 'SupportedCultures' configuration
    /// section, sets the default culture to 'it', and registers a CookieRequestCultureProvider.
    /// </summary>
    /// <remarks>Reads supported cultures from the 'SupportedCultures' configuration section, calls
    /// AddLocalization, sets default culture to 'it', registers supported cultures and UI cultures, and inserts a
    /// CookieRequestCultureProvider as the first RequestCultureProvider.</remarks>
    /// <param name="services">The service collection to which localization services and request localization options are added.</param>
    /// <param name="configuration">The configuration used to retrieve the 'SupportedCultures' array.</param>
    /// <returns>The same IServiceCollection instance to allow method chaining.</returns>
    public static IServiceCollection AddRegistroServiziLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        // List of supported cultures: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-10.0
        var supportedCultures = configuration.GetSection("SupportedCultures").Get<string[]>() ?? throw new InvalidOperationException("SupportedCultures configuration section is missing.");

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
}