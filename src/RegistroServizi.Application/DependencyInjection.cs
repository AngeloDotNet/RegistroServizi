namespace RegistroServizi.Application;

/// <summary>
/// Provides dependency injection registration helpers for the application layer.
/// </summary>
/// <remarks>
/// The registration convention keeps the composition root independent of individual application
/// services. Any class in the application assembly whose name ends with <c>Service</c> is registered
/// through its implemented interfaces with a scoped lifetime.
/// </remarks>
public static class DependencyInjection
{
    /// <summary>
    /// Registers the application-layer services and shared application dependencies.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Service classes are discovered from the assembly containing <see cref="OspedaleService" />.
    /// Classes whose names end with <c>Service</c> are registered against their implemented
    /// interfaces using a scoped lifetime.
    /// </para>
    /// <para>
    /// The system <see cref="TimeProvider" /> is also registered as a singleton. Application
    /// services can depend on the abstraction instead of accessing the system clock directly,
    /// which makes time-dependent behavior easier to test.
    /// </para>
    /// <para>
    /// Call this method while configuring the application's service collection:
    /// </para>
    /// <code>
    /// builder.Services.AddRegistroServiziApplication();
    /// </code>
    /// <para>
    /// For more information about dependency injection, see
    /// <see href="https://learn.microsoft.com/dotnet/core/extensions/dependency-injection" />
    /// and for service scanning, see
    /// <see href="https://github.com/khellang/Scrutor" />.
    /// </para>
    /// </remarks>
    /// <param name="services">
    /// The service collection to which the application-layer registrations are added.
    /// </param>
    /// <returns>
    /// The same <see cref="IServiceCollection" /> instance, after the registrations have been added,
    /// allowing additional service registrations to be chained.
    /// </returns>
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds all conventionally registered application services to the service collection.
        /// </summary>
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