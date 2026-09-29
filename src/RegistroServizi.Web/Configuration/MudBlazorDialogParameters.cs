using MudBlazor;

namespace RegistroServizi.Web.Configuration;

/// <summary>
/// Factory methods that create preconfigured DialogParameters for common MudBlazor dialog scenarios 
/// (confirmation, create, edit, delete).
/// </summary>
/// <remarks>
/// These methods set localized button labels, default button colors, and populate common dialog fields such as Title, Description, and EditDialogId.
/// The confirmation factory uses Italian labels for cancel and confirm buttons.
/// </remarks>
internal static class MudBlazorDialogParameters
{
    /// <summary>
    /// Creates dialog parameters for a CREATE dialog.
    /// </summary>
    /// <typeparam name="TParam">The type of the dialog parameter model.</typeparam>
    /// <param name="title">The title displayed in the dialog.</param>
    /// <param name="cancelText">The text displayed on the cancel button.</param>
    /// <returns>A DialogParameters instance configured with Title, custom button texts, and primary button coloring (Confirm=Success).</returns>
    internal static DialogParameters<TParam> GetCreateItemDialogParameters<TParam>(string title, string cancelText)
        => new DialogParameters<TParam>
        {
            { "EditDialogId", null },
            { "Title", title },
            { "BtnCancelText", cancelText },
            { "BtnCancelColor", Color.Default },
            { "BtnConfirmColor", Color.Success }
        };

    /// <summary>
    /// Creates dialog parameters for an EDIT dialog.
    /// </summary>
    /// <typeparam name="TParam">Payload type attached to the dialog parameters.</typeparam>
    /// <param name="editDialogId">Edit dialog identifier.</param>
    /// <param name="title">Dialog title.</param>
    /// <param name="cancelText">Cancel button text.</param>
    /// <returns>A DialogParameters instance configured with EditDialogId, Title, custom button texts, and default button colors (Cancel=Default, Confirm=Warning).</returns>
    internal static DialogParameters<TParam> GetEditItemDialogParameters<TParam>(Guid editDialogId, string title, string cancelText)
        => new DialogParameters<TParam>
        {
            { "EditDialogId", editDialogId },
            { "Title", title },
            { "BtnCancelText", cancelText },
            { "BtnCancelColor", Color.Default },
            { "BtnConfirmColor", Color.Warning }
        };

    /// <summary>
    /// Creates dialog parameters for a DELETE confirmation dialog.
    /// </summary>
    /// <typeparam name="TParam">The type of the dialog parameter payload.</typeparam>
    /// <param name="editDialogId">Edit dialog identifier.</param>
    /// <param name="title">The dialog title text.</param>
    /// <param name="description">The dialog description text.</param>
    /// <param name="cancelText">Text for the cancel button.</param>
    /// <param name="confirmText">Text for the confirm button.</param>
    /// <returns>A DialogParameters instance configured with all necessary texts and colors (Cancel=Default, Confirm=Error).</returns>
    internal static DialogParameters<TParam> GetDeleteItemDialogParameters<TParam>(Guid editDialogId, string title, string description, string cancelText, string confirmText)
        => new DialogParameters<TParam>
        {
            { "EditDialogId", editDialogId },
            { "Title", title },
            { "Description", description },
            { "BtnCancelText", cancelText },
            { "BtnCancelColor", Color.Default },
            { "BtnConfirmText", confirmText },
            { "BtnConfirmColor", Color.Error }
        };
}