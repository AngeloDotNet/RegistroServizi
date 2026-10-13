namespace RegistroServizi.Application.Options;

public sealed class LocalizationCustomOptions
{
    public const string SectionName = "Localization";

    [Required] public string DefaultCulture { get; init; } = string.Empty;
    [Required] public string DefaultUICulture { get; init; } = string.Empty;
}