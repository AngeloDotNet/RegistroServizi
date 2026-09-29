using MudBlazor;

namespace RegistroServizi.Web.Configuration;

/// <summary>Provides centralized, preconfigured DialogOptions used by the library for modal and backdrop-filter dialogs.</summary>
/// <remarks>Internal utility class exposing presets that enforce consistent dialog behavior: a non-dismissible centered modal
/// (escape and backdrop clicks disabled) and a centered backdrop-filter dialog with a blurred background. Intended for internal use only.</remarks>
internal static class MudBlazorDialogOptions
{
    /// <summary>Gets a DialogOptions instance configured for modal dialogs with escape and backdrop closing disabled and centered positioning.</summary>
    /// <returns>A DialogOptions configured with CloseOnEscapeKey = false, BackdropClick = false, and Position = DialogPosition.Center.</returns>
    internal static DialogOptions GetDefaultDialogOptions() => new DialogOptions
    {
        CloseOnEscapeKey = false,
        BackdropClick = false,
        Position = DialogPosition.Center
    };

    /// <summary>Gets a DialogOptions configured for a small, centered dialog with a blurry background and disabled close interactions.</summary>
    /// <remarks>Intended for small modal dialogs that should not close on Escape or backdrop clicks.</remarks>
    /// <returns>A DialogOptions configured with MaxWidth.Small, BackgroundClass = "blurry-dialog", CloseOnEscapeKey = false, BackdropClick = false
    /// and Position = DialogPosition.Center.</returns>
    internal static DialogOptions GetSmallDialogOptions() => new DialogOptions
    {
        MaxWidth = MaxWidth.Small,
        BackgroundClass = "blurry-dialog",
        CloseOnEscapeKey = false,
        BackdropClick = false,
        Position = DialogPosition.Center
    };
}