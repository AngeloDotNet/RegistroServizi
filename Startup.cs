using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
            services.AddMvc(options => 
            {
                options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            
            #if DEBUG
            .AddRazorRuntimeCompilation()
            #endif
            ;

            //Services - Generics
            services.AddTransient<IApplicationPersister, ApplicationPersister>();
            services.AddSingleton<IErrorViewSelectorService, ErrorViewSelectorService>();

            //Services - Area Impostazioni
            services.AddTransient<IAssociazioniService, EfCoreAssociazioniService>();
            services.AddTransient<ICostiServiziService, EfCoreCostiServiziService>();

            //Services - Area Amministrazione
            services.AddTransient<IClientiService, EfCoreClientiService>();

            //Database
            services.AddDbContextPool<RegistroServiziDbContext>(optionsBuilder => {
                string connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
                optionsBuilder.UseSqlite(connectionString);
            });

            //Options - Generics
            services.Configure<ApplicationOptions>(Configuration.GetSection("Applicazione"));
            
            //Options - Area Impostazioni
            services.Configure<CostoServizioOptions>(Configuration.GetSection("CostoServizio"));
            
            //Options - Area Amministrazione
            services.Configure<ClienteOptions>(Configuration.GetSection("Cliente"));
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

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(routeBuilder => {
                routeBuilder.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routeBuilder.MapFallbackToController("{*path}", "Index", "Error");
            });

        }
    }
}
