using MudBlazor;

namespace RegistroServizi.Web.Common.Helpers;

/// <summary>
/// Provides a reusable base component for Blazor pages and components that require
/// the application's commonly used UI and infrastructure services.
/// </summary>
/// <remarks>
/// Derive page components from this class to access shared services through protected
/// properties instead of declaring the same dependency-injection members repeatedly.
/// The services are assigned by Blazor during component initialization and must not be
/// accessed before dependency injection has completed.
/// </remarks>
public class PageBaseHelper : ComponentBase
{
    /// <summary>
    /// Gets the application configuration used to access registered settings and options.
    /// </summary>
    /// <remarks>
    /// Prefer strongly typed options for application settings that are used throughout
    /// the application.
    /// </remarks>
    [Inject] protected IConfiguration Configuration { get; set; } = default!;

    /// <summary>
    /// Gets the MudBlazor snackbar service used to display transient notifications.
    /// </summary>
    /// <remarks>
    /// Use the snackbar to provide immediate, non-blocking feedback after user actions,
    /// such as successful saves or recoverable errors.
    /// </remarks>
    [Inject] protected ISnackbar Snackbar { get; set; } = default!;

    /// <summary>
    /// Gets the MudBlazor dialog service used to display and manage modal dialogs.
    /// </summary>
    /// <remarks>
    /// Use the service to open dialogs that require user input or confirmation without
    /// navigating away from the current page.
    /// </remarks>
    [Inject] protected IDialogService DialogService { get; set; } = default!;

    /// <summary>
    /// Gets the localizer used to retrieve localized text from the shared resources.
    /// </summary>
    /// <remarks>
    /// Shared localization keeps common UI messages consistent across Blazor pages and
    /// components and allows them to be translated without changing component code.
    /// </remarks>
    [Inject] protected IStringLocalizer<SharedResource> Localizer { get; set; } = default!;

    /// <summary>
    /// Gets the helper used to configure and operate on MudBlazor data grids.
    /// </summary>
    /// <remarks>
    /// Centralizing data-grid behavior in the helper promotes consistent configuration
    /// and interaction patterns across pages.
    /// </remarks>
    [Inject] protected DataGridHelper DataGridHelper { get; set; } = default!;

    /// <summary>
    /// Gets or sets the injected service that provides user-related extension methods and helpers.
    /// </summary>
    /// <remarks>Injected by the dependency injection container. Initialized to a non-null default value to
    /// satisfy the compiler and intended for internal use only.</remarks>
    [Inject] protected UserExtensions UserExtensions { get; set; } = default!;

    /// <summary>
    /// Indicates whether the instance is currently performing a loading operation.
    /// </summary>
    /// <remarks>Defaults to <see langword="true" />. Used to control or query loading state for conditional
    /// logic and UI updates within the declaring class and derived types.</remarks>
    protected bool isLoading = true;

    /// <summary>
    /// Indicates whether the Add button is disabled.
    /// </summary>
    /// <remarks>When true, the UI should prevent adding new items and render the Add button in a disabled
    /// state. Default is false. Not thread-safe; update on the UI thread.</remarks>
    protected bool disableAddButton;

    /// <summary>
    /// Specifies whether the edit button is disabled.
    /// </summary>
    /// <remarks>Managed by the containing class to control edit availability. Update on the UI thread where
    /// applicable to avoid race conditions when accessed from multiple threads.</remarks>
    protected bool disableEditButton;

    /// <summary>
    /// Indicates whether the delete button is disabled.
    /// </summary>
    /// <remarks>Toggled by the owning component to reflect whether the delete action is available in the UI.</remarks>
    protected bool disableDeleteButton;
}