namespace RegistroServizi.Web.Components.Shared.DataGrids;

/// <summary>
/// Provides a reusable, generic data grid for displaying items and executing
/// add, edit, and delete actions.
/// </summary>
/// <typeparam name="T">The type of item displayed in the data grid.</typeparam>
/// <remarks>
/// The component renders columns from <see cref="DataGridColumnDefinition{T}" />
/// instances and exposes callbacks so the parent component can handle CRUD
/// operations without coupling the grid to a specific data source or domain model.
/// </remarks>
public partial class DataGridGeneric<T>
{
    /// <summary>
    /// Gets or sets the items displayed as rows in the data grid.
    /// </summary>
    /// <remarks>
    /// The collection is initialized as empty so the grid can be rendered before
    /// data is available.
    /// </remarks>
    [Parameter] public IEnumerable<T> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the CSS width declarations used to build the grid's column group.
    /// </summary>
    /// <remarks>
    /// Each entry is applied to a <c>col</c> element in the same order as the
    /// corresponding grid column.
    /// </remarks>
    [Parameter] public List<string> LayoutWidths { get; set; } = [];

    /// <summary>
    /// Gets or sets the column definitions used to render the item properties.
    /// </summary>
    /// <remarks>
    /// Each definition specifies the property expression, header title, visibility,
    /// and optional value format for a column.
    /// </remarks>
    [Parameter] public List<DataGridColumnDefinition<T>> Columns { get; set; } = [];

    /// <summary>
    /// Gets or sets the callback invoked when the user requests a new item.
    /// </summary>
    [Parameter] public EventCallback ActionCallback { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the user requests to edit an item.
    /// </summary>
    /// <remarks>
    /// The callback receives the item associated with the selected grid row.
    /// </remarks>
    [Parameter] public EventCallback<T> ActionEditCallback { get; set; }

    /// <summary>
    /// Gets or sets the callback invoked when the user requests to delete an item.
    /// </summary>
    /// <remarks>
    /// The callback receives the item associated with the selected grid row.
    /// </remarks>
    [Parameter] public EventCallback<T> ActionDeleteCallback { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the button for adding a new item is disabled.
    /// </summary>
    /// <remarks>
    /// The add button is rendered only when <see cref="ActionCallback" /> has a delegate.
    /// Set this property to <see langword="true" /> to keep the button visible while
    /// preventing the user from invoking the add action.
    /// </remarks>
    [Parameter] public bool DisableAddButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the edit buttons are disabled.
    /// </summary>
    /// <remarks>
    /// Edit buttons are rendered only when <see cref="ActionEditCallback" /> has a delegate.
    /// Set this property to <see langword="true" /> to keep the buttons visible while
    /// preventing the user from invoking the edit action.
    /// </remarks>
    [Parameter] public bool DisableEditButton { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the delete buttons are disabled.
    /// </summary>
    /// <remarks>
    /// Delete buttons are rendered only when <see cref="ActionDeleteCallback" /> has a delegate.
    /// Set this property to <see langword="true" /> to keep the buttons visible while
    /// preventing the user from invoking the delete action.
    /// </remarks>
    [Parameter] public bool DisableDeleteButton { get; set; }
}