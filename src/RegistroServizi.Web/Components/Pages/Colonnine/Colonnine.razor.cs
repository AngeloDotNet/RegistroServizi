namespace RegistroServizi.Web.Components.Pages.Colonnine;

public partial class Colonnine
{
    [Inject] private IColonninaService ColonninaService { get; set; } = default!;
    [Inject] private ILogger<Colonnine> Logger { get; set; } = default!;

    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    private List<ColonninaDto> elements = [];
    private List<string> columnWidths = [];
    private List<DataGridColumnDefinition<ColonninaDto>> columnDefinition = [];
    private string Title => Localizer["TitleStations"];

    protected override async Task OnInitializedAsync() => await LoadingDataAsync();

    private async Task LoadingDataAsync()
    {
        await this.LoadDataAsync(new MudDataGridLoadingOptions<ColonninaDto>
        {
            LoadItemsAsync = async () => (IEnumerable<ColonninaDto>?)await ColonninaService.GetAllColonnineAsync(cts.Token),
            SetItems = items => elements = items,
            ConfigureView = () =>
            {
                columnWidths = Configuration.GetSection("DataGridColumnWidths:Colonnine").Get<List<string>>() ?? [];
                columnDefinition = MudDataGridParameters.GetColonnineColumns(Localizer);
            },
            SetLoading = value => isLoading = value,
            Logger = Logger,
            Localizer = Localizer,
            Snackbar = Snackbar,
            InvokeAsync = InvokeAsync,
            CancellationToken = cts.Token
        });
    }

    private async Task CreateItemAsync(string title) => await MudDialogExtensions.ExecuteItemDialogAsync<ColonninaEditDialog>(DialogService, Snackbar, Localizer,
        LoadingDataAsync, InvokeAsync, ItemActionType.Create, title, Guid.Empty, string.Empty, string.Empty, DataGridHelper.CancelText, "CancelCreate");

    private async Task EditItemAsync(string title, Guid id, string description) => await MudDialogExtensions.ExecuteItemDialogAsync<ColonninaEditDialog>(DialogService, Snackbar,
        Localizer, LoadingDataAsync, InvokeAsync, ItemActionType.Edit, title, id, description, string.Empty, DataGridHelper.CancelText, "CancelModify");

    private async Task DeleteItemAsync(string title, Guid id, string description) => await MudDialogExtensions.ExecuteItemDialogAsync<ColonninaDeleteDialog>(DialogService, Snackbar,
        Localizer, LoadingDataAsync, InvokeAsync, ItemActionType.Delete, title, id, description, DataGridHelper.ConfirmText, DataGridHelper.CancelText, "CancelDelete");
}