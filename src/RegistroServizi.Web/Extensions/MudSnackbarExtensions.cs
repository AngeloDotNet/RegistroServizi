using MudBlazor;

namespace RegistroServizi.Web.Extensions;

/// <summary>
/// Provides semantic wrappers around <see cref="ISnackbar.Add(string, Severity)" /> for
/// displaying application notifications with consistent severity levels.
/// </summary>
/// <remarks>
/// These extensions centralize snackbar usage in Blazor UI code, making notification intent
/// explicit while preserving the transient behavior provided by MudBlazor.
/// See the <see href="https://mudblazor.com/components/snackbar">MudBlazor snackbar documentation</see>
/// for configuration and display behavior.
/// </remarks>
public static class MudSnackbarExtensions
{
    /// <summary>
    /// Displays a success notification to indicate that an operation completed successfully.
    /// </summary>
    /// <param name="snackbar">The snackbar service that displays the notification.</param>
    /// <param name="message">The message describing the successful operation.</param>
    public static void ShowNotifySuccess(this ISnackbar snackbar, string message) => snackbar.Add(message, Severity.Success);

    /// <summary>
    /// Displays a warning notification to indicate that an operation completed with a
    /// condition requiring the user's attention.
    /// </summary>
    /// <param name="snackbar">The snackbar service that displays the notification.</param>
    /// <param name="message">The message describing the warning condition.</param>
    public static void ShowNotifyWarning(this ISnackbar snackbar, string message) => snackbar.Add(message, Severity.Warning);

    /// <summary>
    /// Displays an error notification to indicate that an operation failed or could not be completed.
    /// </summary>
    /// <param name="snackbar">The snackbar service that displays the notification.</param>
    /// <param name="message">The message describing the error.</param>
    public static void ShowNotifyError(this ISnackbar snackbar, string message) => snackbar.Add(message, Severity.Error);

    /// <summary>
    /// Displays an informational notification that does not indicate success, warning, or failure.
    /// </summary>
    /// <param name="snackbar">The snackbar service that displays the notification.</param>
    /// <param name="message">The informational message to display.</param>
    public static void ShowNotifyInfo(this ISnackbar snackbar, string message) => snackbar.Add(message, Severity.Info);
}