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
    //internal static readonly int minCap = 10000;
    //internal static readonly int maxCap = 99999;

    /// <summary>
    /// Validates that a string is not null, empty, or consists only of white-space; if it is, adds the provided
    /// defaultMessage to the snackbar as a warning.
    /// </summary>
    /// <remarks>No exception is thrown; a warning is added to the snackbar instead.</remarks>
    /// <param name="snackbar">Snackbar used to display a warning when the value is null, empty, or whitespace.</param>
    /// <param name="value">String to validate.</param>
    /// <param name="defaultMessage">Message to display on the snackbar when the value is null, empty, or whitespace.</param>
    //internal static void ValidateIsNotNullOrWhiteSpace(ISnackbar snackbar, string? value, string defaultMessage)
    //{
    //    if (string.IsNullOrWhiteSpace(value))
    //    {
    //        snackbar.Add(defaultMessage, Severity.Warning);
    //    }
    //}

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

    //private const int CapDigits = 5;

    //if (!HasExactDigits(addr.Cap, CapDigits))
    //{
    //    Snackbar.Add($"Il codice avviamento postale deve essere composto da {CapDigits} cifre.", Severity.Warning);
    //    return false;
    //}

    //internal static bool HasExactDigits(int value, int digits)
    //{
    //    if (digits <= 0)
    //    {
    //        return false;
    //    }

    //    if (digits == 1)
    //    {
    //        return value is >= 0 and <= 9;
    //    }

    //    var min = (int)Math.Pow(10, digits - 1);
    //    var max = (int)Math.Pow(10, digits) - 1;

    //    return value >= min && value <= max;
    //}

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