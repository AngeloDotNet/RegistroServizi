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
        //var supportedCultures = configuration.GetSection("SupportedCultures").Get<string[]>() ?? new[] { "en", "it" };
        var supportedCultures = configuration.GetSection("SupportedCultures").Get<string[]>();

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

    ///// <summary>
    ///// Gets a DialogOptions instance configured for modal dialogs with escape and backdrop closing disabled and
    ///// centered positioning.
    ///// </summary>
    ///// <returns>A DialogOptions configured with CloseOnEscapeKey = false, BackdropClick = false, and Position =
    ///// DialogPosition.Center.</returns>
    //public static DialogOptions GetDefaultDialogOptions() => new DialogOptions
    //{
    //    CloseOnEscapeKey = false,
    //    BackdropClick = false,
    //    Position = DialogPosition.Center
    //};

    ///// <summary>
    ///// Creates a DialogOptions configured for a centered, non-dismissible backdrop-filter dialog with a blurred background.
    ///// </summary>
    ///// <remarks>Use for dialogs that apply a backdrop blur and must not be dismissed by escape key or backdrop clicks.</remarks>
    ///// <returns>A DialogOptions configured with BackgroundClass = "blurry-dialog", CloseOnEscapeKey = false, BackdropClick =
    ///// false, and Position = DialogPosition.Center.</returns>
    //public static DialogOptions GetBackdropFilterDialogOptions() => new DialogOptions
    //{
    //    BackgroundClass = "blurry-dialog",
    //    CloseOnEscapeKey = false,
    //    BackdropClick = false,
    //    Position = DialogPosition.Center
    //};

    ///// <summary>
    ///// Creates dialog parameters for a confirmation dialog using the specified message.
    ///// </summary>
    ///// <remarks>Button labels are set to Italian: 'Annulla' (cancel) and 'Conferma' (confirm).</remarks>
    ///// <typeparam name="TParam">The type of the dialog parameter model.</typeparam>
    ///// <param name="message">The confirmation message displayed in the dialog.</param>
    ///// <returns>A DialogParameters instance containing ContentText, localized button labels, and Color set to Success.</returns>
    ////public static DialogParameters GetConfirmDialogParameters<TParam>(string message) => new DialogParameters<TParam>
    ////{
    ////    { "ContentText", message },
    ////    { "BtnCancel", "Annulla" },
    ////    { "BtnCancelColor", Color.Default },
    ////    { "BtnConfirm", "Conferma" },
    ////    { "BtnConfirmColor", Color.Primary }
    ////};

    ///// <summary>
    ///// Creates dialog parameters for a create dialog using the specified title and cancel button text.
    ///// </summary>
    ///// <typeparam name="TParam">The type of the dialog parameter model.</typeparam>
    ///// <param name="title">The title displayed in the dialog.</param>
    ///// <param name="cancelText">The text displayed on the cancel button.</param>
    ///// <returns>A DialogParameters instance containing EditDialogId, Title, localized button labels, and Color set to Primary.</returns>
    //public static DialogParameters GetCreateItemDialogParameters<TParam>(string title, string cancelText) => new DialogParameters<TParam>
    //{
    //    { "EditDialogId", null },
    //    { "Title", title },
    //    { "BtnCancelText", cancelText },
    //    { "BtnCancelColor", Color.Default },
    //    { "BtnConfirmColor", Color.Primary }
    //};

    ///// <summary>
    ///// Creates dialog parameters for an edit dialog populated with the specified dialog id, title, cancel text, and default button colors.
    ///// </summary>
    ///// <remarks>BtnCancelColor is set to Color.Default and BtnConfirmColor is set to Color.Primary.</remarks>
    ///// <typeparam name="TParam">Payload type attached to the dialog parameters.</typeparam>
    ///// <param name="editDialogId">Edit dialog identifier.</param>
    ///// <param name="title">Dialog title.</param>
    ///// <param name="cancelText">Cancel button text.</param>
    ///// <returns>A DialogParameters instance populated with the provided values and default button colors (BtnCancelColor =
    ///// Color.Default, BtnConfirmColor = Color.Primary).</returns>
    //public static DialogParameters GetEditItemDialogParameters<TParam>(Guid editDialogId, string title, string cancelText) => new DialogParameters<TParam>
    //{
    //    { "EditDialogId", editDialogId },
    //    { "Title", title },
    //    { "BtnCancelText", cancelText },
    //    { "BtnCancelColor", Color.Default },
    //    { "BtnConfirmColor", Color.Primary }
    //};
}