using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RegistroServizi.Models.Options;
using RegistroServizi.Models.Services.Application;
using RegistroServizi.Models.Services.Application.CostiServizi;
using RegistroServizi.Models.Services.Application.Associazioni;
using RegistroServizi.Models.Services.Infrastructure;
using RegistroServizi.Customizations.ModelBinders;
using RegistroServizi.Models.Services.Application.Clienti;
using RegistroServizi.Models.Services.Application.Soci;
using RegistroServizi.Models.Services.Application.SociFamiliari;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using RegistroServizi.Models.Services.Application.Ospedali;
using RegistroServizi.Models.Services.Application.SociRinnovi;
using RegistroServizi.Models.Services.Application.Missioni;
using Microsoft.AspNetCore.Http;
using RegistroServizi.Models.Entities;
using RegistroServizi.Customizations.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace RegistroServizi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });
            services.AddCookiePolicy(options =>
            {
                options.CheckConsentNeeded = (context) => false;
            });

            services.AddRazorPages();
            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
            });

            //Services - Generics
            services.AddTransient<IApplicationPersister, ApplicationPersister>();
            services.AddSingleton<IErrorViewSelectorService, ErrorViewSelectorService>();

            //Services - Area Impostazioni
            services.AddTransient<IAssociazioniService, EfCoreAssociazioniService>();
            services.AddTransient<ICostiServiziService, EfCoreCostiServiziService>();

            //Services - Area Amministrazione
            services.AddTransient<IClientiService, EfCoreClientiService>();
            services.AddTransient<ISociService, EfCoreSociService>();
            services.AddTransient<ISociFamiliariService, EfCoreSociFamiliariService>();
            services.AddTransient<ISociRinnoviService, EfCoreSociRinnoviService>();
            services.AddTransient<IMissioniService, InMemoryMissioniService>();
            services.AddScoped<IPessimisticLock, MemoryCachePessimisticLock>();

            //Services - Area Opzioni
            services.AddTransient<IOspedaliService, EfCoreOspedaliService>();

            //Services - ASP.NET Core Identity
            services.AddDefaultIdentity<ApplicationUser>(options => {

                // Criteri di validazione della password
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 4;

                // Conferma dell'account
                options.SignIn.RequireConfirmedAccount = true;

                // Blocco dell'account
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                })
                .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>()
                .AddPasswordValidator<CommonPasswordValidator<ApplicationUser>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //Database
            services.AddDbContextPool<RegistroServiziDbContext>(optionsBuilder => {
                string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                optionsBuilder.UseSqlite(connectionString);
            });
            services.AddDbContextPool<ApplicationDbContext>(optionsBuilder => {
                string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Application");
                optionsBuilder.UseSqlite(connectionString);
            });

            services.AddSingleton<IEmailSender, MailKitEmailSender>();
            services.AddSingleton<IEmailClient, MailKitEmailSender>();

            //Options - Generics
            services.Configure<ApplicationOptions>(Configuration.GetSection("Applicazione"));
            services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));
            services.Configure<UsersOptions>(Configuration.GetSection("Users"));

            //Options - Area Impostazioni
            services.Configure<CostoServizioOptions>(Configuration.GetSection("CostoServizio"));

            //Options - Area Amministrazione
            services.Configure<ClienteOptions>(Configuration.GetSection("Cliente"));
            services.Configure<SocioOptions>(Configuration.GetSection("Socio"));

            //Options - Area Opzioni
            services.Configure<OspedaleOptions>(Configuration.GetSection("Ospedale"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();

                lifetime.ApplicationStarted.Register(() =>
                {
                    string filePath = Path.Combine(env.ContentRootPath, "bin/reload.txt");
                    File.WriteAllText(filePath, DateTime.Now.ToString());
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            CultureInfo appCulture = new("it-IT");

            app.UseStaticFiles();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(appCulture),
                SupportedCultures = new[] { appCulture }
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
            app.UseEndpoints(routeBuilder => {
                    routeBuilder.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    routeBuilder.MapRazorPages();
                    routeBuilder.MapFallbackToController("{*path}", "Index", "Error");
            });
        }
    }
}
