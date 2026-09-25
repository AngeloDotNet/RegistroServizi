namespace RegistroServizi.Web.Components.Shared;

public partial class TextFieldString
{
    [Parameter] public string LabelText { get; set; } = string.Empty;
    [Parameter] public string HelperText { get; set; } = string.Empty;
    [Parameter] public string RequiredErrorText { get; set; } = string.Empty;

    //[Parameter] public string BindValueText { get; set; } = string.Empty;
    [Parameter] public string BindValueText { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> BindValueTextChanged { get; set; }

    [Parameter] public int MaxLengthText { get; set; }

    [Parameter] public bool IsRequired { get; set; } = false;
}