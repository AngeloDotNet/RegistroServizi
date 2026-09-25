using MudBlazor;

namespace RegistroServizi.Web.Configuration;

/// <summary>Factory methods that create preconfigured DialogParameters for common MudBlazor dialog scenarios (confirmation, create, edit, delete).</summary>
/// <remarks>Sets localized button labels and default button colors and populates common dialog fields such as Title, Description, and EditDialogId.
/// The confirmation factory uses Italian labels for cancel and confirm.</remarks>
internal static class MudBlazorDialogParameters
{
    /// <summary>
    /// Creates dialog parameters for a confirmation dialog using the specified message.
    /// </summary>
    /// <remarks>Button labels are set to Italian: 'Annulla' (cancel) and 'Conferma' (confirm).</remarks>
    /// <typeparam name="TParam">The type of the dialog parameter model.</typeparam>
    /// <param name="message">The confirmation message displayed in the dialog.</param>
    /// <returns>A DialogParameters instance containing ContentText, localized button labels, and Color set to Success.</returns>
    //public static DialogParameters GetConfirmDialogParameters<TParam>(string message) => new DialogParameters<TParam>
    //{
    //    { "ContentText", message },
    //    { "BtnCancel", "Annulla" },
    //    { "BtnCancelColor", Color.Default },
    //    { "BtnConfirm", "Conferma" },
    //    { "BtnConfirmColor", Color.Primary }
    //};

    /// <summary>
    /// Creates dialog parameters for a create dialog using the specified title and cancel button text.
    /// </summary>
    /// <typeparam name="TParam">The type of the dialog parameter model.</typeparam>
    /// <param name="title">The title displayed in the dialog.</param>
    /// <param name="cancelText">The text displayed on the cancel button.</param>
    /// <returns>A DialogParameters instance containing EditDialogId, Title, localized button labels, and Color set to Primary.</returns>
    internal static DialogParameters GetCreateItemDialogParameters<TParam>(string title, string cancelText) => new DialogParameters<TParam>
    {
        { "EditDialogId", null },
        { "Title", title },
        { "BtnCancelText", cancelText },
        { "BtnCancelColor", Color.Default },
        { "BtnConfirmColor", Color.Success }
    };

    /// <summary>
    /// Creates dialog parameters for an edit dialog populated with the specified dialog id, title, cancel text, and default button colors.
    /// </summary>
    /// <remarks>BtnCancelColor is set to Color.Default and BtnConfirmColor is set to Color.Primary.</remarks>
    /// <typeparam name="TParam">Payload type attached to the dialog parameters.</typeparam>
    /// <param name="editDialogId">Edit dialog identifier.</param>
    /// <param name="title">Dialog title.</param>
    /// <param name="cancelText">Cancel button text.</param>
    /// <returns>A DialogParameters instance populated with the provided values and default button colors (BtnCancelColor =
    /// Color.Default, BtnConfirmColor = Color.Primary).</returns>
    internal static DialogParameters GetEditItemDialogParameters<TParam>(Guid editDialogId, string title, string cancelText) => new DialogParameters<TParam>
    {
        { "EditDialogId", editDialogId },
        { "Title", title },
        { "BtnCancelText", cancelText },
        { "BtnCancelColor", Color.Default },
        { "BtnConfirmColor", Color.Warning }
    };

    /// <summary>
    /// Creates dialog parameters for a delete confirmation dialog.
    /// </summary>
    /// <remarks>Cancel button color is set to Color.Default; confirm button color is set to Color.Error.</remarks>
    /// <typeparam name="TParam">The type of the dialog parameter payload.</typeparam>
    /// <param name="title">The dialog title text.</param>
    /// <param name="description">The dialog description text.</param>
    /// <param name="cancelText">Text for the cancel button.</param>
    /// <param name="confirmText">Text for the confirm button.</param>
    /// <returns>A DialogParameters instance configured with title, description, cancel and confirm texts, and button colors.</returns>
    internal static DialogParameters GetDeleteItemDialogParameters<TParam>(string title, string description, string cancelText, string confirmText) => new DialogParameters<TParam>
    {
        { "Title", title },
        { "Description", description },
        { "BtnCancelText", cancelText },
        { "BtnCancelColor", Color.Default },
        { "BtnConfirmText", confirmText },
        { "BtnConfirmColor", Color.Error }
    };
}