using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using RegistroServizi.Application;
using RegistroServizi.Data;
using RegistroServizi.Data.Identity;
using RegistroServizi.Web.Components;
using RegistroServizi.Web.Components.Account;

namespace RegistroServizi.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddControllers();
        builder.Services.AddAuthorization();

        builder.Services.AddMudServices();
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownIPNetworks.Clear();
            options.KnownProxies.Clear();
        });

        builder.Services.AddAntiforgery(options =>
        {
            options.Cookie.SecurePolicy = builder.Environment.IsDevelopment() ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
        });

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityRedirectManager>();

        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        }).AddIdentityCookies();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = builder.Environment.IsDevelopment() ? CookieSecurePolicy.SameAsRequest : CookieSecurePolicy.Always;
        });

        builder.Services.AddRegistroServiziData(builder.Configuration);

        var identityConfig = builder.Configuration.GetSection("Identity");
        builder.Services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = identityConfig.GetValue("RequireConfirmedAccount", false);

            options.Lockout.AllowedForNewUsers = identityConfig.GetValue("AllowedForNewUsers", true);
            options.Lockout.MaxFailedAccessAttempts = identityConfig.GetValue("MaxFailedAccessAttempts", 5);
            options.Lockout.DefaultLockoutTimeSpan = identityConfig.GetValue("DefaultLockoutTimeSpan", TimeSpan.FromMinutes(15));

            options.Password.RequiredLength = identityConfig.GetValue("Password:RequiredLength", 10);
            options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<RegistroServiziDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

        builder.Services.AddRegistroServiziApplication();
        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

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

        //app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        //app.UseHttpsRedirection();

        //app.UseRouting();
        //app.UseAntiforgery();

        //app.MapStaticAssets();
        //app.MapRazorComponents<App>()
        //    .AddInteractiveServerRenderMode();

        //// Add additional endpoints required by the Identity /Account Razor components.
        //app.MapAdditionalIdentityEndpoints();
        //app.Run();

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthentication();

        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapControllers();
        app.MapStaticAssets();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapAdditionalIdentityEndpoints();
        app.Run();
    }
}