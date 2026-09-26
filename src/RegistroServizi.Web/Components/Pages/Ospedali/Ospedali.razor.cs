using MudBlazor;

namespace RegistroServizi.Web.Components.Pages.Ospedali;

public partial class Ospedali
{
    [Inject] private ISnackbar Snackbar { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IOspedaleService OspedaleService { get; set; } = default!;
    [Inject] private ILogger<Ospedali> Logger { get; set; } = default!;

    private readonly CancellationTokenSource cts = new CancellationTokenSource();
    private List<OspedaleDto> elements = [];
    private bool isLoading = true;
    private string Title => Localizer["Hospital"];
    private string CancelText => Localizer["Cancel"];
    //private string ConfirmText => Localizer["Confirm"];

    protected override async Task OnInitializedAsync() => await LoadingDataAsync();

    private async Task LoadingDataAsync()
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
            //Logger.LogError(ResourceHelper.LogErrorMessage(resourceKey: Localizer["LoadingError"], resourceValue: Localizer["Message"], message: ex.Message));
            //await InvokeAsync(() => Snackbar.Add(Localizer["LoadingError"], Severity.Error));

            Logger.LogError(ex, $"{Localizer["LoadingError"]}. {Localizer["Message"]}: {ex.Message}");
            Snackbar.Add($"{Localizer["LoadingError"]}.", Severity.Error);

            //Logger.LogError(ex, "{LoadingError}. Message: {Message}", ErrorLoading, ex.Message);
            //await InvokeAsync(() => Snackbar.Add(ErrorLoading, Severity.Error));
        }
        finally
        {
            await InvokeAsync(() => isLoading = false);
        }
    }

    private async Task CreateItemAsync()
    {
        var dialogReference = await DialogService.ShowAsync<OspedaleEditDialog>(
            MudBlazorDialogParameters.GetCreateItemDialogParameters<OspedaleEditDialog>(title: Title, cancelText: CancelText),
            MudBlazorDialogOptions.GetBackdropFilterDialogOptions());

        var result = await dialogReference.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadingDataAsync();
        }
        else
        {
            //await InvokeAsync(() => Snackbar.Add("Creazione annullata.", Severity.Info));
            await InvokeAsync(() => Snackbar.Add(Localizer["CancelCreate"], Severity.Info));
        }
    }

    private async Task EditItemAsync(OspedaleDto item)
    {
        var dialogReference = await DialogService.ShowAsync<OspedaleEditDialog>(
            MudBlazorDialogParameters.GetEditItemDialogParameters<OspedaleEditDialog>(editDialogId: item.Id, title: Title, cancelText: CancelText),
            MudBlazorDialogOptions.GetBackdropFilterDialogOptions());

        var result = await dialogReference.Result;

        if (result is not null && !result.Canceled)
        {
            await LoadingDataAsync();
        }
        else
        {
            //await InvokeAsync(() => Snackbar.Add("Modifica annullata.", Severity.Info));
            await InvokeAsync(() => Snackbar.Add(Localizer["CancelModify"], Severity.Info));
        }
    }

    //private async Task DeleteItem(OspedaleDto item)
    //{
    //    //return DialogService?.ShowAsync<OspedaleDeleteDialog>(
    //    //    MudBlazorDialogParameters.GetDeleteItemDialogParameters<OspedaleDeleteDialog>(title: Title, description: item.NomeOspedale, cancelText: CancelText, confirmText: ConfirmText),
    //    //    MudBlazorDialogOptions.GetBackdropFilterDialogOptions()) ?? Task.CompletedTask;

    //    var result = await DialogService?.ShowAsync<OspedaleDeleteDialog>(
    //        MudBlazorDialogParameters.GetDeleteItemDialogParameters<OspedaleDeleteDialog>(editDialogId: item.Id, title: Title, description: item.NomeOspedale, cancelText: CancelText, confirmText: ConfirmText),
    //        MudBlazorDialogOptions.GetBackdropFilterDialogOptions());

    //    if (result is { Canceled: false })
    //    {
    //        elements.Remove(item);

    //        var ospedali = await OspedaleService.GetAllOspedaliAsync().ConfigureAwait(false);

    //        if (cts.IsCancellationRequested)
    //        {
    //            return;
    //        }

    //        await InvokeAsync(() =>
    //        {
    //            elements = ospedali?.ToList() ?? [];
    //        });
    //    }
    //    else
    //    {
    //        await InvokeAsync(() => Snackbar.Add("Eliminazione annullata.", Severity.Info));
    //    }
    //}

    private async Task DeleteItemAsync(OspedaleDto item)
    {
        var dialogReference = await DialogService.ShowAsync<OspedaleDeleteDialog>(
            MudBlazorDialogParameters.GetDeleteItemDialogParameters<OspedaleDeleteDialog>(editDialogId: item.Id,
                title: Title,
                description: item.NomeOspedale,
                cancelText: CancelText,
                confirmText: Localizer["Confirm"]), MudBlazorDialogOptions.GetBackdropFilterDialogOptions());

        var result = await dialogReference.Result;

        if (result is not null && !result.Canceled)
        {
            elements.Remove(item);
            await LoadingDataAsync();
        }
        else
        {
            //await InvokeAsync(() => Snackbar.Add("Eliminazione annullata.", Severity.Info));
            await InvokeAsync(() => Snackbar.Add(Localizer["CancelDelete"], Severity.Info));
        }
    }

    //private async Task DeleteItem(OspedaleDto item)
    //{
    //    var result = await DialogService.ShowMessageBoxAsync(Title, //string.Format(L["Accounts_ConfirmDeleteMessage"], account.Name),
    //        string.Format("Sei sicuro di voler eliminare {0} ? Questa azione non può essere annullata.", item.NomeOspedale),
    //        yesText: ConfirmText, cancelText: CancelText);

    //    //if (result == true)
    //    //{
    //    //    try
    //    //    {
    //    //        await OspedaleService.DeleteOspedaleAsync(item.Id).ConfigureAwait(false);
    //    //        Snackbar.Add($"L'ospedale '{item.NomeOspedale}' è stato eliminato con successo.", Severity.Success);

    //    //        elements.Remove(item);
    //    //        await InvokeAsync(() => dataGrid.ReloadServerData());
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        var correlationId = Guid.NewGuid().ToString();
    //    //        Logger.LogError(ex, "Errore durante l'eliminazione dell'ospedale. ID: {CorrelationId}. Message: {Message}", correlationId, ex.Message);
    //    //        await InvokeAsync(() => Snackbar.Add($"Errore durante l'eliminazione dell'ospedale. ID: {correlationId}", Severity.Error));
    //    //    }
    //    //}

    //    //if (result == true && _userId != null)
    //    //{
    //    //    var deleted = await AccountService.DeleteAccountAsync(account.Id, _userId);

    //    //    if (deleted)
    //    //    {
    //    //        await LoadAccounts();
    //    //        SnackbarService.Add(L["Accounts_DeletedSuccess"], Severity.Success);
    //    //    }
    //    //    else
    //    //    {
    //    //        SnackbarService.Add(L["Accounts_DeleteFailed"], Severity.Error);
    //    //    }
    //    //}
    //}

    //private void StartedEditingItem(OspedaleDto item) => isEditing = true;
    //private void CanceledEditingItem(OspedaleDto item) => isEditing = false;

    //private async Task<DataGridEditFormAction> CommittedItemChangesAsync(OspedaleDto item)
    //{
    //    if (!ValidateOspedale(item))
    //    {
    //        return DataGridEditFormAction.KeepOpen;
    //    }

    //    var updateItemIndirizzo = new UpdateIndirizzoDto(item.Indirizzo.Strada, item.Indirizzo.Citta, item.Indirizzo.Provincia, item.Indirizzo.Cap);
    //    var updatedItem = new UpdateOspedaleDto(item.Id, item.NomeOspedale, updateItemIndirizzo);

    //    try
    //    {
    //        await OspedaleService.UpdateOspedaleAsync(updatedItem).ConfigureAwait(false);

    //        if (cts.IsCancellationRequested)
    //        {
    //            return DataGridEditFormAction.KeepOpen;
    //        }

    //        await InvokeAsync(() => isEditing = false);
    //        await InvokeAsync(() => Snackbar.Add("L'ospedale è stato aggiornato con successo.", Severity.Success));

    //        return DataGridEditFormAction.Close;
    //    }
    //    catch (Exception ex)
    //    {
    //        var correlationId = Guid.NewGuid().ToString();

    //        Logger.LogError(ex, "Errore durante l'aggiornamento. ID: {CorrelationId}. Message: {Message}", correlationId, ex.Message);
    //        await InvokeAsync(() => Snackbar.Add($"Errore durante l'aggiornamento. ID: {correlationId}", Severity.Error));

    //        return DataGridEditFormAction.KeepOpen;
    //    }
    //}

    //private async void DeleteItem(OspedaleDto item)
    //{
    //    if (item.Id == Guid.Empty)
    //    {
    //        Snackbar.Add("L'ospedale non ha un ID valido e non può essere eliminato.", Severity.Warning);
    //        return;
    //    }

    //    try
    //    {
    //        await OspedaleService.DeleteOspedaleAsync(item.Id).ConfigureAwait(false);
    //        Snackbar.Add("L'ospedale è stato eliminato con successo.", Severity.Success);
    //    }
    //    catch (Exception ex)
    //    {
    //        var correlationId = Guid.NewGuid().ToString();

    //        Logger.LogError(ex, "Errore durante l'eliminazione. ID: {CorrelationId}. Message: {Message}", correlationId, ex.Message);
    //        Snackbar.Add($"Errore durante l'eliminazione. ID: {correlationId}", Severity.Error);
    //    }
    //}

    //private void ValidateNomeOspedale(string value)
    //    => MudblazorValidator.ValidateIsNotNullOrWhiteSpace(Snackbar, value, "Il nome dell'ospedale non può essere vuoto.");

    //private void ValidateStrada(string value)
    //    => MudblazorValidator.ValidateIsNotNullOrWhiteSpace(Snackbar, value, "Il nome della strada non può essere vuoto.");

    //private void ValidateCitta(string value)
    //    => MudblazorValidator.ValidateIsNotNullOrWhiteSpace(Snackbar, value, "Il nome della città non può essere vuoto.");

    //private void ValidateProvincia(string value)
    //    => MudblazorValidator.ValidateIsNotNullOrWhiteSpace(Snackbar, value, "Il nome della provincia non può essere vuoto e deve essere di 2 caratteri.");

    //private void ValidateCap(int value)
    //{
    //    if (value < MudblazorValidator.minCap || value > MudblazorValidator.maxCap)
    //    {
    //        Snackbar.Add("Il codice avviamento postale deve essere un numero di 5 cifre.", Severity.Warning);
    //    }
    //}

    //private bool ValidateOspedale(OspedaleDto item)
    //{
    //    if (item == null || item.Indirizzo == null)
    //    {
    //        return false;
    //    }

    //    var addr = item.Indirizzo;
    //    bool IsNullOrWhite(string s) => string.IsNullOrWhiteSpace(s);

    //    var validations = new (bool Invalid, string Message)[]
    //    {
    //        (IsNullOrWhite(item.NomeOspedale), "Il nome dell'ospedale non può essere vuoto."),
    //        (IsNullOrWhite(addr.Strada), "Il nome della strada non può essere vuoto."),
    //        (IsNullOrWhite(addr.Citta), "Il nome della città non può essere vuoto."),
    //        (IsNullOrWhite(addr.Provincia) || addr.Provincia.Length != 2, "Il nome della provincia non può essere vuoto e deve essere di 2 caratteri."),
    //        (addr.Cap < MudblazorValidator.minCap || addr.Cap > MudblazorValidator.maxCap, "Il codice avviamento postale deve essere un numero di 5 cifre.")
    //    };

    //    foreach (var v in validations)
    //    {
    //        if (v.Invalid)
    //        {
    //            Snackbar.Add(v.Message, Severity.Warning);
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    //public void Dispose()
    //{
    //    if (!cts.IsCancellationRequested)
    //    {
    //        cts.Cancel();
    //    }

    //    cts.Dispose();
    //}
}