using MudBlazor;

namespace RegistroServizi.Web.Extensions;

/// <summary>
/// Provides helper methods for opening MudBlazor item dialogs and interpreting their results.
/// </summary>
/// <remarks>
/// Dialog configuration is delegated to <see cref="MudDialogParameters" /> so that create,
/// edit, and delete operations use consistent parameters and presentation options.
/// </remarks>
public static class MudDialogExtensions
{
    //public static async Task ExecuteItemDialogAsync<TDialog>(this IDialogService dialogService, ISnackbar snackbar, IStringLocalizer localizer, Func<Task> reloadAsync, Func<Action, Task> invokeAsync,
    //    ItemActionType actionType, string title, Guid id, string description, string confirmText, string cancelText, string cancellationMessageKey) where TDialog : IComponent
    //{
    //    ArgumentNullException.ThrowIfNull(dialogService);
    //    ArgumentNullException.ThrowIfNull(snackbar);
    //    ArgumentNullException.ThrowIfNull(localizer);
    //    ArgumentNullException.ThrowIfNull(reloadAsync);
    //    ArgumentNullException.ThrowIfNull(invokeAsync);

    //    var result = await GenerateDialogItemAsync<TDialog>(dialogService, actionType, title, id, description, cancelText, confirmText);

    //    if (result)
    //    {
    //        await reloadAsync();
    //    }
    //    else
    //    {
    //        await invokeAsync(() => snackbar.ShowNotifyInfo(localizer[cancellationMessageKey]));
    //    }
    //}

    /// <summary>
    /// Opens an item dialog for the requested action and reports whether the dialog completed successfully.
    /// </summary>
    /// <typeparam name="TDialog">The Blazor component type used to render the dialog.</typeparam>
    /// <param name="dialogService">The MudBlazor service used to display the dialog.</param>
    /// <param name="snackbar">The snackbar service available to the calling workflow.</param>
    /// <param name="localizer">The localizer available to the calling workflow.</param>
    /// <param name="loadingDataAsync">The asynchronous operation available for refreshing data after the dialog completes.</param>
    /// <param name="invokeAsync">The dispatcher used to invoke UI work from the calling workflow.</param>
    /// <param name="actionType">The item operation represented by the dialog.</param>
    /// <param name="title">The title displayed by the dialog.</param>
    /// <param name="id">The identifier of the item being edited or deleted.</param>
    /// <param name="description">The explanatory text displayed by a delete dialog.</param>
    /// <param name="confirmText">The text displayed on the confirmation button.</param>
    /// <param name="cancelText">The text displayed on the cancel button.</param>
    /// <param name="warningText">The warning text associated with the requested operation.</param>
    /// <param name="deleteAction">
    /// The asynchronous operation executed by the delete dialog when the user confirms deletion;
    /// or <see langword="null" /> when no delete operation is required.
    /// </param>
    /// <returns>
    /// A task whose result is <see langword="true" /> when the dialog completes without cancellation;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public static async Task<bool> ExecuteItemDialogAsync<TDialog>(IDialogService dialogService, ISnackbar snackbar, IStringLocalizer localizer, Func<Task> loadingDataAsync,
        Func<Func<Task>, Task> invokeAsync, ItemActionType actionType, string title, Guid id, string description, string confirmText, string cancelText, string warningText,
        Func<Task>? deleteAction = null) where TDialog : IComponent
    {
        return await GenerateDialogItemAsync<TDialog>(dialogService, actionType, title, id, description, cancelText, confirmText, deleteAction);
    }

    /// <summary>
    /// Creates the parameters for the requested item action and opens the corresponding dialog.
    /// </summary>
    /// <typeparam name="TDialog">The Blazor component type used to render the dialog.</typeparam>
    /// <param name="dialogService">The MudBlazor service used to display the dialog.</param>
    /// <param name="actionType">The item operation represented by the dialog.</param>
    /// <param name="title">The title displayed by the dialog.</param>
    /// <param name="id">The identifier of the item being edited or deleted.</param>
    /// <param name="description">The explanatory text displayed by a delete dialog.</param>
    /// <param name="cancelText">The text displayed on the cancel button.</param>
    /// <param name="confirmText">The text displayed on the confirmation button.</param>
    /// <param name="deleteAction">
    /// The asynchronous operation executed by the delete dialog when the user confirms deletion;
    /// or <see langword="null" /> when no delete operation is available.
    /// </param>
    /// <returns>
    /// A task whose result is <see langword="true" /> when a supported dialog completes without cancellation;
    /// otherwise, <see langword="false" />.
    /// </returns>
    private static async Task<bool> GenerateDialogItemAsync<TDialog>(IDialogService dialogService, ItemActionType actionType, string title, Guid id, string description, string cancelText,
        string confirmText, Func<Task>? deleteAction = null) where TDialog : IComponent
    {
        return actionType switch
        {
            ItemActionType.Create => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudDialogParameters.GetCreateItemDialogParameters<TDialog>(title, cancelText)),
            ItemActionType.Edit => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudDialogParameters.GetEditItemDialogParameters<TDialog>(id, title, cancelText)),
            ItemActionType.Delete when deleteAction is not null => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudDialogParameters.GetDeleteItemDialogParameters(title, description, cancelText, confirmText, deleteAction)),
            _ => false
        };
    }

    /// <summary>
    /// Displays a dialog with the supplied parameters and converts its result to a completion status.
    /// </summary>
    /// <typeparam name="TDialog">The Blazor component type used to render the dialog.</typeparam>
    /// <param name="dialogService">The MudBlazor service used to display the dialog.</param>
    /// <param name="parametersFactory">The factory that creates the dialog parameters.</param>
    /// <returns>
    /// A task whose result is <see langword="true" /> when the dialog returns a non-canceled result;
    /// otherwise, <see langword="false" />.
    /// </returns>
    private static async Task<bool> OpenDialogAndRefreshAsync<TDialog>(IDialogService dialogService, Func<DialogParameters> parametersFactory) where TDialog : IComponent
    {
        var dialogReference = await dialogService.ShowAsync<TDialog>(parametersFactory(), MudDialogOptions.GetSmallDialogOptions());
        var result = await dialogReference.Result;

        if (result is not null && !result.Canceled)
        {
            return true;
        }

        return false;
    }
}