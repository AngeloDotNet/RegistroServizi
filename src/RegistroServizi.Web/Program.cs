using MudBlazor;
using MudBlazor.Services;

namespace RegistroServizi.Web;

/// <summary>
/// Defines the entry point and host configuration for the RegistroServizi Blazor application.
/// </summary>
/// <remarks>
/// The application uses .NET hosting to configure dependency injection, authentication,
/// localization, data access, MudBlazor services, and the request pipeline before starting
/// the interactive server-rendered Blazor application.
/// </remarks>
public class Program
{
    /// <summary>
    /// Builds, configures, and starts the RegistroServizi web application.
    /// </summary>
    /// <param name="args">
    /// Command-line arguments supplied to the application host.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous lifetime of the web application.
    /// </returns>
    /// <remarks>
    /// Database migrations are applied before the application begins processing requests.
    /// The request pipeline supports forwarded headers, HTTPS redirection, authentication,
    /// authorization, antiforgery protection, localization, static assets, and interactive
    /// server-side Blazor rendering.
    /// </remarks>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;    //default: BottomLeft
            config.SnackbarConfiguration.RequireInteraction = false;
            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = false;                                 //default: true
            config.SnackbarConfiguration.VisibleStateDuration = 5000;                           //default: 10000
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });

        var isDevelopment = builder.Configuration.GetValue("Environment:IsDevelopment", false);
        var identityConfig = builder.Configuration.GetSection("Identity");

        builder.Services.AddRegistroServiziLocalization(builder.Configuration);
        builder.Services.AddRegistroServiziProxy(builder.Configuration, isDevelopment);

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityRedirectManager>();

        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        builder.Services.AddScoped<DataGridHelper>();

        builder.Services.AddRegistroServiziData(builder.Configuration);
        builder.Services.AddRegistroServiziAuth(builder.Configuration, isDevelopment, identityConfig);

        builder.Services.AddRegistroServiziApplication();
        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        builder.Services.AddScoped<UserExtensions>();
        builder.Services.AddConfigurationOptions(builder.Configuration);

        var app = builder.Build();

        await DatabaseInitializer.MigrateAsync(app.Services);
        app.UseForwardedHeaders();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAntiforgery();
        app.UseRequestLocalization();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapAdditionalIdentityEndpoints();
        app.Run();
    }
}