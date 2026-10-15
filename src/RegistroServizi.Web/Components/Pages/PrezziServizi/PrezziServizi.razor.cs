namespace RegistroServizi.Web.Components.Pages.PrezziServizi;

public partial class PrezziServizi
{
    [Inject] private IPrezzoServizioService PrezzoServizioService { get; set; } = default!;
    [Inject] private ILogger<PrezziServizi> Logger { get; set; } = default!;
    //[Inject] private UserExtensions UserExtensions { get; set; } = default!;

    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    private List<PrezzoServizioDto> elements = [];
    private List<string> columnWidths = [];
    private List<DataGridColumnDefinition<PrezzoServizioDto>> columnDefinition = [];
    private string Title => Localizer["ServicePrices"];

    //private bool isLoading = true;
    //private bool disableEditButton;

    protected override async Task OnInitializedAsync()
    {
        //disableEditButton = await new UserExtensions(AuthenticationStateProvider).ManageButtonDisabledAsync(new string[] { nameof(Role.Manager) });

        // Versione 1
        //disableEditButton = await new UserExtensions(AuthenticationStateProvider).ManageButtonDisabledAsync(Configuration.GetSection("PermissionsTable:PrezziServizi:Modify").Get<string[]>() ?? []);

        // Versione 2
        disableEditButton = await UserExtensions.ManageButtonDisabledAsync(Configuration.GetSection("PermissionsTable:PrezziServizi:Modify").Get<string[]>() ?? []);

        await LoadingDataAsync();
    }

    private async Task LoadingDataAsync()
    {
        // Opzione originale
        //try
        //{
        //    var prezziServizi = await PrezzoServizioService.GetAllPrezziServiziAsync().ConfigureAwait(false);
        //
        //    if (cts.IsCancellationRequested)
        //    {
        //        return;
        //    }
        //
        //    await InvokeAsync(() =>
        //    {
        //        elements = prezziServizi?.ToList() ?? [];
        //        columnWidths = Configuration.GetSection("DataGridColumnWidths:PrezziServizi").Get<List<string>>() ?? [];
        //        columnDefinition = MudBlazorDataGridParameters.GetPrezziServiziColumns(Localizer);
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

        await this.LoadDataAsync(new MudDataGridLoadingOptions<PrezzoServizioDto>
        {
            LoadItemsAsync = async () => (IEnumerable<PrezzoServizioDto>?)await PrezzoServizioService.GetAllPrezziServiziAsync(cts.Token),
            SetItems = items => elements = items,
            ConfigureView = () =>
            {
                columnWidths = Configuration.GetSection("DataGridColumnWidths:PrezziServizi").Get<List<string>>() ?? [];
                columnDefinition = MudDataGridParameters.GetPrezziServiziColumns(Localizer);
            },
            SetLoading = value => isLoading = value,
            Logger = Logger,
            Localizer = Localizer,
            Snackbar = Snackbar,
            InvokeAsync = InvokeAsync,
            CancellationToken = cts.Token
        });
    }

    //private async Task EditItemAsync(string Title, Guid Id, string Description)
    //{
    //    var result = await MudDialogExtensions.GenerateDialogItemAsync<PrezzoServizioEditDialog>(DialogService, ItemActionType.Edit, Title, Id, Description, DataGridHelper.CancelText, string.Empty);

    //    if (result)
    //    {
    //        await LoadingDataAsync();
    //    }
    //    else
    //    {
    //        await InvokeAsync(() => SnackbarExtensions.ShowNotifyInfo(Snackbar, Localizer["CancelModify"]));
    //    }
    //}

    private async Task EditItemAsync(string title, Guid id, string description)
        => await MudDialogExtensions.ExecuteItemDialogAsync<PrezzoServizioEditDialog>(DialogService, Snackbar, Localizer, LoadingDataAsync, InvokeAsync, ItemActionType.Edit, title, id,
            description, string.Empty, DataGridHelper.CancelText, "CancelModify");
}