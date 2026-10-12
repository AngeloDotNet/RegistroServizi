using RegistroServizi.Web.Common.Application;

namespace RegistroServizi.Web.Components.Pages.PrezziServizi;

public partial class PrezziServizi
{
    //[Inject] private ISnackbar Snackbar { get; set; } = default!;
    //[Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IPrezzoServizioService PrezzoServizioService { get; set; } = default!;
    [Inject] private ILogger<PrezziServizi> Logger { get; set; } = default!;

    //private readonly CancellationTokenSource cts = new();
    //private List<PrezzoServizioDto> elements = [];
    //private bool isLoading = true;
    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    private List<PrezzoServizioDto> elements = [];
    private List<string> columnWidths = [];
    private List<DataGridColumnDefinition<PrezzoServizioDto>> columnDefinition = [];
    //private bool isLoading = true;
    private string Title => Localizer["ServicePrices"];
    //private string CancelText => Localizer["Cancel"];
    //private string ConfirmText => Localizer["Confirm"];

    private bool isLoading = true;
    private bool addDisabled = true;
    private bool modifyDisabled;
    private bool deleteDisabled = true;

    //protected override async Task OnInitializedAsync() => await LoadingDataAsync();
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

    //protected override async Task OnInitializedAsync()
    //{
    //    try
    //    {
    //        var prezziServizi = await PrezzoServizioService.GetAllPrezziServiziAsync().ConfigureAwait(false);

    //        if (cts.IsCancellationRequested)
    //        {
    //            return;
    //        }

    //        await InvokeAsync(() =>
    //        {
    //            elements = prezziServizi?.ToList() ?? [];
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        Logger.LogError(ex, "Errore durante il caricamento dei prezzi dei servizi. Message: {Message}", ex.Message);
    //        await InvokeAsync(() => Snackbar.Add("Errore durante il caricamento dei prezzi dei servizi.", Severity.Error));
    //    }
    //    finally
    //    {
    //        await InvokeAsync(() => isLoading = false);
    //    }
    //}

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

    //private Task CreateItem()
    //{
    //    return DialogService?.ShowAsync<PrezzoServizioEditDialog>(
    //        MudBlazorDialogParameters.GetCreateItemDialogParameters<PrezzoServizioEditDialog>(title: Title, cancelText: CancelText),
    //        MudBlazorDialogOptions.GetSmallDialogOptions()) ?? Task.CompletedTask;
    //}

    //private Task EditItem(PrezzoServizioDto item)
    //{
    //    return DialogService?.ShowAsync<PrezzoServizioEditDialog>(
    //        MudBlazorDialogParameters.GetEditItemDialogParameters<PrezzoServizioEditDialog>(editDialogId: item.Id, title: Title, cancelText: CancelText),
    //        MudBlazorDialogOptions.GetSmallDialogOptions()) ?? Task.CompletedTask;
    //}
}