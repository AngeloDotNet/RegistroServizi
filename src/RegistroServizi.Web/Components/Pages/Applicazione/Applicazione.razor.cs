using System.Text.RegularExpressions;
using MudBlazor;

namespace RegistroServizi.Web.Components.Pages.Applicazione;

public partial class Applicazione
{
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    [Inject] private IApplicazioneService ApplicazioneService { get; set; } = default!;
    [Inject] private ILogger<Applicazione> Logger { get; set; } = default!;

    private MudDataGrid<ApplicazioneDto> dataGrid = default!;
    private List<ApplicazioneDto> elements = [];

    private bool isLoading = true;
    private bool isEditing;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var applicazione = await ApplicazioneService.GetAllApplicazioniAsync();
            elements = applicazione.ToList();
        }
        catch (Exception)
        {
            Snackbar.Add("Errore durante il caricamento delle applicazioni.", Severity.Error);
        }
        finally
        {
            isLoading = false;
        }
    }

    private void StartedEditingItem(ApplicazioneDto item)
    {
        isEditing = true;
    }

    private void CanceledEditingItem(ApplicazioneDto item)
    {
        isEditing = false;
    }

    private async Task<DataGridEditFormAction> CommittedItemChangesAsync(ApplicazioneDto item)
    {
        if (!ValidateApplicazione(item))
        {
            return DataGridEditFormAction.KeepOpen;
        }

        var updatedItem = new UpdateApplicazioneDto(item.Id, item.NomeApplicazione, item.Versione, item.TimeZone);

        try
        {
            await ApplicazioneService.UpdateApplicazioneAsync(updatedItem);

            isEditing = false;

            Snackbar.Add("L'applicazione è stata aggiornata con successo.", Severity.Success);
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

    private void ValidateNomeApplicazione(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Snackbar.Add("Il nome dell'applicazione non può essere vuoto.", Severity.Warning);
        }
    }

    private static readonly Regex versionRegex = new(@"^\d+(\.\d+)*$", RegexOptions.Compiled);

    private void ValidateVersione(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Snackbar.Add("La versione dell'applicazione non può essere vuota.", Severity.Warning);
            return;
        }

        if (!versionRegex.IsMatch(value))
        {
            Snackbar.Add("Formato versione non valido. Usa ad esempio 1.0.0.", Severity.Warning);
        }
    }

    private void ValidateTimeZone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Snackbar.Add("Il fuso orario non può essere vuoto.", Severity.Warning);
        }
    }

    private bool ValidateApplicazione(ApplicazioneDto item)
    {
        if (item == null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(item.NomeApplicazione))
        {
            Snackbar.Add("Il nome dell'applicazione non può essere vuoto.", Severity.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(item.Versione) || !versionRegex.IsMatch(item.Versione))
        {
            Snackbar.Add("La versione dell'applicazione non è valida. Usa ad esempio 1.0.0.", Severity.Warning);
            return false;
        }

        if (string.IsNullOrWhiteSpace(item.TimeZone))
        {
            Snackbar.Add("Il fuso orario non può essere vuoto.", Severity.Warning);
            return false;
        }

        return true;
    }
}