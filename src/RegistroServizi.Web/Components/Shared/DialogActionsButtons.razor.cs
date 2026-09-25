using MudBlazor;

namespace RegistroServizi.Web.Components.Shared;

public partial class DialogActionsButtons
{
    [Parameter] public EventCallback OnCancel { get; set; }
    [Parameter] public EventCallback OnSubmit { get; set; }

    [Parameter] public string BtnCancelText { get; set; } = string.Empty;
    [Parameter] public string BtnSubmitText { get; set; } = string.Empty;

    [Parameter] public Color BtnCancelColor { get; set; }
    [Parameter] public Color BtnConfirmColor { get; set; }
}