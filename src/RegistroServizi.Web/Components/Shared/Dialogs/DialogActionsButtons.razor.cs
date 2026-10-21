using MudBlazor;

namespace RegistroServizi.Web.Components.Shared.Dialogs;

/// <summary>
/// Provides the cancel and submit actions displayed at the bottom of a dialog.
/// </summary>
/// <remarks>
/// The component renders two filled MudBlazor buttons and exposes callbacks,
/// labels, icons, and colors so that consuming dialogs can configure their actions
/// without duplicating the button layout.
/// </remarks>
public partial class DialogActionsButtons
{
    /// <summary>
    /// Gets or sets the callback invoked when the cancel button is clicked.
    /// </summary>
    [Parameter] public EventCallback OnCancel { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the submit button is clicked.
    /// </summary>
    [Parameter] public EventCallback OnSubmit { get; set; }

    /// <summary>
    /// Gets or sets the text displayed on the cancel button.
    /// </summary>
    [Parameter] public string BtnCancelText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the text displayed on the submit button.
    /// </summary>
    [Parameter] public string BtnSubmitText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the icon displayed before the cancel button text.
    /// </summary>
    [Parameter] public string BtnCancelIcon { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the icon displayed before the submit button text.
    /// </summary>
    [Parameter] public string BtnSubmitIcon { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the MudBlazor color applied to the cancel button.
    /// </summary>
    [Parameter] public Color BtnCancelColor { get; set; }

    /// <summary>
    /// Gets or sets the MudBlazor color applied to the submit button.
    /// </summary>
    [Parameter] public Color BtnConfirmColor { get; set; }
}