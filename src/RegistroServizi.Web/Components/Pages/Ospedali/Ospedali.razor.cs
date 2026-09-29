namespace RegistroServizi.Web.Components.Pages.Ospedali;

public partial class Ospedali
{
    //[Inject] private IConfiguration Configuration { get; set; } = default!;
    [Inject] private IOspedaleService OspedaleService { get; set; } = default!;
    [Inject] private ILogger<Ospedali> Logger { get; set; } = default!;
    //[Inject] private DataGridHelper DataGridHelper { get; set; } = default!;
    //[Inject] private IOptions<MudBlazorDataGridOptions> DataGridOptions { get; set; } = default!;

    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    private List<OspedaleDto> elements = [];
    private List<string> columnWidths = [];
    private List<DataGridColumnDefinition<OspedaleDto>> columnDefinition = [];
    private bool isLoading = true;
    private string Title => Localizer["Hospital"];

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
                columnWidths = Configuration.GetSection("DataGridColumnWidths:Ospedali").Get<List<string>>() ?? [];
                columnDefinition = MudBlazorDataGridParameters.GetOspedaliColumns(Localizer);
            });
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{Localizer["LoadingError"]}. {Localizer["Message"]}: {ex.Message}");
            SnackbarExtensions.ShowNotifyError(Snackbar, Localizer["LoadingError"]);
        }
        finally
        {
            await InvokeAsync(() => isLoading = false);
        }
    }

    //private async Task<bool> OpenDialogAndRefreshAsync<TDialog>(Func<DialogParameters> parametersFactory, string cancelMessage) where TDialog : IComponent
    //{
    //    var dialogReference = await DialogService.ShowAsync<TDialog>(parametersFactory(), MudBlazorDialogOptions.GetSmallDialogOptions());
    //    var result = await dialogReference.Result;

    //    if (result is not null && !result.Canceled)
    //    {
    //        await LoadingDataAsync();
    //        return true;
    //    }

    //    await InvokeAsync(() => SnackbarExtensions.ShowNotifyInfo(Snackbar, Localizer[cancelMessage]));
    //    return false;
    //}

    //private static Task GenerateDialogItemAsync<TDialog>(ItemActionType actionType, Guid id, string description) where TDialog : IComponent
    //{
    //    if (actionType == ItemActionType.Create)
    //    {
    //        return OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetCreateItemDialogParameters<TDialog>(Title, DataGridHelper.CancelText), "CancelCreate");
    //    }

    //    if (actionType == ItemActionType.Edit)
    //    {
    //        return OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetEditItemDialogParameters<TDialog>(id, Title, DataGridHelper.CancelText), "CancelModify");
    //    }

    //    if (actionType == ItemActionType.Delete)
    //    {
    //        return OpenDialogAndRefreshAsync<TDialog>(() => MudBlazorDialogParameters.GetDeleteItemDialogParameters<TDialog>(id, Title, description, DataGridHelper.CancelText, DataGridHelper.ConfirmText), "CancelDelete");
    //    }

    //    return Task.CompletedTask;
    //}

    //private async Task CreateItemAsync(IDialogService dialogService, string Title)
    private async Task CreateItemAsync(string Title)
    {
        //var result = await MudDialogExtensions.GenerateDialogItemAsync<OspedaleEditDialog>(dialogService, ItemActionType.Create, Title, Guid.Empty, string.Empty, DataGridHelper.CancelText, string.Empty);
        var result = await MudDialogExtensions.GenerateDialogItemAsync<OspedaleEditDialog>(DialogService, ItemActionType.Create, Title, Guid.Empty, string.Empty, DataGridHelper.CancelText, string.Empty);

        if (result)
        {
            await LoadingDataAsync();
        }
        else
        {
            await InvokeAsync(() => SnackbarExtensions.ShowNotifyInfo(Snackbar, Localizer["CancelCreate"]));
        }
    }

    //private async Task EditItemAsync(IDialogService dialogService, string Title, Guid Id, string Description)
    private async Task EditItemAsync(string Title, Guid Id, string Description)
    {
        //var result = await MudDialogExtensions.GenerateDialogItemAsync<OspedaleEditDialog>(dialogService, ItemActionType.Edit, Title, Id, Description, DataGridHelper.CancelText, string.Empty);
        var result = await MudDialogExtensions.GenerateDialogItemAsync<OspedaleEditDialog>(DialogService, ItemActionType.Edit, Title, Id, Description, DataGridHelper.CancelText, string.Empty);

        if (result)
        {
            await LoadingDataAsync();
        }
        else
        {
            await InvokeAsync(() => SnackbarExtensions.ShowNotifyInfo(Snackbar, Localizer["CancelModify"]));
        }
    }

    //private async Task DeleteItemAsync(IDialogService dialogService, string Title, Guid Id, string Description)
    private async Task DeleteItemAsync(string Title, Guid Id, string Description)
    {
        //var result = await MudDialogExtensions.GenerateDialogItemAsync<OspedaleDeleteDialog>(dialogService, ItemActionType.Delete, Title, Id, Description, DataGridHelper.CancelText, DataGridHelper.ConfirmText);
        var result = await MudDialogExtensions.GenerateDialogItemAsync<OspedaleDeleteDialog>(DialogService, ItemActionType.Delete, Title, Id, Description, DataGridHelper.CancelText, DataGridHelper.ConfirmText);

        if (result)
        {
            await LoadingDataAsync();
        }
        else
        {
            await InvokeAsync(() => SnackbarExtensions.ShowNotifyInfo(Snackbar, Localizer["CancelDelete"]));
        }
    }

    //private Task CreateItemAsync() => OpenDialogAndRefreshAsync<OspedaleEditDialog>(()
    //    => MudBlazorDialogParameters.GetCreateItemDialogParameters<OspedaleEditDialog>(Title, DataGridHelper.CancelText), "CancelCreate");

    //private Task EditItemAsync(OspedaleDto item) => OpenDialogAndRefreshAsync<OspedaleEditDialog>(()
    //    => MudBlazorDialogParameters.GetEditItemDialogParameters<OspedaleEditDialog>(item.Id, Title, DataGridHelper.CancelText), "CancelModify");

    //private Task DeleteItemAsync(OspedaleDto item) => OpenDialogAndRefreshAsync<OspedaleDeleteDialog>(()
    //    => MudBlazorDialogParameters.GetDeleteItemDialogParameters<OspedaleDeleteDialog>(item.Id, Title, item.NomeOspedale, DataGridHelper.CancelText, DataGridHelper.ConfirmText), "CancelDelete");

    //private readonly List<string> columnWidths = ["width: 30%", "width: 30%", "width: 30%", "width: 5%", "width: 5%"];

    //private readonly List<DataGridColumnDefinition<OspedaleDto>> hospitalColumns =
    //[
    //    new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Id, Title = "Id", IsHidden = true },
    //    new DataGridColumnDefinition<OspedaleDto> { Property = x => x.NomeOspedale, Title = "Nome Ospedale" },
    //    new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Strada, Title = "Via" },
    //    new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Citta, Title = "Città" },
    //    new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Provincia, Title = "Provincia" },
    //    new DataGridColumnDefinition<OspedaleDto> { Property = x => x.Indirizzo.Cap, Title = "Cap" }
    //];
}