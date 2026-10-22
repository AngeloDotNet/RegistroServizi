namespace RegistroServizi.Application.Options;

/// <summary>
/// Defines the localization settings used by the application.
/// </summary>
/// <remarks>
/// The settings are bound from the <c>Localization</c> configuration section and
/// validated when the application starts. <see cref="DefaultCulture"/> controls
/// culture-specific formatting, while <see cref="DefaultUICulture"/> controls
/// localized resource selection.
/// </remarks>
public sealed class LocalizationCustomOptions
{
    /// <summary>
    /// Gets the name of the configuration section containing the localization settings.
    /// </summary>
    public const string SectionName = "Localization";

    /// <summary>
    /// Gets the default culture used for culture-specific formatting, such as dates and numbers.
    /// </summary>
    /// <remarks>
    /// The value should be a valid .NET culture name, such as <c>en-US</c> or <c>it-IT</c>.
    /// </remarks>
    [Required] public string DefaultCulture { get; init; } = string.Empty;

    /// <summary>
    /// Gets the default UI culture used to select localized resources and text.
    /// </summary>
    /// <remarks>
    /// The value should be a valid .NET UI culture name, such as <c>en</c> or <c>it</c>.
    /// </remarks>
    [Required] public string DefaultUICulture { get; init; } = string.Empty;
}