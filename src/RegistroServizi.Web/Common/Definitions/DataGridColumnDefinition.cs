using System.Linq.Expressions;

namespace RegistroServizi.Web.Common.Definitions;

/// <summary>
/// Describes how a data grid renders a column for a specified item type.
/// </summary>
/// <typeparam name="T">The type of item displayed in the grid.</typeparam>
/// <remarks>
/// Use this definition to associate a row property with its column caption and
/// presentation settings. The property expression allows the grid to retrieve
/// values from each row without relying on property-name strings.
/// <para>
/// For example:
/// <code>
/// new DataGridColumnDefinition&lt;Person&gt;
/// {
///     Property = person =&gt; person.Name,
///     Title = "Name"
/// };
/// </code>
/// </para>
/// </remarks>
public class DataGridColumnDefinition<T>
{
    /// <summary>
    /// Gets or sets the expression used to retrieve the column value from a row.
    /// </summary>
    /// <remarks>
    /// The expression should typically reference a single property on
    /// <typeparamref name="T" />, such as <c>person =&gt; person.Name</c>.
    /// </remarks>
    public Expression<Func<T, object>> Property { get; set; } = default!;

    /// <summary>
    /// Gets or sets the text displayed in the column header.
    /// </summary>
    /// <remarks>
    /// This value is used as the column caption presented to the user.
    /// </remarks>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the column is hidden from the data grid.
    /// </summary>
    /// <remarks>
    /// When set to <see langword="true" />, the column is excluded from the visible
    /// grid output while remaining part of the column definition.
    /// </remarks>
    public bool IsHidden { get; set; } = false;

    /// <summary>
    /// Gets or sets the format string used to render the column value.
    /// </summary>
    /// <remarks>
    /// The format can be a standard or custom .NET format string, such as
    /// <c>C2</c>, <c>d</c>, or a custom date and time pattern. An empty value
    /// uses the grid's default rendering behavior.
    /// </remarks>
    public string Format { get; set; } = string.Empty;
}