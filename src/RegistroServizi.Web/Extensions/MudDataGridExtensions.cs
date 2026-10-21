namespace RegistroServizi.Web.Extensions;

public static class MudDataGridExtensions
{
    /// <summary>
    /// Loads data asynchronously and applies the result to a Blazor data-grid component.
    /// </summary>
    /// <typeparam name="T">The type of item displayed by the data grid.</typeparam>
    /// <param name="component">
    /// The Blazor component whose renderer synchronizes state updates and notifications.
    /// </param>
    /// <param name="options">
    /// The operations and services required to retrieve items, update the data-grid state,
    /// handle cancellation, and report loading failures.
    /// </param>
    /// <returns>A task that represents the asynchronous loading operation.</returns>
    /// <remarks>
    /// <para>
    /// The loading state is enabled before data retrieval and disabled when the operation
    /// completes, is canceled, or fails. Loaded items are materialized before being assigned
    /// to the component, and the view is configured after the items are updated.
    /// </para>
    /// <para>
    /// Cancellation requests prevent the loaded result from being applied and suppress the
    /// expected <see cref="OperationCanceledException"/>. Unexpected exceptions are logged
    /// and reported to the user through the configured snackbar.
    /// </para>
    /// <para>
    /// Component state changes are dispatched through <see cref="MudDataGridLoadingOptions{T}.InvokeAsync"/>
    /// to ensure that updates run on the Blazor renderer synchronization context.
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="component"/>, <paramref name="options"/>, or a required
    /// operation or service in <paramref name="options"/> is <see langword="null"/>.
    /// </exception>
    public static async Task LoadDataAsync<T>(this ComponentBase component, MudDataGridLoadingOptions<T> options)
    {
        ArgumentNullException.ThrowIfNull(component);
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(options.LoadItemsAsync);
        ArgumentNullException.ThrowIfNull(options.SetItems);
        ArgumentNullException.ThrowIfNull(options.ConfigureView);
        ArgumentNullException.ThrowIfNull(options.SetLoading);
        ArgumentNullException.ThrowIfNull(options.Logger);
        ArgumentNullException.ThrowIfNull(options.Localizer);
        ArgumentNullException.ThrowIfNull(options.Snackbar);
        ArgumentNullException.ThrowIfNull(options.InvokeAsync);

        await options.InvokeAsync(() =>
        {
            options.SetLoading(true);
            return Task.CompletedTask;
        });

        try
        {
            var loadedItems = await options.LoadItemsAsync();

            if (options.CancellationToken.IsCancellationRequested)
            {
                return;
            }

            await options.InvokeAsync(() =>
            {
                options.SetItems(loadedItems?.ToList() ?? []);
                options.ConfigureView();

                return Task.CompletedTask;
            });
        }
        catch (OperationCanceledException) when (options.CancellationToken.IsCancellationRequested)
        {
        }
        catch (Exception ex)
        {
            options.Logger.LogError(ex, "{LoadingError}. {Message}: {ExceptionMessage}", options.Localizer["LoadingError"], options.Localizer["Message"], ex.Message);

            await options.InvokeAsync(() =>
            {
                MudSnackbarExtensions.ShowNotifyError(options.Snackbar, options.Localizer["LoadingError"]);
                return Task.CompletedTask;
            });
        }
        finally
        {
            await options.InvokeAsync(() =>
            {
                options.SetLoading(false);
                return Task.CompletedTask;
            });
        }
    }
}