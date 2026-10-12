using RegistroServizi.Web.Common.Application;

namespace RegistroServizi.Web.Components.Pages.PrezziServizi;

public partial class PrezziServizi
{
    [Inject] private IPrezzoServizioService PrezzoServizioService { get; set; } = default!;
    [Inject] private ILogger<PrezziServizi> Logger { get; set; } = default!;
    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    private List<PrezzoServizioDto> elements = [];
    private List<string> columnWidths = [];
    private List<DataGridColumnDefinition<PrezzoServizioDto>> columnDefinition = [];
    private string Title => Localizer["ServicePrices"];

    private bool isLoading = true;
    private bool addDisabled = true;
    private bool modifyDisabled;
    private bool deleteDisabled = true;

    protected override async Task OnInitializedAsync()
    {
        //addDisabled = await new UserExtensions(AuthenticationStateProvider).ManageButtonDisabledAsync(new string[] { nameof(Role.Manager) });
        modifyDisabled = await new UserExtensions(AuthenticationStateProvider).ManageButtonDisabledAsync(new string[] { nameof(Role.Manager) });
        //deleteDisabled = await new UserExtensions(AuthenticationStateProvider).ManageButtonDisabledAsync(new string[] { nameof(Role.Admin) });

        await LoadingDataAsync();
    }

    private async Task LoadingDataAsync()
    {
        try
        {
            var prezziServizi = await PrezzoServizioService.GetAllPrezziServiziAsync().ConfigureAwait(false);

            if (cts.IsCancellationRequested)
            {
                return;
            }

            await InvokeAsync(() =>
            {
                elements = prezziServizi?.ToList() ?? [];
                columnWidths = Configuration.GetSection("DataGridColumnWidths:PrezziServizi").Get<List<string>>() ?? [];
                columnDefinition = MudBlazorDataGridParameters.GetPrezziServiziColumns(Localizer);
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
        var result = await MudDialogExtensions.GenerateDialogItemAsync<PrezzoServizioEditDialog>(DialogService, ItemActionType.Create, Title, Guid.Empty, string.Empty, DataGridHelper.CancelText, string.Empty);

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
        var result = await MudDialogExtensions.GenerateDialogItemAsync<PrezzoServizioEditDialog>(DialogService, ItemActionType.Edit, Title, Id, Description, DataGridHelper.CancelText, string.Empty);

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
        var result = await MudDialogExtensions.GenerateDialogItemAsync<PrezzoServizioDeleteDialog>(DialogService, ItemActionType.Delete, Title, Id, Description, DataGridHelper.CancelText, DataGridHelper.ConfirmText);

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