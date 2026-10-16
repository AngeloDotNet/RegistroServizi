using MudBlazor;

namespace RegistroServizi.Web.Extensions;

/// <summary>
/// Provides helper methods for displaying item-operation dialogs through MudBlazor.
/// </summary>
/// <remarks>
/// The helper centralizes dialog creation for create, edit, and delete operations and
/// reports whether the displayed dialog completed without being canceled.
/// </remarks>
public static class MudDialogExtensions
{
    public static async Task ExecuteItemDialogAsync<TDialog>(this IDialogService dialogService, ISnackbar snackbar, IStringLocalizer localizer, Func<Task> reloadAsync, Func<Action, Task> invokeAsync,
        ItemActionType actionType, string title, Guid id, string description, string confirmText, string cancelText, string cancellationMessageKey) where TDialog : IComponent
    {
        ArgumentNullException.ThrowIfNull(dialogService);
        ArgumentNullException.ThrowIfNull(snackbar);
        ArgumentNullException.ThrowIfNull(localizer);
        ArgumentNullException.ThrowIfNull(reloadAsync);
        ArgumentNullException.ThrowIfNull(invokeAsync);

        var result = await GenerateDialogItemAsync<TDialog>(dialogService, actionType, title, id, description, cancelText, confirmText);

        if (result)
        {
            await reloadAsync();
        }
        else
        {
            await invokeAsync(() => snackbar.ShowNotifyInfo(localizer[cancellationMessageKey]));
        }
    }

    /// <summary>
    /// Displays the dialog associated with the specified item operation.
    /// </summary>
    /// <typeparam name="TDialog">
    /// The Blazor component used to render the operation dialog.
    /// </typeparam>
    /// <param name="dialogService">
    /// The service used to display the dialog.
    /// </param>
    /// <param name="actionType">
    /// The item operation to perform.
    /// </param>
    /// <param name="title">
    /// The title displayed by the dialog.
    /// </param>
    /// <param name="id">
    /// The identifier of the item being edited or deleted. This value is ignored for create operations.
    /// </param>
    /// <param name="description">
    /// The description displayed when confirming a delete operation.
    /// </param>
    /// <param name="cancelText">
    /// The text displayed on the cancel button.
    /// </param>
    /// <param name="confirmText">
    /// The text displayed on the confirmation button for delete operations.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous dialog operation. The result is <see langword="true" />
    /// when the dialog completes without being canceled; otherwise, <see langword="false" />.
    /// </returns>
    /// <remarks>
    /// Unsupported <paramref name="actionType" /> values do not display a dialog and return
    /// <see langword="false" />.
    /// </remarks>
    private static async Task<bool> GenerateDialogItemAsync<TDialog>(IDialogService dialogService, ItemActionType actionType, string title, Guid id, string description,
        string cancelText, string confirmText) where TDialog : IComponent
    {
        return actionType switch
        {
            ItemActionType.Create => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudDialogParameters.GetCreateItemDialogParameters<TDialog>(title, cancelText)),
            ItemActionType.Edit => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudDialogParameters.GetEditItemDialogParameters<TDialog>(id, title, cancelText)),
            ItemActionType.Delete => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, title, description, cancelText, confirmText)),
            _ => false,
        };
    }

    /// <summary>
    /// Displays a dialog with the specified parameters and evaluates its result.
    /// </summary>
    /// <typeparam name="TDialog">
    /// The Blazor component used to render the dialog.
    /// </typeparam>
    /// <param name="dialogService">
    /// The service used to display the dialog.
    /// </param>
    /// <param name="parametersFactory">
    /// A factory that creates the parameters required by the dialog.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous dialog operation. The result is <see langword="true" />
    /// when the dialog result is available and was not canceled; otherwise, <see langword="false" />.
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