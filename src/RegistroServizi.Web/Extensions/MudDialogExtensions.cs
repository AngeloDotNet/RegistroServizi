using MudBlazor;

namespace RegistroServizi.Web.Extensions;

public static class MudDialogExtensions
{
    public static async Task<bool> GenerateDialogItemAsync<TDialog>(IDialogService dialogService, ItemActionType actionType, string title, Guid id, string description, string cancelText, string confirmText) where TDialog : IComponent
    {
        return actionType switch
        {
            ItemActionType.Create => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetCreateItemDialogParameters<TDialog>(title, cancelText)),
            ItemActionType.Edit => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetEditItemDialogParameters<TDialog>(id, title, cancelText)),
            ItemActionType.Delete => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, title, description, cancelText, confirmText)),
            _ => false,
        };
    }

    private static async Task<bool> OpenDialogAndRefreshAsync<TDialog>(IDialogService dialogService, Func<DialogParameters> parametersFactory) where TDialog : IComponent
    {
        var dialogReference = await dialogService.ShowAsync<TDialog>(parametersFactory(), MudBlazorDialogOptions.GetSmallDialogOptions());
        var result = await dialogReference.Result;

        if (result is not null && !result.Canceled)
        {
            return true;
        }

        return false;
    }
}