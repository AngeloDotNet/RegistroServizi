namespace RegistroServizi.Application;

/// <summary>
/// Extension methods for registering application services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers application services and validators in the dependency injection container.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public IServiceCollection AddRegistroServiziApplication()
        {
            services.AddScoped<IApplicazioneService, ApplicazioneService>();
            services.AddScoped<IPrezzoServizioService, PrezzoServizioService>();

            services.AddSingleton(TimeProvider.System);
            services.AddSingleton<ClientTimeProvider>();
            services.AddSingleton<ITimeZoneService, TimeZoneService>();

            //services.AddValidatorsFromAssemblyContaining<CreatePrezzoServizioValidator>();

            return services;
        }
    }
}