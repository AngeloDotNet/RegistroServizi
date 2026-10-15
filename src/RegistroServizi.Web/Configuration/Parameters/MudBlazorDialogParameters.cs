using MudBlazor;

namespace RegistroServizi.Web.Configuration.Parameters;

/// <summary>
/// Provides factory methods for creating consistently configured
/// <see cref="DialogParameters{TParam}" /> instances used by MudBlazor dialogs.
/// </summary>
/// <remarks>
/// Centralizing these defaults keeps dialog behavior consistent across the application.
/// Each factory populates the parameter keys consumed by the corresponding dialog component,
/// including the title, button text, button colors, and optional edit identifier.
/// </remarks>
internal static class MudBlazorDialogParameters
{
    /// <summary>
    /// Creates the parameters required to display a dialog for creating a new item.
    /// </summary>
    /// <typeparam name="TParam">The type of the payload associated with the dialog.</typeparam>
    /// <param name="title">The title displayed by the dialog.</param>
    /// <param name="cancelText">The text displayed on the cancel button.</param>
    /// <returns>
    /// A <see cref="DialogParameters{TParam}" /> instance with no edit identifier,
    /// a success-colored confirmation button, and a default-colored cancel button.
    /// </returns>
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
    /// Creates the parameters required to display a dialog for editing an existing item.
    /// </summary>
    /// <typeparam name="TParam">The type of the payload associated with the dialog.</typeparam>
    /// <param name="editDialogId">The identifier of the item or edit operation represented by the dialog.</param>
    /// <param name="title">The title displayed by the dialog.</param>
    /// <param name="cancelText">The text displayed on the cancel button.</param>
    /// <returns>
    /// A <see cref="DialogParameters{TParam}" /> instance containing the edit identifier,
    /// a warning-colored confirmation button, and a default-colored cancel button.
    /// </returns>
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
    /// Creates the parameters required to display a confirmation dialog for deleting an item.
    /// </summary>
    /// <typeparam name="TParam">The type of the payload associated with the dialog.</typeparam>
    /// <param name="editDialogId">The identifier of the item or edit operation represented by the dialog.</param>
    /// <param name="title">The title displayed by the dialog.</param>
    /// <param name="description">The explanatory text displayed in the dialog.</param>
    /// <param name="cancelText">The text displayed on the cancel button.</param>
    /// <param name="confirmText">The text displayed on the confirmation button.</param>
    /// <returns>
    /// A <see cref="DialogParameters{TParam}" /> instance containing the dialog text,
    /// the item identifier, a default-colored cancel button, and an error-colored
    /// confirmation button to emphasize the destructive operation.
    /// </returns>
    internal static DialogParameters<TParam> GetDeleteItemDialogParameters<TParam>(
        Guid editDialogId,
        string title,
        string description,
        string cancelText,
        string confirmText)
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