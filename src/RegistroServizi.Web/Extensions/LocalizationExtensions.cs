namespace RegistroServizi.Web.Extensions;

public static class LocalizationExtensions
{
    public static IServiceCollection AddRegistroServiziLocalization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLocalization();

        //var cultureName = configuration[$"{LocalizationCustomOptions.SectionName}:DefaultCulture"] ?? "en-US"; // Default to "en-US" if not specified
        //var cultureUIName = configuration[$"{LocalizationCustomOptions.SectionName}:DefaultUICulture"] ?? "en"; // Default to "en" if not specified

        //var culture = new CultureInfo(cultureName);

        //services.Configure<RequestLocalizationOptions>(options =>
        //{
        //    options.DefaultRequestCulture = new RequestCulture(culture);
        //    options.SupportedCultures = [culture];
        //    options.SupportedUICultures = [culture];
        //    options.RequestCultureProviders = [];
        //});

        var defaultCulture = new CultureInfo(configuration[$"{LocalizationCustomOptions.SectionName}:DefaultCulture"] ?? "en-US"); // Default to "en-US" if not specified
        var defaultUICulture = new CultureInfo(configuration[$"{LocalizationCustomOptions.SectionName}:DefaultUICulture"] ?? "en"); // Default to "en" if not specified

        //CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
        //CultureInfo.DefaultThreadCurrentUICulture = defaultUICulture;

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(defaultCulture, defaultUICulture);
            options.SupportedCultures = [defaultCulture];
            options.SupportedUICultures = [defaultUICulture];
            //options.RequestCultureProviders = Array.Empty<IRequestCultureProvider>();
            options.RequestCultureProviders = [];
        });

        return services;
    }
}