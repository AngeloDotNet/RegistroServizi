using System.Linq.Expressions;

namespace RegistroServizi.Web.Components.Shared;

//public partial class TextFieldNumber
public partial class TextFieldNumber<TValue>
{
    [Parameter] public string LabelText { get; set; } = string.Empty;
    [Parameter] public string HelperText { get; set; } = string.Empty;
    [Parameter] public string RequiredErrorText { get; set; } = string.Empty;
    [Parameter] public string DefaultMask { get; set; } = string.Empty;

    [Parameter] public TValue BindValueText { get; set; } = default!;
    [Parameter] public EventCallback<TValue> BindValueTextChanged { get; set; }
    [Parameter] public Expression<Func<TValue>>? BindValueTextExpression { get; set; }

    [Parameter] public bool IsMasked { get; set; } = false;
    [Parameter] public bool IsRequired { get; set; } = false;
}