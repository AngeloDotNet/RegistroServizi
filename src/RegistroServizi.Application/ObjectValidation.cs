namespace RegistroServizi.Application;

/// <summary>
/// Provides internal helper methods for validating common argument values.
/// </summary>
/// <remarks>
/// These helpers are intended for use within the application layer only and throw
/// <see cref="ArgumentException"/> or <see cref="ArgumentOutOfRangeException"/> when validation fails.
/// </remarks>
internal static class ObjectValidation
{
    /// <summary>
    /// Ensures that a string value is not <see langword="null"/>, empty, or whitespace-only.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="message">The exception message to use when validation fails.</param>
    /// <param name="paramName">The parameter name associated with the exception.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> is <see langword="null"/>, empty, or contains only whitespace.
    /// </exception>
    /// <remarks>
    /// Use this method when a meaningful, non-blank string value is required.
    /// </remarks>
    internal static void ValidateNotNullOrWhiteSpace(string? value, string message, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Ensures that a <see cref="Guid"/> value is not <see cref="Guid.Empty"/>.
    /// </summary>
    /// <param name="value">The GUID value to validate.</param>
    /// <param name="message">The exception message to use when validation fails.</param>
    /// <param name="paramName">The parameter name associated with the exception.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> equals <see cref="Guid.Empty"/>.
    /// </exception>
    internal static void ValidateGuidNotEmpty(Guid value, string message, string paramName)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Ensures that a nullable integer value is either <see langword="null"/> or greater than or equal to zero.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="message">The exception message to use when validation fails.</param>
    /// <param name="paramName">The parameter name associated with the exception.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="value"/> is less than zero.
    /// </exception>
    /// <remarks>
    /// A <see langword="null"/> value is considered valid.
    /// </remarks>
    internal static void ValidateIntegerGreaterOrEqualThanZero(int? value, string message, string paramName)
    {
        if (value.HasValue && value.Value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value.Value, message);
        }
    }
}