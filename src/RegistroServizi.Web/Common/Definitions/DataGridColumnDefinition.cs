using System.Linq.Expressions;

namespace RegistroServizi.Web.Common.Definitions;

/// <summary>
/// Describes a column in a data grid for the specified item type.
/// </summary>
/// <typeparam name="T">The type of item displayed in the grid.</typeparam>
public class DataGridColumnDefinition<T>
{
    /// <summary>
    /// Gets or sets the property expression used to read the column value from <typeparamref name="T" />.
    /// </summary>
    /// <remarks>
    /// This expression typically points to a single property on the row model, for example <c>x =&gt; x.Name</c>.
    /// </remarks>
    public Expression<Func<T, object>> Property { get; set; } = default!;

    /// <summary>
    /// Gets or sets the text displayed in the column header.
    /// </summary>
    /// <remarks>
    /// This value is shown to the user as the column caption in the grid header.
    /// </remarks>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the column is hidden from the data grid.
    /// </summary>
    /// <remarks>
    /// When set to <see langword="true" />, the column is excluded from the visible grid output.
    /// </remarks>
    public bool IsHidden { get; set; } = false;

    /// <summary>
    /// Gets or sets the format string used to render the column value.
    /// </summary>
    /// <remarks>
    /// This value can be used for standard or custom .NET formatting, such as
    /// <c>C2</c>, <c>d</c>, or a custom date/time pattern. Leave it empty to use the
    /// default rendering behavior.
    /// </remarks>
    public string Format { get; set; } = string.Empty;
}