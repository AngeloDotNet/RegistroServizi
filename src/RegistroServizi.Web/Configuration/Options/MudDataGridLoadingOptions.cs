using MudBlazor;

namespace RegistroServizi.Web.Configuration.Options;

/// <summary>
/// Provides the operations and services used to load data into a <see cref="MudDataGrid{T}"/>.
/// </summary>
/// <typeparam name="T">The type of item displayed by the data grid.</typeparam>
/// <remarks>
/// <para>
/// This options object groups the loading workflow's data retrieval, state updates, UI
/// synchronization, cancellation, logging, localization, and error notification dependencies.
/// </para>
/// <para>
/// The delegates that update component state should be executed through <see cref="InvokeAsync"/>
/// so that changes are dispatched to the Blazor synchronization context. Set
/// <see cref="CancellationToken"/> to cancel an in-progress loading operation.
/// </para>
/// </remarks>
public sealed class MudDataGridLoadingOptions<T>
{
    /// <summary>
    /// Gets the asynchronous function that retrieves the items to display.
    /// </summary>
    /// <value>
    /// A task that returns the loaded items, or <see langword="null"/> when no result is available.
    /// </value>
    public required Func<Task<IEnumerable<T>?>> LoadItemsAsync { get; init; }

    /// <summary>
    /// Gets the callback that replaces the component's current items with the loaded items.
    /// </summary>
    /// <value>
    /// An action that receives the materialized list of items.
    /// </value>
    public required Action<List<T>> SetItems { get; init; }

    /// <summary>
    /// Gets the callback that applies view-specific configuration after the items are loaded.
    /// </summary>
    /// <value>
    /// An action that configures data-grid settings such as columns, widths, or pagination.
    /// </value>
    public required Action ConfigureView { get; init; }

    /// <summary>
    /// Gets the callback that updates the component's loading state.
    /// </summary>
    /// <value>
    /// An action that receives <see langword="true"/> when loading starts and
    /// <see langword="false"/> when loading finishes.
    /// </value>
    public required Action<bool> SetLoading { get; init; }

    /// <summary>
    /// Gets the logger used to record exceptions raised during data loading.
    /// </summary>
    /// <value>
    /// The logger used to record diagnostic information and loading failures.
    /// </value>
    public required ILogger Logger { get; init; }

    /// <summary>
    /// Gets the localizer used to resolve localized loading and error messages.
    /// </summary>
    /// <value>
    /// The localizer for <see cref="SharedResource"/>.
    /// </value>
    public required IStringLocalizer<SharedResource> Localizer { get; init; }

    /// <summary>
    /// Gets the snackbar used to notify the user when loading fails.
    /// </summary>
    /// <value>
    /// The MudBlazor snackbar service used to display error notifications.
    /// </value>
    public required ISnackbar Snackbar { get; init; }

    /// <summary>
    /// Gets the function that dispatches component updates to the Blazor synchronization context.
    /// </summary>
    /// <value>
    /// A function that executes the supplied asynchronous callback on the component's renderer context.
    /// </value>
    /// <remarks>
    /// Use the component's <c>InvokeAsync</c> method so that state changes and user notifications
    /// are performed safely during rendering.
    /// </remarks>
    public required Func<Func<Task>, Task> InvokeAsync { get; init; }

    /// <summary>
    /// Gets the token used to observe cancellation of the loading operation.
    /// </summary>
    /// <value>
    /// The cancellation token associated with the current data-loading request.
    /// </value>
    /// <remarks>
    /// When cancellation is requested, the loading workflow stops processing the result and
    /// suppresses the expected <see cref="OperationCanceledException"/>.
    /// </remarks>
    public CancellationToken CancellationToken { get; init; }
}