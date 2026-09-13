using RegistroServizi.Application.Services.Common;

namespace RegistroServizi.Application;

/// <summary>
/// Extension methods for registering application services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers application services in the dependency injection container based on the specified cache provider.
        /// </summary>
        /// <param name="cacheProvider"></param>
        /// <returns></returns>
        public IServiceCollection AddRegistroServiziApplication(string cacheProvider)
        {
            if (cacheProvider.Equals("Memory", StringComparison.OrdinalIgnoreCase))
            {
                services.AddMemoryCache();
                services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            }

            services.AddScoped<IApplicazioneService, ApplicazioneService>();
            services.AddScoped<IPrezzoServizioService, PrezzoServizioService>();

            services.AddSingleton(TimeProvider.System);
            services.AddSingleton<ClientTimeProvider>();
            services.AddSingleton<ITimeZoneService, TimeZoneService>();

            return services;
        }
    }
}