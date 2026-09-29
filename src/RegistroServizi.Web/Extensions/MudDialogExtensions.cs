using MudBlazor;

namespace RegistroServizi.Web.Extensions;

public static class MudDialogExtensions
{
    //private static async Task GenerateDialogItemAsync<TDialog>(ItemActionType actionType, string Title, Guid id, string description) where TDialog : IComponent
    //{
    //    var result = false;

    //    if (actionType == ItemActionType.Create)
    //    {
    //        //return OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetCreateItemDialogParameters<TDialog>(Title, DataGridHelper.CancelText), "CancelCreate");
    //        var result = await OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetCreateItemDialogParameters<TDialog>(Title, DataGridHelper.CancelText), "CancelCreate");
    //        return result;
    //    }

    //    if (actionType == ItemActionType.Edit)
    //    {
    //        //return OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetEditItemDialogParameters<TDialog>(id, Title, DataGridHelper.CancelText), "CancelModify");
    //        var result = await OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetEditItemDialogParameters<TDialog>(id, Title, DataGridHelper.CancelText), "CancelModify");
    //        return result;
    //    }

    //    if (actionType == ItemActionType.Delete)
    //    {
    //        //return OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, Title, description, DataGridHelper.CancelText, DataGridHelper.ConfirmText), "CancelDelete");
    //        var result = await OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, Title, description, DataGridHelper.CancelText, DataGridHelper.ConfirmText), "CancelDelete");
    //        return result;
    //    }

    //    //return Task.CompletedTask;
    //    return result;
    //}

    //private static async Task<bool> GenerateDialogItemAsync<TDialog>(ItemActionType actionType, string title, Guid id, string description) where TDialog : IComponent
    //{
    //    if (actionType == ItemActionType.Create)
    //    {
    //        return await OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetCreateItemDialogParameters<TDialog>(title, DataGridHelper.CancelText), "CancelCreate");
    //    }

    //    if (actionType == ItemActionType.Edit)
    //    {
    //        return await OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetEditItemDialogParameters<TDialog>(id, title, DataGridHelper.CancelText), "CancelModify");
    //    }

    //    if (actionType == ItemActionType.Delete)
    //    {
    //        return await OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, title, description, DataGridHelper.CancelText, DataGridHelper.ConfirmText), "CancelDelete");
    //    }

    //    return false;
    //}

    public static async Task<bool> GenerateDialogItemAsync<TDialog>(IDialogService dialogService, ItemActionType actionType, string title, Guid id, string description, string cancelText, string confirmText) where TDialog : IComponent
    {
        //switch (actionType)
        //{
        //    case ItemActionType.Create:
        //        //return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetCreateItemDialogParameters<TDialog>(title, DataGridHelper.CancelText));
        //        return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetCreateItemDialogParameters<TDialog>(title, cancelText));

        //    case ItemActionType.Edit:
        //        //return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetEditItemDialogParameters<TDialog>(id, title, DataGridHelper.CancelText));
        //        return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetEditItemDialogParameters<TDialog>(id, title, cancelText));

        //    case ItemActionType.Delete:
        //        //return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, title, description, DataGridHelper.CancelText, DataGridHelper.ConfirmText));
        //        return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, title, description, cancelText, confirmText));

        //    default:
        //        return false;
        //}

        return actionType switch
        {
            // return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetCreateItemDialogParameters<TDialog>(title, DataGridHelper.CancelText));
            ItemActionType.Create => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetCreateItemDialogParameters<TDialog>(title, cancelText)),

            // return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetEditItemDialogParameters<TDialog>(id, title, DataGridHelper.CancelText));
            ItemActionType.Edit => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetEditItemDialogParameters<TDialog>(id, title, cancelText)),

            // return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, title, description, DataGridHelper.CancelText, DataGridHelper.ConfirmText));
            ItemActionType.Delete => await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, title, description, cancelText, confirmText)),

            // default => false,
            _ => false,
        };

        //if (actionType == ItemActionType.Create)
        //{
        //    return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetCreateItemDialogParameters<TDialog>(title, DataGridHelper.CancelText));
        //}

        //if (actionType == ItemActionType.Edit)
        //{
        //    return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetEditItemDialogParameters<TDialog>(id, title, DataGridHelper.CancelText));
        //}

        //if (actionType == ItemActionType.Delete)
        //{
        //    return await OpenDialogAndRefreshAsync<TDialog>(dialogService, () => MudBlazorDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, title, description, DataGridHelper.CancelText, DataGridHelper.ConfirmText));
        //}

        //return false;
    }

    //private static async Task<bool> OpenDialogAndRefreshAsync<TDialog>(Func<DialogParameters> parametersFactory, string cancelMessage) where TDialog : IComponent
    //{
    //    //var dialogReference = await DialogService.ShowAsync<TDialog>(parametersFactory(), MudBlazorDialogOptions.GetSmallDialogOptions());
    //    //var result = await dialogReference.Result;

    //    //if (result is not null && !result.Canceled)
    //    //{
    //    //    //await LoadingDataAsync();
    //    //    return true;
    //    //}

    //    ////await InvokeAsync(() => SnackbarExtensions.ShowNotifyInfo(Snackbar, Localizer[cancelMessage]));
    //    //return false;

    //    var dialogReference = await DialogService.ShowAsync<TDialog>(parametersFactory(), MudBlazorDialogOptions.GetSmallDialogOptions());
    //    var result = await dialogReference.Result;

    //    if (result is not null && !result.Canceled)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    //private static async Task<bool> OpenDialogAndRefreshAsync<TDialog>(IDialogService dialogService, Func<DialogParameters> parametersFactory, string cancelMessage) where TDialog : IComponent
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