namespace RegistroServizi.Web.Components.Shared.Miscellaneous;

/// <summary>
/// Displays a loading indicator while data is being retrieved and an informational
/// message when the completed operation produces no elements.
/// </summary>
/// <remarks>
/// The component renders nothing after loading completes when <see cref="ElementsCount"/>
/// is greater than zero. The accompanying <c>ProgressNoData.razor</c> markup determines
/// which state is displayed.
/// </remarks>
public partial class ProgressNoData
{
    /// <summary>
    /// Gets or sets a value indicating whether the associated data is still loading.
    /// </summary>
    /// <value>
    /// <see langword="true"/> to display an indeterminate progress indicator;
    /// otherwise, <see langword="false"/>.
    /// </value>
    [Parameter] public bool IsLoading { get; set; }

    /// <summary>
    /// Gets or sets the number of elements returned by the completed data operation.
    /// </summary>
    /// <value>
    /// The number of elements available to display.
    /// </value>
    [Parameter] public int ElementsCount { get; set; }

    /// <summary>
    /// Gets or sets the informational message displayed when no elements are available.
    /// </summary>
    /// <value>
    /// The message shown when <see cref="IsLoading"/> is <see langword="false"/> and
    /// <see cref="ElementsCount"/> is zero.
    /// </value>
    [Parameter] public string CountMessage { get; set; } = string.Empty;
}