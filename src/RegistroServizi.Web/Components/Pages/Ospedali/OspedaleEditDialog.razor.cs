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
    //private int cap = 0;
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
                    strada = item.Indirizzo.Strada;
                    citta = item.Indirizzo.Citta;
                    provincia = item.Indirizzo.Provincia;
                    cap = item.Indirizzo.Cap;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"{Localizer["LoadingIdError"]}: {EditDialogId}. {Localizer["Message"]}: {ex.Message}");
                //Snackbar.Add($"{Localizer["LoadingIdError"]}: {EditDialogId}.", Severity.Error);
                SnackbarExtensions.ShowNotifyError(Snackbar, $"{Localizer["LoadingIdError"]}: {EditDialogId}.");
            }
        }
    }

    private async Task<bool> SubmitForm()
    {
        await form.ValidateAsync();

        if (!isValid)
        {
            //Snackbar.Add($"{Localizer["AllRequiredFields"]} !", Severity.Error);

            // Show a notification error message using the SnackbarExtensions
            //SnackbarExtensions.ShowNotifyError(Snackbar, $"{Localizer["AllRequiredFields"]} !");
            return false;
        }

        try
        {
            if (EditDialogId.HasValue)
            {
                var indirizzo = new UpdateIndirizzoDto(strada, citta, provincia, cap ?? 0);
                var updateItem = new UpdateOspedaleDto(EditDialogId.Value, nomeOspedale, indirizzo);

                await OspedaleService.UpdateOspedaleAsync(updateItem);
                //Snackbar.Add($"{Localizer["EntityUpdateSuccess"]} !", Severity.Success);
                SnackbarExtensions.ShowNotifySuccess(Snackbar, $"{Localizer["EntityUpdateSuccess"]} !");
            }
            else
            {
                var indirizzo = new CreateIndirizzoDto(strada, citta, provincia, cap ?? 0);
                var createItem = new CreateOspedaleDto(nomeOspedale, indirizzo);

                await OspedaleService.CreateOspedaleAsync(createItem);
                //Snackbar.Add($"{Localizer["EntityCreateSuccess"]} !", Severity.Success);
                SnackbarExtensions.ShowNotifySuccess(Snackbar, $"{Localizer["EntityCreateSuccess"]} !");
            }

            MudDialog.Close(DialogResult.Ok(true));
            return true;
        }
        catch (Exception ex)
        {
            //Snackbar.Add($"{Localizer["ErrorOccurred"]}: {ex.Message}", Severity.Error);
            SnackbarExtensions.ShowNotifyError(Snackbar, $"{Localizer["ErrorOccurred"]}: {ex.Message}");
            return false;
        }
    }

    private void Cancel() => MudDialog.Cancel();
}