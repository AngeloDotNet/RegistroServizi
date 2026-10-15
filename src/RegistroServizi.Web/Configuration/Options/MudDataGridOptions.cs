using MudBlazor;

namespace RegistroServizi.Web.Configuration.Options;

/// <summary>
/// Represents the shared configuration used to initialize and customize MudBlazor data grids
/// throughout the Blazor application.
/// </summary>
/// <remarks>
/// These options are bound from the <c>DataGridOptions</c> configuration section and registered
/// through dependency injection. Shared configuration ensures that data grids use consistent
/// paging, sorting, and visual presentation defaults across pages.
/// </remarks>
public class MudDataGridOptions
{
    /// <summary>
    /// The configuration section name used to bind these options from application settings.
    /// </summary>
    /// <remarks>
    /// The section is consumed when registering
    /// <see cref="MudDataGridOptions"/> with the application options system.
    /// </remarks>
    public const string SectionName = "DataGridOptions";

    /// <summary>
    /// Gets or sets the default number of rows displayed per page.
    /// </summary>
    /// <value>
    /// The default page size. The default value is <c>5</c>.
    /// </value>
    /// <remarks>
    /// This value is applied to the <c>RowsPerPage</c> property of each shared MudBlazor data grid.
    /// </remarks>
    public int RowsPerPage { get; set; } = 5;

    /// <summary>
    /// Gets or sets the page size values available for selection in the data-grid pager.
    /// </summary>
    /// <value>
    /// An array containing the available page sizes, or <see langword="null"/> when no explicit
    /// page-size options are configured.
    /// </value>
    /// <remarks>
    /// These values are passed to the MudBlazor pager as its page-size options and are displayed
    /// when the page-size selector is enabled.
    /// </remarks>
    public int[]? PageSize { get; set; }

    /// <summary>
    /// Gets or sets the sort mode applied to the data grid.
    /// </summary>
    /// <value>
    /// A <see cref="SortMode"/> value. The default is <see cref="SortMode.None"/>.
    /// </value>
    /// <remarks>
    /// Use this setting to control whether the grid permits single-column or multi-column sorting.
    /// </remarks>
    public SortMode SortMode { get; set; } = SortMode.None;

    /// <summary>
    /// Gets or sets a value indicating whether the data grid should display borders.
    /// </summary>
    /// <value>
    /// <see langword="true"/> to display borders; otherwise, <see langword="false"/>.
    /// The default is <see langword="true"/>.
    /// </value>
    public bool Bordered { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the data grid should use a dense layout.
    /// </summary>
    /// <value>
    /// <see langword="true"/> to use reduced spacing; otherwise, <see langword="false"/>.
    /// The default is <see langword="true"/>.
    /// </value>
    public bool Dense { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether row hover highlighting is enabled.
    /// </summary>
    /// <value>
    /// <see langword="true"/> to highlight a row when the pointer hovers over it; otherwise,
    /// <see langword="false"/>. The default is <see langword="true"/>.
    /// </value>
    public bool Hover { get; set; } = true;
}