namespace RegistroServizi.Web.Common.Helpers;

/// <summary>
/// Provides the localized labels and format strings required by data grid components.
/// </summary>
/// <remarks>
/// Centralizing these strings ensures that data grid controls use the shared localization
/// resources consistently across the application.
/// </remarks>
/// <param name="localizer">
/// The localizer used to resolve the data grid strings from <see cref="SharedResource" />.
/// </param>
public sealed class DataGridHelper(IStringLocalizer<SharedResource> localizer)
{
    /// <summary>
    /// Gets the localized label for the cancel action.
    /// </summary>
    /// <value>
    /// The localized value associated with the <c>Cancel</c> resource key.
    /// </value>
    public string CancelText => localizer["Cancel"];

    /// <summary>
    /// Gets the localized label for the confirm action.
    /// </summary>
    /// <value>
    /// The localized value associated with the <c>Confirm</c> resource key.
    /// </value>
    public string ConfirmText => localizer["Confirm"];

    /// <summary>
    /// Gets the localized label displayed for the rows-per-page selector.
    /// </summary>
    /// <value>
    /// The localized value associated with the <c>RowsPerPage</c> resource key.
    /// </value>
    public string RowsPerPageString => localizer["RowsPerPage"];

    /// <summary>
    /// Gets the localized format string used to display the current rows and total row count.
    /// </summary>
    /// <remarks>
    /// The format string receives the <c>{first_item}</c>, <c>{last_item}</c>, and
    /// <c>{all_items}</c> placeholders used by the data grid to display the visible
    /// item range and total number of items.
    /// </remarks>
    /// <value>
    /// The localized value associated with the <c>RowsPerPageInfoFormat</c> resource key.
    /// </value>
    public string RowsPerPageInfoFormat => localizer["RowsPerPageInfoFormat", "{first_item}", "{last_item}", "{all_items}"];
}