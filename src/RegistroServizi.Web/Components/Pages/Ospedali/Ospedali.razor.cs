namespace RegistroServizi.Web.Components.Pages.Ospedali;

public partial class Ospedali
{
    [Inject] private IOspedaleService OspedaleService { get; set; } = default!;
    [Inject] private ILogger<Ospedali> Logger { get; set; } = default!;

    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    private List<OspedaleDto> elements = [];
    private List<string> columnWidths = [];
    private List<DataGridColumnDefinition<OspedaleDto>> columnDefinition = [];
    private string Title => Localizer["Hospital"];

    protected override async Task OnInitializedAsync() => await LoadingDataAsync();

    private async Task LoadingDataAsync()
    {
        // Opzione originale
        //try
        //{
        //    var ospedali = await OspedaleService.GetAllOspedaliAsync().ConfigureAwait(false);
        //
        //    if (cts.IsCancellationRequested)
        //    {
        //        return;
        //    }
        //
        //    await InvokeAsync(() =>
        //    {
        //        elements = ospedali?.ToList() ?? [];
        //        columnWidths = Configuration.GetSection("DataGridColumnWidths:Ospedali").Get<List<string>>() ?? [];
        //        columnDefinition = MudBlazorDataGridParameters.GetOspedaliColumns(Localizer);
        //    });
        //}
        //catch (Exception ex)
        //{
        //    Logger.LogError(ex, $"{Localizer["LoadingError"]}. {Localizer["Message"]}: {ex.Message}");
        //    SnackbarExtensions.ShowNotifyError(Snackbar, Localizer["LoadingError"]);
        //}
        //finally
        //{
        //    await InvokeAsync(() => isLoading = false);
        //}

        // Opzione 2
        //await this.LoadDataAsync(loadItemsAsync: async () => (IEnumerable<OspedaleDto>?)await OspedaleService.GetAllOspedaliAsync(cts.Token),
        //    setItems: items => elements = items?.ToList() ?? [], configureView: () =>
        //    {
        //        columnWidths = Configuration.GetSection("DataGridColumnWidths:Ospedali").Get<List<string>>() ?? [];
        //        columnDefinition = MudBlazorDataGridParameters.GetOspedaliColumns(Localizer);
        //    },
        //    setLoading: value => isLoading = value, logger: Logger, localizer: Localizer, snackbar: Snackbar, invokeAsync: InvokeAsync, cancellationToken: cts.Token);

        // Opzione 3
        await this.LoadDataAsync(new MudDataGridLoadingOptions<OspedaleDto>
        {
            LoadItemsAsync = async () => (IEnumerable<OspedaleDto>?)await OspedaleService.GetAllOspedaliAsync(cts.Token),
            SetItems = items => elements = items,
            ConfigureView = () =>
            {
                columnWidths = Configuration.GetSection("DataGridColumnWidths:Ospedali").Get<List<string>>() ?? [];
                columnDefinition = MudBlazorDataGridParameters.GetOspedaliColumns(Localizer);
            },
            SetLoading = value => isLoading = value,
            Logger = Logger,
            Localizer = Localizer,
            Snackbar = Snackbar,
            InvokeAsync = InvokeAsync,
            CancellationToken = cts.Token
        });
    }

    private async Task CreateItemAsync(string title)
        => await MudDialogExtensions.ExecuteItemDialogAsync<OspedaleEditDialog>(DialogService, Snackbar, Localizer, LoadingDataAsync, InvokeAsync, ItemActionType.Create, title, Guid.Empty,
            string.Empty, string.Empty, DataGridHelper.CancelText, "CancelCreate");
    //{
    //    await MudDialogExtensions.ExecuteItemDialogAsync<OspedaleEditDialog>(DialogService,
    //        Snackbar,
    //        Localizer,
    //        LoadingDataAsync,
    //        InvokeAsync,
    //        ItemActionType.Create,
    //        title,
    //        Guid.Empty,
    //        string.Empty,
    //        string.Empty,
    //        DataGridHelper.CancelText,
    //        "CancelCreate");
    //}

    private async Task EditItemAsync(string title, Guid id, string description)
        => await MudDialogExtensions.ExecuteItemDialogAsync<OspedaleEditDialog>(DialogService, Snackbar, Localizer, LoadingDataAsync, InvokeAsync, ItemActionType.Edit, title, id,
            description, string.Empty, DataGridHelper.CancelText, "CancelModify");
    //{
    //    await MudDialogExtensions.ExecuteItemDialogAsync<OspedaleEditDialog>(DialogService,
    //        Snackbar,
    //        Localizer,
    //        LoadingDataAsync,
    //        InvokeAsync,
    //        ItemActionType.Edit,
    //        title,
    //        id,
    //        description,
    //        string.Empty,
    //        DataGridHelper.CancelText,
    //        "CancelModify");
    //}

    private async Task DeleteItemAsync(string title, Guid id, string description)
        => await MudDialogExtensions.ExecuteItemDialogAsync<OspedaleDeleteDialog>(DialogService, Snackbar, Localizer, LoadingDataAsync, InvokeAsync, ItemActionType.Delete, title, id,
            description, DataGridHelper.ConfirmText, DataGridHelper.CancelText, "CancelDelete");
    //{
    //    await MudDialogExtensions.ExecuteItemDialogAsync<OspedaleDeleteDialog>(DialogService,
    //        Snackbar,
    //        Localizer,
    //        LoadingDataAsync,
    //        InvokeAsync,
    //        ItemActionType.Delete,
    //        title,
    //        id,
    //        description,
    //        DataGridHelper.ConfirmText,
    //        DataGridHelper.CancelText,
    //        "CancelDelete");
    //}

    // Inizio originale
    //private async Task CreateItemAsync(string Title)
    //{
    //    var result = await MudDialogExtensions.GenerateDialogItemAsync<OspedaleEditDialog>(DialogService, ItemActionType.Create, Title, Guid.Empty, string.Empty, DataGridHelper.CancelText, string.Empty);

    //    if (result)
    //    {
    //        await LoadingDataAsync();
    //    }
    //    else
    //    {
    //        await InvokeAsync(() => SnackbarExtensions.ShowNotifyInfo(Snackbar, Localizer["CancelCreate"]));
    //    }
    //}

    //private async Task EditItemAsync(string Title, Guid Id, string Description)
    //{
    //    var result = await MudDialogExtensions.GenerateDialogItemAsync<OspedaleEditDialog>(DialogService, ItemActionType.Edit, Title, Id, Description, DataGridHelper.CancelText, string.Empty);

    //    if (result)
    //    {
    //        await LoadingDataAsync();
    //    }
    //    else
    //    {
    //        await InvokeAsync(() => SnackbarExtensions.ShowNotifyInfo(Snackbar, Localizer["CancelModify"]));
    //    }
    //}

    //private async Task DeleteItemAsync(string Title, Guid Id, string Description)
    //{
    //    var result = await MudDialogExtensions.GenerateDialogItemAsync<OspedaleDeleteDialog>(DialogService, ItemActionType.Delete, Title, Id, Description, DataGridHelper.CancelText, DataGridHelper.ConfirmText);

    //    if (result)
    //    {
    //        await LoadingDataAsync();
    //    }
    //    else
    //    {
    //        await InvokeAsync(() => SnackbarExtensions.ShowNotifyInfo(Snackbar, Localizer["CancelDelete"]));
    //    }
    //}
}