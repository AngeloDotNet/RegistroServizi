using MudBlazor;

namespace RegistroServizi.Web.Components.Pages.Ospedali;

public partial class Ospedali : IDisposable
{
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IOspedaleService OspedaleService { get; set; } = default!;
    [Inject] private ILogger<Ospedali> Logger { get; set; } = default!;

    private MudDataGrid<OspedaleDto> dataGrid = default!;
    private List<OspedaleDto> elements = [];

    private bool isLoading = true;
    private bool isEditing;

    private readonly CancellationTokenSource cts = new CancellationTokenSource();

    protected override async Task OnInitializedAsync()
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
            });
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Errore durante il caricamento degli ospedali. Message: {Message}", ex.Message);
            await InvokeAsync(() => Snackbar.Add("Errore durante il caricamento degli ospedali.", Severity.Error));
        }
        finally
        {
            await InvokeAsync(() => isLoading = false);
        }
    }

    private void StartedEditingItem(OspedaleDto item) => isEditing = true;
    private void CanceledEditingItem(OspedaleDto item) => isEditing = false;

    private Task OpenDialogAsync
    {
        get
        {
            const string message = "Sei sicuro di voler aprire il dialogo ?";

            return DialogService?.ShowAsync<TestDialog>("Simple Dialog",
                DependencyInjection.GetConfirmDialogParameters<TestDialog>(message),
                DependencyInjection.GetDefaultDialogOptions()) ?? Task.CompletedTask;
        }
    }

    private async Task<DataGridEditFormAction> CommittedItemChangesAsync(OspedaleDto item)
    {
        if (!ValidateOspedale(item))
        {
            return DataGridEditFormAction.KeepOpen;
        }

        var updateItemIndirizzo = new UpdateIndirizzoDto(item.Indirizzo.Strada, item.Indirizzo.Citta, item.Indirizzo.Provincia, item.Indirizzo.Cap);
        var updatedItem = new UpdateOspedaleDto(item.Id, item.NomeOspedale, updateItemIndirizzo);

        try
        {
            await OspedaleService.UpdateOspedaleAsync(updatedItem).ConfigureAwait(false);

            if (cts.IsCancellationRequested)
            {
                return DataGridEditFormAction.KeepOpen;
            }

            await InvokeAsync(() => isEditing = false);
            await InvokeAsync(() => Snackbar.Add("L'ospedale è stato aggiornato con successo.", Severity.Success));

            return DataGridEditFormAction.Close;
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();

            Logger.LogError(ex, "Errore durante l'aggiornamento. ID: {CorrelationId}. Message: {Message}", correlationId, ex.Message);
            await InvokeAsync(() => Snackbar.Add($"Errore durante l'aggiornamento. ID: {correlationId}", Severity.Error));

            return DataGridEditFormAction.KeepOpen;
        }
    }

    private async void DeleteItem(OspedaleDto item)
    {
        if (item.Id == Guid.Empty)
        {
            Snackbar.Add("L'ospedale non ha un ID valido e non può essere eliminato.", Severity.Warning);
            return;
        }

        try
        {
            await OspedaleService.DeleteOspedaleAsync(item.Id).ConfigureAwait(false);
            Snackbar.Add("L'ospedale è stato eliminato con successo.", Severity.Success);
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();

            Logger.LogError(ex, "Errore durante l'eliminazione. ID: {CorrelationId}. Message: {Message}", correlationId, ex.Message);
            Snackbar.Add($"Errore durante l'eliminazione. ID: {correlationId}", Severity.Error);
        }
    }

    private void ValidateNomeOspedale(string value)
        => MudblazorValidator.ValidateIsNotNullOrWhiteSpace(Snackbar, value, "Il nome dell'ospedale non può essere vuoto.");

    private void ValidateStrada(string value)
        => MudblazorValidator.ValidateIsNotNullOrWhiteSpace(Snackbar, value, "Il nome della strada non può essere vuoto.");

    private void ValidateCitta(string value)
        => MudblazorValidator.ValidateIsNotNullOrWhiteSpace(Snackbar, value, "Il nome della città non può essere vuoto.");

    private void ValidateProvincia(string value)
        => MudblazorValidator.ValidateIsNotNullOrWhiteSpace(Snackbar, value, "Il nome della provincia non può essere vuoto e deve essere di 2 caratteri.");

    private void ValidateCap(int value)
    {
        if (value < MudblazorValidator.minCap || value > MudblazorValidator.maxCap)
        {
            Snackbar.Add("Il codice avviamento postale deve essere un numero di 5 cifre.", Severity.Warning);
        }
    }

    private bool ValidateOspedale(OspedaleDto item)
    {
        if (item == null || item.Indirizzo == null)
        {
            return false;
        }

        var addr = item.Indirizzo;
        bool IsNullOrWhite(string s) => string.IsNullOrWhiteSpace(s);

        var validations = new (bool Invalid, string Message)[]
        {
            (IsNullOrWhite(item.NomeOspedale), "Il nome dell'ospedale non può essere vuoto."),
            (IsNullOrWhite(addr.Strada), "Il nome della strada non può essere vuoto."),
            (IsNullOrWhite(addr.Citta), "Il nome della città non può essere vuoto."),
            (IsNullOrWhite(addr.Provincia) || addr.Provincia.Length != 2, "Il nome della provincia non può essere vuoto e deve essere di 2 caratteri."),
            (addr.Cap < MudblazorValidator.minCap || addr.Cap > MudblazorValidator.maxCap, "Il codice avviamento postale deve essere un numero di 5 cifre.")
        };

        foreach (var v in validations)
        {
            if (v.Invalid)
            {
                Snackbar.Add(v.Message, Severity.Warning);
                return false;
            }
        }

        return true;
    }

    public void Dispose()
    {
        if (!cts.IsCancellationRequested)
        {
            cts.Cancel();
        }

        cts.Dispose();
    }
}