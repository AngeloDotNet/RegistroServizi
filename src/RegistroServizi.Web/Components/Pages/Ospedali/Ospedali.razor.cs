using RegistroServizi.Web.Components.Shared.Dialogs;

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
        await this.LoadDataAsync(new MudDataGridLoadingOptions<OspedaleDto>
        {
            LoadItemsAsync = async () => (IEnumerable<OspedaleDto>?)await OspedaleService.GetAllOspedaliAsync(cts.Token),
            SetItems = items => elements = items,
            ConfigureView = () =>
            {
                columnWidths = Configuration.GetSection("DataGridColumnWidths:Ospedali").Get<List<string>>() ?? [];
                columnDefinition = MudDataGridParameters.GetOspedaliColumns(Localizer);
            },
            SetLoading = value => isLoading = value,
            Logger = Logger,
            Localizer = Localizer,
            Snackbar = Snackbar,
            InvokeAsync = InvokeAsync,
            CancellationToken = cts.Token
        });
    }

    private async Task CreateItemAsync(string title) => await MudDialogExtensions.ExecuteItemDialogAsync<OspedaleEditDialog>(DialogService, Snackbar, Localizer,
        LoadingDataAsync, InvokeAsync, ItemActionType.Create, title, Guid.Empty, string.Empty, string.Empty, DataGridHelper.CancelText, "CancelCreate");

    private async Task EditItemAsync(string title, Guid id, string description) => await MudDialogExtensions.ExecuteItemDialogAsync<OspedaleEditDialog>(DialogService, Snackbar,
        Localizer, LoadingDataAsync, InvokeAsync, ItemActionType.Edit, title, id, description, string.Empty, DataGridHelper.CancelText, "CancelModify");

    //private async Task DeleteItemAsync(string title, Guid id, string description) => await MudDialogExtensions.ExecuteItemDialogAsync<OspedaleDeleteDialog>(DialogService, Snackbar,
    //    Localizer, LoadingDataAsync, InvokeAsync, ItemActionType.Delete, title, id, description, DataGridHelper.ConfirmText, DataGridHelper.CancelText, "CancelDelete");

    private async Task DeleteItemAsync(string title, Guid id, string description)
    {
        await MudDialogExtensions.ExecuteItemDialogAsync<DeleteDialog>(DialogService, Snackbar, Localizer, LoadingDataAsync, InvokeAsync, ItemActionType.Delete, title, id, description,
            DataGridHelper.ConfirmText, DataGridHelper.CancelText, "CancelDelete", () => OspedaleService.DeleteOspedaleAsync(id));
    }
}