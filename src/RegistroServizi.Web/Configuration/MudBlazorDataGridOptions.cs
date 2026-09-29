using MudBlazor;

namespace RegistroServizi.Web.Configuration;

/// <summary>
/// Represents the configuration options used to initialize and customize a MudBlazor data grid.
/// </summary>
public class MudBlazorDataGridOptions
{
    /// <summary>
    /// The configuration section name used to bind these options from application settings.
    /// </summary>
    public const string SectionName = "DataGridOptions";

    /// <summary>
    /// Gets or sets the default number of rows displayed per page.
    /// </summary>
    public int RowsPerPage { get; set; } = 5;

    /// <summary>
    /// Gets or sets the available page size values users can select from.
    /// </summary>
    public int[]? PageSize { get; set; }

    /// <summary>
    /// Gets or sets the sort mode applied to the data grid.
    /// </summary>
    public SortMode SortMode { get; set; } = SortMode.None;

    /// <summary>
    /// Gets or sets a value indicating whether the data grid should display borders.
    /// </summary>
    public bool Bordered { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the data grid should use a dense layout.
    /// </summary>
    public bool Dense { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether row hover highlighting is enabled.
    /// </summary>
    public bool Hover { get; set; } = true;
}