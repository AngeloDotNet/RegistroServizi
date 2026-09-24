using MudBlazor;

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
        var supportedCultures = configuration.GetSection("SupportedCultures").Get<string[]>() ?? ["it"];

        services.AddLocalization();
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.SetDefaultCulture("it")
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
        });

        return services;
    }

    /// <summary>
    /// Gets a DialogOptions instance configured for modal dialogs with escape and backdrop closing disabled and
    /// centered positioning.
    /// </summary>
    /// <returns>A DialogOptions configured with CloseOnEscapeKey = false, BackdropClick = false, and Position =
    /// DialogPosition.Center.</returns>
    public static DialogOptions GetDefaultDialogOptions() => new DialogOptions
    {
        CloseOnEscapeKey = false,
        BackdropClick = false,
        Position = DialogPosition.Center
    };

    //public static DialogParameters GetConfirmDialogParameters(string message) => new DialogParameters
    //{
    //    //{ "ContentText", "Sei sicuro di voler procedere?" },
    //    { "ContentText", message },
    //    { "ButtonText", "Conferma" },
    //    { "Color", Color.Success }
    //};

    public static DialogParameters GetConfirmDialogParameters<TParam>(string message) => new DialogParameters<TParam>
    {
        { "ContentText", message },
        { "BtnCancel", "Annulla" },
        { "BtnConfirm", "Conferma" },
        { "Color", Color.Success }
    };
}