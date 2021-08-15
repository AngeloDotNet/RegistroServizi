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
            });

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

            //Database
            services.AddDbContextPool<RegistroServiziDbContext>(optionsBuilder =>
            {
                string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                optionsBuilder.UseSqlite(connectionString);
            });

            //Options - Generics
            services.Configure<ApplicationOptions>(Configuration.GetSection("Applicazione"));

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

            app.UseSession();
            app.UseEndpoints(routeBuilder =>
            {
                routeBuilder.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routeBuilder.MapFallbackToController("{*path}", "Index", "Error");
            });

        }
    }
}
