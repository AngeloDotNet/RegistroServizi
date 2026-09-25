namespace RegistroServizi.Application;

/// <summary>
/// Provides internal helper methods to validate argument values (strings, GUIDs, and nullable integers).
/// </summary>
/// <remarks>Validation methods throw ArgumentException or ArgumentOutOfRangeException for invalid values;
/// intended for internal use.</remarks>
internal static class ObjectValidation
{
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