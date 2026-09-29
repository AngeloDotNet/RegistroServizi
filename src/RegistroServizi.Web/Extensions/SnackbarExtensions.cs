using MudBlazor;

namespace RegistroServizi.Web.Extensions;

/// <summary>
/// Provides extension methods for ISnackbar to display predefined notifications with Success, Warning, and Error severities.
/// </summary>
/// <remarks>Convenience wrappers that call ISnackbar.Add(message, Severity.*) to enqueue transient notifications.
/// Intended for use in UI code to show brief status messages.</remarks>
public static class SnackbarExtensions
{
    /// <summary>
    /// Adds a success notification to the specified snackbar with the provided message.
    /// </summary>
    /// <remarks>Extension method for ISnackbar that invokes Add with Severity.Success.</remarks>
    /// <param name="snackbar">The snackbar to which the notification is added.</param>
    /// <param name="message">The message text to display in the notification.</param>
    public static void ShowNotifySuccess(this ISnackbar snackbar, string message) => snackbar.Add(message, Severity.Success);

    /// <summary>
    /// Displays a warning snackbar with the specified message.
    /// </summary>
    /// <remarks>Adds the message to the snackbar using Warning severity.</remarks>
    /// <param name="snackbar">The ISnackbar instance used to display the notification.</param>
    /// <param name="message">The message text to display in the snackbar.</param>
    public static void ShowNotifyWarning(this ISnackbar snackbar, string message) => snackbar.Add(message, Severity.Warning);

    /// <summary>
    /// Displays an error notification using the specified snackbar.
    /// </summary>
    /// <remarks>Adds the message to the snackbar with Severity.Error.</remarks>
    /// <param name="snackbar">The snackbar used to show the notification.</param>
    /// <param name="message">The error message to display.</param>
    public static void ShowNotifyError(this ISnackbar snackbar, string message) => snackbar.Add(message, Severity.Error);

    /// <summary>
    /// Displays an informational snackbar notification.
    /// </summary>
    /// <remarks>Adds the provided message with informational severity (Severity.Info).</remarks>
    /// <param name="snackbar">The snackbar instance that displays the notification.</param>
    /// <param name="message">The message text to display.</param>
    public static void ShowNotifyInfo(this ISnackbar snackbar, string message) => snackbar.Add(message, Severity.Info);
}