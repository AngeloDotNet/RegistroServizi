namespace RegistroServizi.Application;

/// <summary>
/// Provides dependency injection registration helpers for the application layer.
/// </summary>
/// <remarks>
/// This type exposes extension members that register application services by convention and
/// add shared framework services required by the application.
/// </remarks>
public static class DependencyInjection
{
    /// <summary>
    /// Registers application services and shared infrastructure services in the specified
    /// <see cref="IServiceCollection" />.
    /// </summary>
    /// <remarks>
    /// Discovers classes in the assembly containing <c>OspedaleService</c> whose names end with
    /// <c>Service</c>, registers them against their implemented interfaces, and configures them
    /// with a scoped lifetime.
    /// <para>
    /// Also registers <see cref="TimeProvider.System" /> as a singleton so application services can
    /// depend on an abstracted time source.
    /// </para>
    /// </remarks>
    /// <param name="services">
    /// The service collection to configure.
    /// </param>
    /// <returns>
    /// The configured <see cref="IServiceCollection" /> instance.
    /// </returns>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds the application-layer service registrations to the service collection.
        /// </summary>
        /// <remarks>
        /// This method applies convention-based registration for application services and registers
        /// the system time provider for use throughout the application.
        /// </remarks>
        /// <returns>
        /// The configured <see cref="IServiceCollection" /> instance.
        /// </returns>
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