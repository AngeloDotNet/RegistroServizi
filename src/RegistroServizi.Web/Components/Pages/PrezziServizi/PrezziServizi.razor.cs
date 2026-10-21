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

    protected override async Task OnInitializedAsync()
    {
        disableEditButton = await UserExtensions.ManageButtonDisabledAsync(Configuration.GetSection("PermissionsTable:PrezziServizi:Modify").Get<string[]>() ?? []);

        await LoadingDataAsync();
    }

    private async Task LoadingDataAsync()
    {
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

    private async Task EditItemAsync(string title, Guid id, string description) => await MudDialogExtensions.ExecuteItemDialogAsync<PrezzoServizioEditDialog>(DialogService,
        Snackbar, Localizer, LoadingDataAsync, InvokeAsync, ItemActionType.Edit, title, id, description, string.Empty, DataGridHelper.CancelText, "CancelModify");
}