using MudBlazor;

namespace RegistroServizi.Web.Components.Shared.Dialogs;

public partial class DeleteDialog
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter] public string Title { get; set; } = string.Empty;

    [Parameter] public string Description { get; set; } = string.Empty;

    [Parameter] public string BtnCancelText { get; set; } = string.Empty;

    [Parameter] public string BtnConfirmText { get; set; } = string.Empty;

    [Parameter] public Color BtnCancelColor { get; set; }

    [Parameter] public Color BtnConfirmColor { get; set; }

    // Azione specifica dell'entità chiamante
    [Parameter] public Func<Task>? DeleteAction { get; set; }

    private void Cancel()
    {
        MudDialog.Cancel();
    }

    private async Task<bool> SubmitForm()
    {
        try
        {
            if (DeleteAction is null)
            {
                MudSnackbarExtensions.ShowNotifyWarning(Snackbar, Localizer["DeleteActionNotConfigured"]);
                return false;
            }

            await DeleteAction();

            MudSnackbarExtensions.ShowNotifySuccess(Snackbar, Localizer["EntityDeleteSuccess"]);
            MudDialog.Close(DialogResult.Ok(true));

            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "{Exception}: {Message}", Localizer["ExceptionIdError"], ex.Message);
            MudSnackbarExtensions.ShowNotifyError(Snackbar, Localizer["EntityDeleteError"]);

            return false;
        }
    }
}