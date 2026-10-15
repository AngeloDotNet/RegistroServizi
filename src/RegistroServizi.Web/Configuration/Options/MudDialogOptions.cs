using MudBlazor;

namespace RegistroServizi.Web.Configuration.Options;

/// <summary>
/// Provides centralized dialog-option presets for the application's MudBlazor dialogs.
/// </summary>
/// <remarks>
/// The presets keep modal behavior consistent across the application by preventing
/// accidental dismissal and ensuring dialogs are centered. Each method returns a new
/// <see cref="DialogOptions"/> instance so callers can customize the result without
/// modifying options used by another dialog.
/// </remarks>
internal static class MudDialogOptions
{
    /// <summary>
    /// Creates options for a centered modal dialog that requires explicit dismissal.
    /// </summary>
    /// <returns>
    /// A new <see cref="DialogOptions"/> instance with centered positioning and both
    /// Escape-key and backdrop-click dismissal disabled.
    /// </returns>
    internal static DialogOptions GetDefaultDialogOptions() => new()
    {
        CloseOnEscapeKey = false,
        BackdropClick = false,
        Position = DialogPosition.Center
    };

    /// <summary>
    /// Creates options for a small, centered modal dialog with a blurred backdrop.
    /// </summary>
    /// <remarks>
    /// The <c>blurry-dialog</c> CSS class is applied to the dialog, allowing the
    /// application's styles to provide the backdrop-filter appearance. The dialog
    /// requires explicit dismissal and cannot be closed with the Escape key or by
    /// clicking the backdrop.
    /// </remarks>
    /// <returns>
    /// A new <see cref="DialogOptions"/> instance configured with
    /// <see cref="MaxWidth.Small"/>, the <c>blurry-dialog</c> CSS class, centered
    /// positioning, and disabled implicit dismissal.
    /// </returns>
    internal static DialogOptions GetSmallDialogOptions() => new()
    {
        MaxWidth = MaxWidth.Small,
        BackgroundClass = "blurry-dialog",
        CloseOnEscapeKey = false,
        BackdropClick = false,
        Position = DialogPosition.Center
    };
}