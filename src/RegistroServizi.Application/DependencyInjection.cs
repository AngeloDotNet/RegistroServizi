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
            services.Scan(scan => scan
                .FromAssemblyOf<ApplicazioneService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddSingleton(TimeProvider.System);

            return services;
        }
    }

    /// <summary>
    /// Validates that a string is not null, empty, or whitespace. Throws an ArgumentException if the validation fails.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="message"></param>
    /// <param name="paramName"></param>
    /// <exception cref="ArgumentException"></exception>
    internal static void ValidateNotNullOrWhiteSpace(string? value, string message, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Validates that a GUID is not empty. Throws an ArgumentException if the validation fails.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="message"></param>
    /// <param name="paramName"></param>
    /// <exception cref="ArgumentException"></exception>
    internal static void ValidateGuidNotEmpty(Guid value, string message, string paramName)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Validates that an integer is greater than zero. Throws an ArgumentOutOfRangeException if the validation fails.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="message"></param>
    /// <param name="paramName"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    internal static void ValidateIntegerGreaterOrEqualThanZero(int? value, string message, string paramName)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, message);
        }
    }
}