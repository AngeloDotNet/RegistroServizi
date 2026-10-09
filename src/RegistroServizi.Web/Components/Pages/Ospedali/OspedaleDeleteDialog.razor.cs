using MudBlazor;

namespace RegistroServizi.Web.Components.Pages.Ospedali;

public partial class OspedaleDeleteDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter] public Guid? EditDialogId { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string Description { get; set; } = string.Empty;
    [Parameter] public string BtnCancelText { get; set; } = string.Empty;
    [Parameter] public string BtnConfirmText { get; set; } = string.Empty;

    [Parameter] public Color BtnCancelColor { get; set; }
    [Parameter] public Color BtnConfirmColor { get; set; }

    private void Cancel() => MudDialog.Cancel();

    private async Task<bool> SubmitForm()
    {
        try
        {
            if (EditDialogId.HasValue)
            {
                await OspedaleService.DeleteOspedaleAsync(EditDialogId.Value);
                SnackbarExtensions.ShowNotifySuccess(Snackbar, $"{Localizer["EntityDeleteSuccess"]}");
            }
            else
            {
                SnackbarExtensions.ShowNotifyWarning(Snackbar, $"{Localizer["LoadingIdError"]}: {EditDialogId}.");
                return false;
            }

            MudDialog.Close(DialogResult.Ok(true));
            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"{Localizer["ExceptionIdError"]}: {EditDialogId}. {Localizer["Message"]}: {ex.Message}");
            SnackbarExtensions.ShowNotifyError(Snackbar, $"{Localizer["ExceptionIdError"]}: {EditDialogId}.");

            return false;
        }
    }
}