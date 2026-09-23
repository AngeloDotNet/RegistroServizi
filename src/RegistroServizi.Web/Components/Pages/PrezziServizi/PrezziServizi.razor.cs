using MudBlazor;

namespace RegistroServizi.Web.Components.Pages.PrezziServizi;

public partial class PrezziServizi : IAsyncDisposable
{
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    [Inject] private IPrezzoServizioService PrezzoServizioService { get; set; } = default!;
    [Inject] private ILogger<PrezziServizi> Logger { get; set; } = default!;

    private MudDataGrid<PrezzoServizioDto> dataGrid = default!;
    private List<PrezzoServizioDto> elements = [];

    private bool isLoading = true;
    private bool isEditing;
    private readonly CancellationTokenSource cts = new();

    protected override async Task OnInitializedAsync()
    {
        var correlationId = Guid.NewGuid().ToString();

        try
        {
            var prezziServizi = await PrezzoServizioService.GetAllPrezziServiziAsync().ConfigureAwait(false);
            elements = prezziServizi?.ToList() ?? new List<PrezzoServizioDto>();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Errore durante il caricamento dei prezzi. ID: {CorrelationId}", correlationId);
            Snackbar.Add($"Errore durante il caricamento dei prezzi. ID: {correlationId}", Severity.Error);
        }
        finally
        {
            isLoading = false;
        }
    }

    private void StartedEditingItem(PrezzoServizioDto item) => isEditing = true;
    private void CanceledEditingItem(PrezzoServizioDto item) => isEditing = false;

    private async Task<DataGridEditFormAction> CommittedItemChangesAsync(PrezzoServizioDto item)
    {
        if (!ValidatePrezzoServizio(item) || item == null)
        {
            return DataGridEditFormAction.KeepOpen;
        }

        var updatedItem = new UpdatePrezzoServizioDto(item.Id, item.TipologiaServizioId, item.CostoFisso, item.CostoKm, item.SecondoTrasportato, item.FermoMacchina, item.Accompagnatore, item.ScontoSocio);

        try
        {
            await PrezzoServizioService.UpdatePrezzoServizioAsync(updatedItem).ConfigureAwait(false);

            isEditing = false;

            Snackbar.Add("I prezzi del servizio aggiornati con successo.", Severity.Success);
            return DataGridEditFormAction.Close;
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();

            Logger.LogError(ex, "Errore durante l'aggiornamento. ID: {CorrelationId}. Message: {Message}", correlationId, ex.Message);
            Snackbar.Add($"Errore durante l'aggiornamento. ID: {correlationId}", Severity.Error);

            return DataGridEditFormAction.KeepOpen;
        }
    }

    private void ValidateCostoFisso(decimal value)
        => MudblazorValidator.ValidateNonNegative(Snackbar, value, "Il costo fisso non può essere negativo.");

    private void ValidateCostoKm(decimal value)
        => MudblazorValidator.ValidateNonNegative(Snackbar, value, "Il costo per km non può essere negativo.");

    private void ValidateSecondoTrasportato(decimal value)
        => MudblazorValidator.ValidateNonNegative(Snackbar, value, "Il costo per il secondo trasportato non può essere negativo.");

    private void ValidateFermoMacchina(decimal value)
        => MudblazorValidator.ValidateNonNegative(Snackbar, value, "Il costo del fermo macchina non può essere negativo.");

    private void ValidateAccompagnatore(decimal? value)
        => MudblazorValidator.ValidateNonNegative(Snackbar, value, "Il costo per l'accompagnatore non può essere negativo.");

    private void ValidateScontoSocio(int? value)
        => MudblazorValidator.ValidateNonNegative(Snackbar, value, "Lo sconto socio non può essere negativo.");

    private bool ValidatePrezzoServizio(PrezzoServizioDto item)
    {
        if (item == null)
        {
            return false;
        }

        var checks = new (object? Value, string Message)[]
        {
            (item.CostoFisso, "Il costo fisso non può essere negativo."),
            (item.CostoKm, "Il costo per km non può essere negativo."),
            (item.SecondoTrasportato, "Il costo per il secondo trasportato non può essere negativo."),
            (item.FermoMacchina, "Il costo del fermo macchina non può essere negativo."),
            (item.Accompagnatore, "Il costo per l'accompagnatore non può essere negativo."),
            (item.ScontoSocio, "Lo sconto socio non può essere negativo.")
        };

        foreach (var (value, message) in checks)
        {
            if (MudblazorValidator.TryConvertToDecimalNullable(value, out var dec) && MudblazorValidator.IsNegative(dec, out _))
            {
                Snackbar.Add(message, Severity.Warning);
                return false;
            }
        }

        return true;
    }

    public ValueTask DisposeAsync()
    {
        cts.Cancel();
        cts.Dispose();

        return ValueTask.CompletedTask;
    }
}