namespace RegistroServizi.Domain.Enums;

/// <summary>
/// Specifies the action performed on an item.
/// </summary>
public enum ItemActionType
{
    /// <summary>
    /// Creates a new item.
    /// </summary>
    Create,

    /// <summary>
    /// Updates an existing item.
    /// </summary>
    Edit,

    /// <summary>
    /// Deletes an existing item.
    /// </summary>
    Delete
}