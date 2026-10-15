using System.Linq.Expressions;

namespace RegistroServizi.Web.Components.Shared.Forms;

/// <summary>
/// Provides the parameters for a reusable, strongly typed text field component
/// that supports optional masking, validation, helper text, and two-way binding.
/// </summary>
/// <typeparam name="TValue">
/// The type of the value edited by the text field.
/// </typeparam>
public partial class TextFieldNumber<TValue>
{
    /// <summary>
    /// Gets or sets the label displayed for the field.
    /// </summary>
    [Parameter] public string LabelText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the helper text displayed below the field when it receives focus.
    /// </summary>
    [Parameter] public string HelperText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the validation message displayed when the field is required
    /// but has no value.
    /// </summary>
    [Parameter] public string RequiredErrorText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the pattern used to constrain the field value when masking is enabled.
    /// </summary>
    /// <remarks>
    /// This value is passed to <c>PatternMask</c> and is used only when
    /// <see cref="IsMasked" /> is <see langword="true" />.
    /// </remarks>
    [Parameter] public string DefaultMask { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the placeholder displayed when the field has no value.
    /// </summary>
    [Parameter] public string PlaceholderText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current value of the field.
    /// </summary>
    /// <remarks>
    /// Use this property together with <see cref="BindValueTextChanged" /> and
    /// <see cref="BindValueTextExpression" /> to support strongly typed two-way binding.
    /// </remarks>
    [Parameter] public TValue BindValueText { get; set; } = default!;

    /// <summary>
    /// Gets or sets the callback invoked when <see cref="BindValueText" /> changes.
    /// </summary>
    [Parameter] public EventCallback<TValue> BindValueTextChanged { get; set; }

    /// <summary>
    /// Gets or sets the expression that identifies the bound value for validation.
    /// </summary>
    /// <remarks>
    /// Supplying the expression allows the surrounding Blazor form to associate
    /// validation messages with the bound field.
    /// </remarks>
    [Parameter] public Expression<Func<TValue>>? BindValueTextExpression { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the field applies the pattern
    /// specified by <see cref="DefaultMask" />.
    /// </summary>
    [Parameter] public bool IsMasked { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether the field requires a value.
    /// </summary>
    [Parameter] public bool IsRequired { get; set; } = false;

    [Parameter] public bool IsCurrency { get; set; } = false;
    [Parameter] public CultureInfo LocalizationCulture { get; set; } = CultureInfo.GetCultureInfo("en-US");
}