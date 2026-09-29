using MudBlazor;
using MudBlazor.Services;

namespace RegistroServizi.Web;

public class Program
{
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