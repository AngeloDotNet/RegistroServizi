namespace RegistroServizi.Application;

/// <summary>
/// Provides extension methods and helper functions for registering application services and performing parameter
/// validations with the dependency injection container.
/// </summary>
/// <remarks>Registers types whose names end with 'Service' from the application's assembly as implemented
/// interfaces with scoped lifetime and adds TimeProvider.System as a singleton.</remarks>
public static class DependencyInjection
{
    /// <summary>
    /// Registers application services into the specified IServiceCollection and adds TimeProvider.System.
    /// </summary>
    /// <remarks>Scans the assembly containing OspedaleService and registers classes whose names end with
    /// "Service" as their implemented interfaces with a scoped lifetime. Also registers TimeProvider.System as a
    /// singleton.</remarks>
    /// <param name="services">The IServiceCollection to configure with application services and TimeProvider.System.</param>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers application services into the IServiceCollection and adds the system TimeProvider.
        /// </summary>
        /// <remarks>Scans the assembly containing OspedaleService and registers classes with names ending in 'Service' 
        /// as their implemented interfaces with scoped lifetime. Also registers TimeProvider.System as a singleton.</remarks>
        /// <returns>The IServiceCollection with the registered services.</returns>
        public IServiceCollection AddRegistroServiziApplication()
        {
            services.Scan(scan => scan
                .FromAssemblyOf<OspedaleService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddSingleton(TimeProvider.System);

            return services;
        }
    }
}