namespace RegistroServizi.Application;

/// <summary>
/// Provides reusable validation helpers for arguments used by the application layer.
/// </summary>
/// <remarks>
/// The helpers centralize common precondition checks so application services apply
/// consistent validation rules and exception types before processing input.
/// These methods do not modify the values being validated.
/// </remarks>
internal static class ObjectValidation
{
    /// <summary>
    /// Verifies that a string contains a meaningful value.
    /// </summary>
    /// <param name="value">The string value to validate.</param>
    /// <param name="message">The message included in the exception when validation fails.</param>
    /// <param name="paramName">The name of the parameter associated with the exception.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> is <see langword="null"/>, empty, or contains only whitespace.
    /// </exception>
    /// <remarks>
    /// Use this validation when downstream application logic requires a non-blank
    /// identifier, name, or other textual value.
    /// </remarks>
    internal static void ValidateNotNullOrWhiteSpace(string? value, string message, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Verifies that a <see cref="Guid"/> contains a non-empty identifier.
    /// </summary>
    /// <param name="value">The GUID value to validate.</param>
    /// <param name="message">The message included in the exception when validation fails.</param>
    /// <param name="paramName">The name of the parameter associated with the exception.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> is <see cref="Guid.Empty"/>.
    /// </exception>
    /// <remarks>
    /// Use this validation when an application-layer operation requires an existing
    /// entity identifier rather than the default GUID value.
    /// </remarks>
    internal static void ValidateGuidNotEmpty(Guid value, string message, string paramName)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(message, paramName);
        }
    }

    /// <summary>
    /// Verifies that a nullable integer is either absent or non-negative.
    /// </summary>
    /// <param name="value">The nullable integer value to validate.</param>
    /// <param name="message">The message included in the exception when validation fails.</param>
    /// <param name="paramName">The name of the parameter associated with the exception.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="value"/> is less than zero.
    /// </exception>
    /// <remarks>
    /// A <see langword="null"/> value is valid because it represents an omitted
    /// optional value. When a value is provided, it must be zero or greater.
    /// </remarks>
    internal static void ValidateIntegerGreaterOrEqualThanZero(int? value, string message, string paramName)
    {
        if (value.HasValue && value.Value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value.Value, message);
        }
    }
}