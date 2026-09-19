namespace RegistroServizi.Web;

/// <summary>
/// Provides extension methods for registering services and configuring options, including binding configuration
/// sections and enabling data‑annotation validation.
/// </summary>
/// <remarks>Intended for use during application startup to centralize dependency-registration and
/// options-configuration logic. Includes helpers that bind configuration sections to options types and validate them on
/// host startup.</remarks>
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
}