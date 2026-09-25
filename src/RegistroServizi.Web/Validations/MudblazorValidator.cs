using MudBlazor;

namespace RegistroServizi.Web.Validations;

/// <summary>
/// Provides internal static helper methods for validating values and adding warning notifications via ISnackbar.
/// </summary>
/// <remarks>Includes helpers to validate non-null or non-whitespace strings, check for non-negative numeric
/// values, attempt conversion to nullable decimal, and determine negativity. Defines minCap and maxCap constants for a
/// postal-code range. Intended for internal use only.</remarks>
internal static class MudblazorValidator
{
    /// <summary>
    /// Adds a warning to the provided snackbar when the given value represents a negative number, using the specific
    /// validation message if available or the supplied defaultMessage.
    /// </summary>
    /// <remarks>No action is taken if the value cannot be converted to a decimal or is non-negative. When a
    /// negative value is detected, the message from IsNegative is used if present; otherwise defaultMessage is added
    /// with Severity.Warning.</remarks>
    /// <param name="snackbar">Snackbar used to display the warning message.</param>
    /// <param name="value">Value to evaluate for negativity; conversion to decimal is attempted.</param>
    /// <param name="defaultMessage">Fallback message used when no specific negative-value message is produced.</param>
    internal static void ValidateNonNegative(ISnackbar snackbar, object? value, string defaultMessage)
    {
        if (TryConvertToDecimalNullable(value, out var dec) && IsNegative(dec, out var message))
        {
            snackbar.Add(message ?? defaultMessage, Severity.Warning);
        }
    }

    /// <summary>
    /// Attempts to convert the specified object to a nullable decimal.
    /// </summary>
    /// <remarks>Double values are cast to decimal and may lose precision. Null or unsupported types cause
    /// conversion to fail and leave result as null.</remarks>
    /// <param name="value">The object to convert; supported types: decimal, int, long, and double.</param>
    /// <param name="result">When successful, contains the converted decimal; otherwise null.</param>
    /// <returns>True if the object was converted to a decimal; otherwise false.</returns>
    internal static bool TryConvertToDecimalNullable(object? value, out decimal? result)
    {
        result = null;

        if (value == null)
        {
            return false;
        }

        switch (value)
        {
            case decimal d:
                result = d;
                return true;

            case int i:
                result = i;
                return true;

            case long l:
                result = l;
                return true;

            case double dd:
                result = (decimal)dd;
                return true;

            default:
                return false;
        }
    }

    /// <summary>
    /// Determines whether the specified nullable decimal is negative.
    /// </summary>
    /// <remarks>Sets message to null in the current implementation.</remarks>
    /// <param name="value">The nullable decimal to evaluate.</param>
    /// <param name="message">When the method returns, contains a diagnostic message if applicable; otherwise null.</param>
    /// <returns>True if value has a value less than zero; otherwise false.</returns>
    internal static bool IsNegative(decimal? value, out string? message)
    {
        message = null;

        if (value.HasValue && value.Value < 0)
        {
            return true;
        }

        return false;
    }
}