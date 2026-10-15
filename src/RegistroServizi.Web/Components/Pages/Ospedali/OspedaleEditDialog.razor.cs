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
    private string indirizzo = string.Empty;
    private string comune = string.Empty;
    private string provincia = string.Empty;
    private int? cap;

    protected override async Task OnInitializedAsync()
    {
        if (EditDialogId.HasValue)
        {
            try
            {
                var item = await OspedaleService.GetOspedaleByIdAsync(EditDialogId.Value);

                if (item != null)
                {
                    nomeOspedale = item.NomeOspedale;
                    indirizzo = item.Ubicazione.Indirizzo;
                    comune = item.Ubicazione.Comune;
                    provincia = item.Ubicazione.Provincia;
                    cap = item.Ubicazione.Cap;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{Localizer["LoadingIdError"]}: {EditDialogId}. {Localizer["Message"]}: {ex.Message}");
                MudSnackbarExtensions.ShowNotifyError(Snackbar, $"{Localizer["LoadingIdError"]}: {EditDialogId}.");
            }
        }
    }

    private async Task<bool> SubmitForm()
    {
        await form.ValidateAsync();

        if (!isValid)
        {
            // TODO: Remove the localizer and use the validation messages from the form instead
            // Show a notification error message using the SnackbarExtensions
            //SnackbarExtensions.ShowNotifyError(Snackbar, $"{Localizer["AllRequiredFields"]} !");
            return false;
        }

        try
        {
            if (EditDialogId.HasValue)
            {
                var ubicazione = new UpdateUbicazioneDto(indirizzo, comune, provincia, cap ?? 0);
                var updateItem = new UpdateOspedaleDto(EditDialogId.Value, nomeOspedale, ubicazione);

                await OspedaleService.UpdateOspedaleAsync(updateItem);
                MudSnackbarExtensions.ShowNotifySuccess(Snackbar, $"{Localizer["EntityUpdateSuccess"]} !");
            }
            else
            {
                var ubicazione = new CreateUbicazioneDto(indirizzo, comune, provincia, cap ?? 0);
                var createItem = new CreateOspedaleDto(nomeOspedale, ubicazione);

                await OspedaleService.CreateOspedaleAsync(createItem);
                MudSnackbarExtensions.ShowNotifySuccess(Snackbar, $"{Localizer["EntityCreateSuccess"]} !");
            }

            MudDialog.Close(DialogResult.Ok(true));
            return true;
        }
        catch (Exception ex)
        {
            MudSnackbarExtensions.ShowNotifyError(Snackbar, $"{Localizer["ErrorOccurred"]}: {ex.Message}");
            return false;
        }
    }

    private void Cancel() => MudDialog.Cancel();
}