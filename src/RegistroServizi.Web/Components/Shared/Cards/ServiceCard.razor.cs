namespace RegistroServizi.Web.Components.Shared.Cards;

/// <summary>
/// Represents a reusable Blazor component that displays a service summary as an interactive card.
/// </summary>
/// <remarks>
/// The card presents an icon, title, and subtitle, and notifies its parent component when selected.
/// The parent component can use <see cref="OnClick"/> to perform navigation or another service-related action.
/// </remarks>
public partial class ServiceCard
{
    /// <summary>
    /// Gets or sets the primary text displayed on the card.
    /// </summary>
    /// <value>
    /// The service title. The default value is an empty string.
    /// </value>
    [Parameter]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the secondary text displayed below the title.
    /// </summary>
    /// <value>
    /// The service subtitle. The default value is an empty string.
    /// </value>
    [Parameter]
    public string Subtitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the icon identifier rendered by the MudBlazor icon component.
    /// </summary>
    /// <value>
    /// A MudBlazor-compatible icon identifier. The default value is an empty string.
    /// </value>
    [Parameter]
    public string IconName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the callback invoked when the card is clicked.
    /// </summary>
    /// <value>
    /// An <see cref="EventCallback"/> without event arguments that allows the parent component
    /// to respond to the card selection.
    /// </value>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>
    /// Forwards the card activation to the parent component through the configured callback.
    /// </summary>
    /// <returns>
    /// A task representing the asynchronous callback invocation.
    /// </returns>
    private async Task HandleClickAsync() => await OnClick.InvokeAsync();
}