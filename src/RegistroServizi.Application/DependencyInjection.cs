namespace RegistroServizi.Application;

/// <summary>
/// Extension methods for registering application services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers application services in the dependency injection container.
        /// </summary>
        /// <returns></returns>
        public IServiceCollection AddRegistroServiziApplication()
        {
            services.AddScoped<IApplicazioneService, ApplicazioneService>();
            services.AddScoped<IPrezzoServizioService, PrezzoServizioService>();

            services.AddSingleton(TimeProvider.System);

            return services;
        }
    }
}