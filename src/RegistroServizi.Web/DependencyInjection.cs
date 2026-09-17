namespace RegistroServizi.Web;

public static class DependencyInjection
{
    public static void ConfigureAndValidate<TOptions>(string section)
        => builder.Services.AddOptions<TOptions>()
            .Bind(builder.Configuration.GetSection(section))
            .ValidateDataAnnotations()
            .ValidateOnStart();
}
