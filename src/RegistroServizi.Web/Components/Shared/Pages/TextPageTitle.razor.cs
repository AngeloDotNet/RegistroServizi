namespace RegistroServizi.Web.Components.Shared.Pages;

/// <summary>
/// Displays a page title and optional subtitle in the shared section-header layout.
/// </summary>
/// <remarks>
/// The title is rendered both as the browser page title, with the application name appended,
/// and as the visible heading. The subtitle is rendered beneath the heading when provided.
/// </remarks>
public partial class TextPageTitle
{
    /// <summary>
    /// Gets or sets the primary title displayed for the page.
    /// </summary>
    /// <value>
    /// The page title, or <see langword="null" /> when no title is specified.
    /// </value>
    [Parameter] public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the descriptive subtitle displayed below the page title.
    /// </summary>
    /// <value>
    /// The page subtitle, or <see langword="null" /> when no subtitle is specified.
    /// </value>
    [Parameter] public string? SubTitle { get; set; }
}