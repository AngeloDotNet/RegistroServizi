using MudBlazor;

namespace RegistroServizi.Web.Components.Pages.PrezziServizi;

public partial class PrezziServizi
{
    //[Inject] private ISnackbar Snackbar { get; set; } = default!;
    //[Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IPrezzoServizioService PrezzoServizioService { get; set; } = default!;
    [Inject] private ILogger<PrezziServizi> Logger { get; set; } = default!;

    private readonly CancellationTokenSource cts = new();
    private List<PrezzoServizioDto> elements = [];
    private bool isLoading = true;
    private string Title => Localizer["ServicePrices"];
    private string CancelText => Localizer["Cancel"];
    private string ConfirmText => Localizer["Confirm"];

    protected override async Task OnInitializedAsync()
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
            });
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Errore durante il caricamento dei prezzi dei servizi. Message: {Message}", ex.Message);
            await InvokeAsync(() => Snackbar.Add("Errore durante il caricamento dei prezzi dei servizi.", Severity.Error));
        }
        finally
        {
            await InvokeAsync(() => isLoading = false);
        }
    }

    private Task CreateItem()
    {
        return DialogService?.ShowAsync<PrezzoServizioEditDialog>(
            MudBlazorDialogParameters.GetCreateItemDialogParameters<PrezzoServizioEditDialog>(title: Title, cancelText: CancelText),
            MudBlazorDialogOptions.GetSmallDialogOptions()) ?? Task.CompletedTask;
    }

    private Task EditItem(PrezzoServizioDto item)
    {
        return DialogService?.ShowAsync<PrezzoServizioEditDialog>(
            MudBlazorDialogParameters.GetEditItemDialogParameters<PrezzoServizioEditDialog>(editDialogId: item.Id, title: Title, cancelText: CancelText),
            MudBlazorDialogOptions.GetSmallDialogOptions()) ?? Task.CompletedTask;
    }
}