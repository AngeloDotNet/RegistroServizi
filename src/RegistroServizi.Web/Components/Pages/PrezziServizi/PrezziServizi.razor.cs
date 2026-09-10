using MudBlazor;

namespace RegistroServizi.Web.Components.Pages.PrezziServizi;

public partial class PrezziServizi
{
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    [Inject] private IPrezzoServizioService PrezzoServizioService { get; set; } = default!;
    [Inject] private ILogger<PrezziServizi> Logger { get; set; } = default!;

    private MudDataGrid<PrezzoServizioDto> dataGrid = default!;
    private List<PrezzoServizioDto> elements = [];

    private bool isLoading = true;
    private bool isEditing;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var prezziServizi = await PrezzoServizioService.GetAllPrezziServiziAsync();
            elements = prezziServizi.ToList();
        }
        //catch (KeyNotFoundException ex)
        //{
        //    Logger.LogWarning(ex, "Nessun prezzo servizio trovato.");
        //    Snackbar.Add("Nessun prezzo servizio trovato.", Severity.Warning);
        //}
        catch (Exception)
        {
            Snackbar.Add("Errore durante il caricamento dei prezzi.", Severity.Error);
        }
        finally
        {
            isLoading = false;
        }
    }

    private void StartedEditingItem(PrezzoServizioDto item)
    {
        isEditing = true;
    }

    private void CanceledEditingItem(PrezzoServizioDto item)
    {
        isEditing = false;
    }

    private async Task<DataGridEditFormAction> CommittedItemChangesAsync(PrezzoServizioDto item)
    {
        if (!ValidatePrezzoServizio(item))
        {
            return DataGridEditFormAction.KeepOpen;
        }

        var updatedItem = new UpdatePrezzoServizioDto(item.Id, item.TipologiaServizioId, item.CostoFisso, item.CostoKm, item.SecondoTrasportato, item.FermoMacchina, item.Accompagnatore, item.ScontoSocio);

        try
        {
            await PrezzoServizioService.UpdatePrezzoServizioAsync(updatedItem);

            isEditing = false;

            Snackbar.Add("I prezzi del servizio aggiornati con successo.", Severity.Success);
            return DataGridEditFormAction.Close;
        }
        catch (Exception ex)
        {
            var correlationId = Guid.NewGuid().ToString();

            Logger.LogError(ex, "Errore durante l'aggiornamento. {NewLine} ID: {CorrelationId} {NewLine} Message: {Message}", Environment.NewLine, correlationId, Environment.NewLine, ex.Message);
            Snackbar.Add($"Errore durante l'aggiornamento. ID: {correlationId}", Severity.Error);

            return DataGridEditFormAction.KeepOpen;
        }
    }

    #region "Validazione"

    private void ValidateCostoFisso(decimal value, PrezzoServizioDto item)
    {
        if (IsNegative(value, out var msg))
        {
            Snackbar.Add(msg ?? "Il costo fisso non può essere negativo.", Severity.Warning);
        }
    }

    private void ValidateCostoKm(decimal value, PrezzoServizioDto item)
    {
        if (IsNegative(value, out var msg))
        {
            Snackbar.Add(msg ?? "Il costo per km non può essere negativo.", Severity.Warning);
        }
    }

    private void ValidateSecondoTrasportato(decimal value, PrezzoServizioDto item)
    {
        if (IsNegative(value, out var msg))
        {
            Snackbar.Add(msg ?? "Il costo per il secondo trasportato non può essere negativo.", Severity.Warning);
        }
    }

    private void ValidateFermoMacchina(decimal value, PrezzoServizioDto item)
    {
        if (IsNegative(value, out var msg))
        {
            Snackbar.Add(msg ?? "Il costo del fermo macchina non può essere negativo.", Severity.Warning);
        }
    }

    private void ValidateAccompagnatore(decimal? value, PrezzoServizioDto item)
    {
        if (IsNegative(value, out var msg))
        {
            Snackbar.Add(msg ?? "Il costo per l'accompagnatore non può essere negativo.", Severity.Warning);
        }
    }

    private void ValidateScontoSocio(int? value, PrezzoServizioDto item)
    {
        if (IsNegative(value, out var msg))
        {
            Snackbar.Add(msg ?? "Lo sconto socio non può essere negativo.", Severity.Warning);
        }
    }

    private static bool IsNegative(decimal? value, out string? message)
    {
        message = null;

        if (value.HasValue && value.Value < 0)
        {
            return true;
        }

        return false;
    }

    private bool ValidatePrezzoServizio(PrezzoServizioDto item)
    {
        if (item == null)
        {
            return false;
        }

        var checks = new (decimal? Value, string Message)[]
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
            if (IsNegative(value, out _))
            {
                Snackbar.Add(message, Severity.Warning);
                return false;
            }
        }

        return true;
    }

    #endregion

}