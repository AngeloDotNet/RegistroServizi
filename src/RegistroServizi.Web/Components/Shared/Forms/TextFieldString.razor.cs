namespace RegistroServizi.Web.Components.Shared.Forms;

/// <summary>
/// Provides a reusable text input component for editing string values with optional
/// validation, helper text, placeholder text, and maximum-length enforcement.
/// </summary>
/// <remarks>
/// When <see cref="MaxLengthText"/> is greater than zero, the input displays a character
/// counter and prevents values from exceeding the configured maximum length.
/// </remarks>
public partial class TextFieldString
{
    /// <summary>
    /// Gets or sets the label displayed for the text input.
    /// </summary>
    [Parameter] public string LabelText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the helper text displayed for the text input when it receives focus.
    /// </summary>
    [Parameter] public string HelperText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the validation message displayed when a required value is missing.
    /// </summary>
    [Parameter] public string RequiredErrorText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the placeholder displayed when the input does not contain a value.
    /// </summary>
    [Parameter] public string PlaceholderText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the string value displayed and edited by the text input.
    /// </summary>
    [Parameter] public string BindValueText { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the callback invoked when the text input value changes.
    /// </summary>
    [Parameter] public EventCallback<string> BindValueTextChanged { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of characters allowed in the input.
    /// </summary>
    /// <remarks>
    /// A value less than or equal to zero disables maximum-length enforcement and the
    /// character counter.
    /// </remarks>
    [Parameter] public int MaxLengthText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the input must contain a value.
    /// </summary>
    [Parameter] public bool IsRequired { get; set; } = false;
}