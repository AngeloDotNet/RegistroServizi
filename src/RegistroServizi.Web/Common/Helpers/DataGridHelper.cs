namespace RegistroServizi.Web.Common.Helpers;

/// <summary>
/// Provides localized text used by data grid UI elements.
/// </summary>
/// <param name="localizer">The localizer used to resolve shared UI strings.</param>
public sealed class DataGridHelper(IStringLocalizer<SharedResource> localizer)
{
    /// <summary>
    /// Gets the localized text for the cancel action.
    /// </summary>
    public string CancelText => localizer["Cancel"];

    /// <summary>
    /// Gets the localized text for the confirm action.
    /// </summary>
    public string ConfirmText => localizer["Confirm"];

    /// <summary>
    /// Gets the localized text for the rows-per-page label.
    /// </summary>
    public string RowsPerPageString => localizer["RowsPerPage"];

    /// <summary>
    /// Gets the localized format string used to display the rows-per-page summary.
    /// </summary>
    public string RowsPerPageInfoFormat => localizer["RowsPerPageInfoFormat", "{first_item}", "{last_item}", "{all_items}"];
}