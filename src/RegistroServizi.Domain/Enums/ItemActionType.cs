namespace RegistroServizi.Domain.Enums;

/// <summary>
/// Specifies the operation to perform on an item.
/// </summary>
/// <remarks>
/// Use this enumeration to identify the intended lifecycle operation when processing
/// an item, such as creating, editing, or deleting it.
/// </remarks>
public enum ItemActionType
{
    /// <summary>
    /// Creates and persists a new item.
    /// </summary>
    Create,

    /// <summary>
    /// Updates and persists changes to an existing item.
    /// </summary>
    Edit,

    /// <summary>
    /// Removes an existing item.
    /// </summary>
    Delete
}