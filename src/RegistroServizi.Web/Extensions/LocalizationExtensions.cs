namespace RegistroServizi.Web.Extensions;

public static class LocalizationExtensions
{
    public static IServiceCollection AddRegistroServiziLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLocalization();

        var defaultCulture = new CultureInfo(configuration[$"{LocalizationCustomOptions.SectionName}:DefaultCulture"] ?? "en-US"); // Default to "en-US" if not specified
        var defaultUICulture = new CultureInfo(configuration[$"{LocalizationCustomOptions.SectionName}:DefaultUICulture"] ?? "en"); // Default to "en" if not specified

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(defaultCulture, defaultUICulture);
            options.SupportedCultures = [defaultCulture];
            options.SupportedUICultures = [defaultUICulture];
            options.RequestCultureProviders = [];
        });

        return services;
    }
}