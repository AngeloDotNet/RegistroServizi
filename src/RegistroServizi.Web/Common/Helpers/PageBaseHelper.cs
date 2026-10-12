using MudBlazor;

namespace RegistroServizi.Web.Common.Helpers;

/// <summary>
/// Provides a reusable base component for Blazor pages and components.
/// </summary>
/// <remarks>
/// Exposes commonly used services through dependency injection, including authentication,
/// configuration, notifications, dialogs, localization, and MudBlazor data-grid support.
/// </remarks>
public class PageBaseHelper : ComponentBase
{

    /// <summary>
    /// Gets the provider used to retrieve the current user's authentication state.
    /// </summary>
    [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    /// <summary>
    /// Gets the application configuration used to read settings and options.
    /// </summary>
    [Inject] protected IConfiguration Configuration { get; set; } = default!;

    /// <summary>
    /// Gets the snackbar service used to display transient notifications to the user.
    /// </summary>
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;

    /// <summary>
    /// Gets the dialog service used to open, configure, and manage dialogs.
    /// </summary>
    [Inject] protected IDialogService DialogService { get; set; } = default!;

    /// <summary>
    /// Gets the localizer used to retrieve localized UI text.
    /// </summary>
    [Inject] protected IStringLocalizer<SharedResource> Localizer { get; set; } = default!;

    /// <summary>
    /// Gets the helper used to configure and work with data grids.
    /// </summary>
    [Inject] protected DataGridHelper DataGridHelper { get; set; } = default!;

    /// <summary>
    /// Gets the configured options for MudBlazor data grids.
    /// </summary>
    [Inject] protected IOptions<MudBlazorDataGridOptions> DataGridOptions { get; set; } = default!;
}