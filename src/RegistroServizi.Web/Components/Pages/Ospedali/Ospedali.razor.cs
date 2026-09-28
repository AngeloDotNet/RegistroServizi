using MudBlazor;
using RegistroServizi.Web.Common.Helpers;

namespace RegistroServizi.Web.Components.Pages.Ospedali;

public partial class Ospedali
{
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IOspedaleService OspedaleService { get; set; } = default!;
    [Inject] private ILogger<Ospedali> Logger { get; set; } = default!;
    [Inject] private DataGridHelper DataGridHelper { get; set; } = default!;

    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    private List<OspedaleDto> elements = [];
    private bool isLoading = true;
    private string Title => Localizer["Hospital"];
    //private string CancelText => Localizer["Cancel"];
    //private string ConfirmText => Localizer["Confirm"];
    //private string RowsPerPageString => Localizer["RowsPerPage"];
    //private string RowsPerPageInfoFormat => Localizer["RowsPerPageInfoFormat", "{first_item}", "{last_item}", "{all_items}"];

    protected override async Task OnInitializedAsync() => await LoadingDataAsync();

    private async Task LoadingDataAsync()
    {
        try
        {
            var ospedali = await OspedaleService.GetAllOspedaliAsync().ConfigureAwait(false);

            if (cts.IsCancellationRequested)
            {
                return;
            }

            await InvokeAsync(() =>
            {
                elements = ospedali?.ToList() ?? [];
            });
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{Localizer["LoadingError"]}. {Localizer["Message"]}: {ex.Message}");
            Snackbar.Add($"{Localizer["LoadingError"]}.", Severity.Error);
        }
        finally
        {
            await InvokeAsync(() => isLoading = false);
        }
    }

    //private async Task CreateItemAsync()
    //{
    //    var dialogReference = await DialogService.ShowAsync<OspedaleEditDialog>(
    //        MudBlazorDialogParameters.GetCreateItemDialogParameters<OspedaleEditDialog>(title: Title, cancelText: CancelText),
    //        MudBlazorDialogOptions.GetBackdropFilterDialogOptions());

    //    var result = await dialogReference.Result;

    //    if (result is not null && !result.Canceled)
    //    {
    //        await LoadingDataAsync();
    //    }
    //    else
    //    {
    //        await InvokeAsync(() => Snackbar.Add(Localizer["CancelCreate"], Severity.Info));
    //    }
    //}

    //private async Task EditItemAsync(OspedaleDto item)
    //{
    //    var dialogReference = await DialogService.ShowAsync<OspedaleEditDialog>(
    //        MudBlazorDialogParameters.GetEditItemDialogParameters<OspedaleEditDialog>(editDialogId: item.Id, title: Title, cancelText: CancelText),
    //        MudBlazorDialogOptions.GetBackdropFilterDialogOptions());

    //    var result = await dialogReference.Result;

    //    if (result is not null && !result.Canceled)
    //    {
    //        await LoadingDataAsync();
    //    }
    //    else
    //    {
    //        await InvokeAsync(() => Snackbar.Add(Localizer["CancelModify"], Severity.Info));
    //    }
    //}

    //private async Task DeleteItemAsync(OspedaleDto item)
    //{
    //    var dialogReference = await DialogService.ShowAsync<OspedaleDeleteDialog>(
    //        MudBlazorDialogParameters.GetDeleteItemDialogParameters<OspedaleDeleteDialog>(editDialogId: item.Id,
    //            title: Title,
    //            description: item.NomeOspedale,
    //            cancelText: CancelText,
    //            confirmText: Localizer["Confirm"]), MudBlazorDialogOptions.GetBackdropFilterDialogOptions());

    //    var result = await dialogReference.Result;

    //    if (result is not null && !result.Canceled)
    //    {
    //        elements.Remove(item);
    //        await LoadingDataAsync();
    //    }
    //    else
    //    {
    //        await InvokeAsync(() => Snackbar.Add(Localizer["CancelDelete"], Severity.Info));
    //    }
    //}

    private async Task<bool> OpenDialogAndRefreshAsync<TDialog>(Func<DialogParameters> parametersFactory, string cancelMessage) where TDialog : IComponent
    {
        var dialogReference = await DialogService.ShowAsync<TDialog>(parametersFactory(), MudBlazorDialogOptions.GetBackdropFilterDialogOptions());
        var result = await dialogReference.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadingDataAsync();
            return true;
        }

        await InvokeAsync(() => Snackbar.Add(Localizer[cancelMessage], Severity.Info));
        return false;
    }

    private Task CreateItemAsync() => OpenDialogAndRefreshAsync<OspedaleEditDialog>(()
        => MudBlazorDialogParameters.GetCreateItemDialogParameters<OspedaleEditDialog>(Title, DataGridHelper.CancelText), "CancelCreate");
    //{
    //    return OpenDialogAndRefreshAsync<OspedaleEditDialog>(() => MudBlazorDialogParameters
    //        .GetCreateItemDialogParameters<OspedaleEditDialog>(title: Title, cancelText: CancelText), "CancelCreate");
    //}

    private Task EditItemAsync(OspedaleDto item) => OpenDialogAndRefreshAsync<OspedaleEditDialog>(()
        => MudBlazorDialogParameters.GetEditItemDialogParameters<OspedaleEditDialog>(item.Id, Title, DataGridHelper.CancelText), "CancelModify");
    //{
    //    return OpenDialogAndRefreshAsync<OspedaleEditDialog>(() => MudBlazorDialogParameters
    //        .GetEditItemDialogParameters<OspedaleEditDialog>(editDialogId: item.Id, title: Title, cancelText: CancelText), "CancelModify");
    //}

    //private async Task DeleteItemAsync(OspedaleDto item)
    //{
    //    var confirmed = await OpenDialogAndRefreshAsync<OspedaleDeleteDialog>(()
    //        => MudBlazorDialogParameters.GetDeleteItemDialogParameters<OspedaleDeleteDialog>(item.Id, Title, item.NomeOspedale, CancelText, ConfirmText), "CancelDelete");

    //    //if (confirmed)
    //    //{
    //    //    elements.Remove(item);
    //    //}
    //}

    private async Task DeleteItemAsync(OspedaleDto item) => OpenDialogAndRefreshAsync<OspedaleDeleteDialog>(()
        => MudBlazorDialogParameters.GetDeleteItemDialogParameters<OspedaleDeleteDialog>(item.Id, Title, item.NomeOspedale, DataGridHelper.CancelText, DataGridHelper.ConfirmText), "CancelDelete");
}