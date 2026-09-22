namespace RegistroServizi.Application;

/// <summary>
/// Provides extension methods and helper functions for registering application services and performing parameter
/// validations with the dependency injection container.
/// </summary>
/// <remarks>Registers types whose names end with 'Service' from the application's assembly as implemented
/// interfaces with scoped lifetime and adds TimeProvider.System as a singleton. Also exposes internal validation
/// helpers that throw ArgumentException or ArgumentOutOfRangeException for invalid inputs.</remarks>
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

    /// <summary>
    /// Validates that a string is not null, empty, or consists only of white-space characters.
    /// </summary>
    /// <remarks>Uses ArgumentException for null, empty, or white-space values; supply a specific message and
    /// parameter name for clearer diagnostics.</remarks>
    /// <param name="value">The string to validate.</param>
    /// <param name="message">The message to include in the ArgumentException if validation fails.</param>
    /// <param name="paramName">The name of the parameter to include in the ArgumentException.</param>
    /// <exception cref="ArgumentException">Thrown when value is null, empty, or consists only of white-space characters.</exception>
    internal static void ValidateNotNullOrWhiteSpace(string? value, string message, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Validates that the specified GUID is not Guid.Empty.
    /// </summary>
    /// <param name="value">The GUID to validate.</param>
    /// <param name="message">The error message for the ArgumentException.</param>
    /// <param name="paramName">The name of the parameter to associate with the ArgumentException.</param>
    /// <exception cref="ArgumentException">Thrown when the specified GUID equals Guid.Empty.</exception>
    internal static void ValidateGuidNotEmpty(Guid value, string message, string paramName)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Validates that value is null or greater than or equal to zero. Throws ArgumentOutOfRangeException when value is
    /// less than zero.
    /// </summary>
    /// <remarks>Null values are considered valid.</remarks>
    /// <param name="value">Nullable integer to validate. Null is permitted; values must be >= 0.</param>
    /// <param name="message">Error message to include in the thrown exception.</param>
    /// <param name="paramName">Name of the parameter to include in the thrown exception.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when value is less than zero. The exception includes the parameter name, the actual value, and the
    /// provided message.</exception>
    internal static void ValidateIntegerGreaterOrEqualThanZero(int? value, string message, string paramName)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, message);
        }
    }
}