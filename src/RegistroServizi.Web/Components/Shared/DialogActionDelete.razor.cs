using MudBlazor;

namespace RegistroServizi.Web.Components.Shared;

public partial class DialogActionDelete
{
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = default!;

    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string Description { get; set; } = string.Empty;
    [Parameter] public string BtnCancelText { get; set; } = string.Empty;
    [Parameter] public string BtnConfirmText { get; set; } = string.Empty;

    [Parameter] public Color BtnCancelColor { get; set; }
    [Parameter] public Color BtnConfirmColor { get; set; }

    private void Cancel() => MudDialog.Cancel();

    //TODO: Fix the issue with the MudDialog.Close() method not returning a value to the caller.
    //The caller should be able to know if the user confirmed or canceled the action.
    private void SubmitForm() => MudDialog.Close(DialogResult.Ok(true));
}