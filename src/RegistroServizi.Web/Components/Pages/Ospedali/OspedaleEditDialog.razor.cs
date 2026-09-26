using MudBlazor;

namespace RegistroServizi.Web.Components.Pages.Ospedali;

public partial class OspedaleEditDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter] public Guid? EditDialogId { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string BtnCancelText { get; set; } = string.Empty;
    [Parameter] public Color BtnCancelColor { get; set; }
    [Parameter] public Color BtnConfirmColor { get; set; }

    private MudForm form = null!;
    private bool isValid = false;

    private string nomeOspedale = string.Empty;
    private string strada = string.Empty;
    private string citta = string.Empty;
    private string provincia = string.Empty;
    private int? cap = null;

    protected override async Task OnInitializedAsync()
    {
        if (EditDialogId.HasValue)
        {
            try
            {
                var ospedale = await OspedaleService.GetOspedaleByIdAsync(EditDialogId.Value);

                if (ospedale != null)
                {
                    nomeOspedale = ospedale.NomeOspedale;
                    strada = ospedale.Indirizzo.Strada;
                    citta = ospedale.Indirizzo.Citta;
                    provincia = ospedale.Indirizzo.Provincia;
                    cap = ospedale.Indirizzo.Cap;
                }
            }
            catch (Exception ex)
            {
                //Logger.LogError(ex, $"Errore durante il caricamento dell'ospedale con ID {EditDialogId}. Message: {ex.Message}", EditDialogId, ex.Message);
                Logger.LogError(ex, $"{Localizer["LoadingIdError"]}: {EditDialogId}. {Localizer["Message"]}: {ex.Message}");
                Snackbar.Add($"{Localizer["LoadingIdError"]}: {EditDialogId}.", Severity.Error);
            }
        }
    }

    private async Task<bool> SubmitForm()
    {
        await form.ValidateAsync();

        if (!isValid)
        {
            Snackbar.Add($"{Localizer["AllRequiredFields"]} !", Severity.Error);
            return false;
        }

        try
        {
            if (EditDialogId.HasValue)
            {
                var indirizzo = new UpdateIndirizzoDto(strada, citta, provincia, cap ?? 0);
                var updateItem = new UpdateOspedaleDto(EditDialogId.Value, nomeOspedale, indirizzo);

                await OspedaleService.UpdateOspedaleAsync(updateItem);
                //Snackbar.Add("Ospedale aggiornato con successo!", Severity.Success);
                Snackbar.Add($"{Localizer["EntityUpdateSuccess"]} !", Severity.Success);
            }
            else
            {
                var indirizzo = new CreateIndirizzoDto(strada, citta, provincia, cap ?? 0);
                var createItem = new CreateOspedaleDto(nomeOspedale, indirizzo);

                await OspedaleService.CreateOspedaleAsync(createItem);
                //Snackbar.Add("Ospedale creato con successo!", Severity.Success);
                Snackbar.Add($"{Localizer["EntityCreateSuccess"]} !", Severity.Success);
            }

            MudDialog.Close(DialogResult.Ok(true));
            return true;
        }
        catch (Exception ex)
        {
            //Snackbar.Add($"Si è verificato un errore: {ex.Message}", Severity.Error);
            Snackbar.Add($"{Localizer["ErrorOccurred"]}: {ex.Message}", Severity.Error);

            return false;
        }
    }

    private void Cancel() => MudDialog.Cancel();
}