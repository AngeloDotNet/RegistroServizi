namespace RegistroServizi.Web.Components.Pages.Ospedali;

public partial class Ospedali
{
    [Inject] private IOspedaleService OspedaleService { get; set; } = default!;
    [Inject] private ILogger<Ospedali> Logger { get; set; } = default!;

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

    private async Task CreateItemAsync(string Title)
    {
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

    private async Task EditItemAsync(string Title, Guid Id, string Description)
    {
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

    private async Task DeleteItemAsync(string Title, Guid Id, string Description)
    {
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
}