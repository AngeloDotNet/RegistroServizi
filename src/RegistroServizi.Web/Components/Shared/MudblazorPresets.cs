using MudBlazor;

namespace RegistroServizi.Web.Components.Shared;

/// <summary>
/// Provides the standard Material Filled icons used by shared Blazor components.
/// </summary>
/// <remarks>
/// Centralizing these icons keeps action buttons visually consistent throughout the
/// application and allows the selected icons to be changed without updating each
/// consuming component. See the <see href="https://mudblazor.com/components/icons">MudBlazor icons documentation</see>
/// for details about the underlying icon set.
/// </remarks>
public static class MudblazorPresets
{
    /// <summary>
    /// Gets the Material Filled check icon used to represent create or confirm actions.
    /// </summary>
    /// <value>
    /// The icon identifier for <see cref="Icons.Material.Filled.Check"/>.
    /// </value>
    public static string IconCreate => Icons.Material.Filled.Check;

    /// <summary>
    /// Gets the Material Filled edit icon used to represent actions that modify existing data.
    /// </summary>
    /// <value>
    /// The icon identifier for <see cref="Icons.Material.Filled.Edit"/>.
    /// </value>
    public static string IconEdit => Icons.Material.Filled.Edit;

    /// <summary>
    /// Gets the Material Filled delete icon used to represent standard delete actions.
    /// </summary>
    /// <value>
    /// The icon identifier for <see cref="Icons.Material.Filled.Delete"/>.
    /// </value>
    public static string IconDelete => Icons.Material.Filled.Delete;

    /// <summary>
    /// Gets the Material Filled delete-forever icon used to represent irreversible delete actions.
    /// </summary>
    /// <value>
    /// The icon identifier for <see cref="Icons.Material.Filled.DeleteForever"/>.
    /// </value>
    public static string IconDeleteSecure => Icons.Material.Filled.DeleteForever;

    /// <summary>
    /// Gets the Material Filled save icon used to represent actions that persist changes.
    /// </summary>
    /// <value>
    /// The icon identifier for <see cref="Icons.Material.Filled.Save"/>.
    /// </value>
    public static string IconSave => Icons.Material.Filled.Save;

    /// <summary>
    /// Gets the Material Filled cancel icon used to represent actions that discard or stop an operation.
    /// </summary>
    /// <value>
    /// The icon identifier for <see cref="Icons.Material.Filled.Cancel"/>.
    /// </value>
    public static string IconCancel => Icons.Material.Filled.Cancel;
}