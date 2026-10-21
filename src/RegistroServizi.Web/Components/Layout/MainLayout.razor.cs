namespace RegistroServizi.Web.Components.Layout;

/// <summary>
/// Provides the shared layout for the application's Blazor pages.
/// </summary>
/// <remarks>
/// The layout coordinates the navigation drawer, association name, authentication-aware
/// actions, and logout return URL. It also responds to changes from
/// <see cref="NavigationManager"/> and <see cref="IOptionsMonitor{TOptions}"/> so the
/// rendered layout remains synchronized with application state.
/// </remarks>
public partial class MainLayout
{
    private string? currentUrl;
    private bool _drawerOpen = true;
    private string? _nomeAssociazione;

    private IDisposable? _associazioneOptionsChangeSubscription;

    /// <inheritdoc />
    /// <remarks>
    /// Initializes the association name, subscribes to association-option changes, and
    /// starts tracking navigation changes so the logout action can preserve the current URL.
    /// </remarks>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _nomeAssociazione = AssociazioneOptions.CurrentValue.NomeAssociazione;
        _associazioneOptionsChangeSubscription = AssociazioneOptions.OnChange(options =>
        {
            _nomeAssociazione = options.NomeAssociazione;
            _ = InvokeAsync(StateHasChanged);
        });

        currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    /// <summary>
    /// Releases the navigation and configuration-change subscriptions created by the layout.
    /// </summary>
    /// <remarks>
    /// This prevents callbacks from targeting the layout after it has been removed from the
    /// Blazor component hierarchy.
    /// </remarks>
    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
        _associazioneOptionsChangeSubscription?.Dispose();
    }

    /// <summary>
    /// Updates the relative URL used as the logout return URL after navigation occurs.
    /// </summary>
    /// <param name="sender">The object that raised the navigation event.</param>
    /// <param name="e">The event data containing the new absolute URL.</param>
    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
        StateHasChanged();
    }

    /// <summary>
    /// Changes the navigation drawer between its open and closed states.
    /// </summary>
    private void ToggleDrawer()
    {
        _drawerOpen = !_drawerOpen;
    }

    /// <inheritdoc />
    /// <remarks>
    /// Requests the current authentication state so authorization-aware content in the
    /// layout is initialized before it is rendered.
    /// </remarks>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
    }

    /// <summary>
    /// Creates the initials displayed in the authenticated user's avatar.
    /// </summary>
    /// <param name="name">
    /// The user's display name. Names are split on whitespace, using the first and last
    /// components when more than one component is available.
    /// </param>
    /// <returns>
    /// The upper-case initial for a single-component name, the upper-case initials for a
    /// multi-component name, or <c>?</c> when <paramref name="name"/> is
    /// <see langword="null"/>, empty, or contains only whitespace.
    /// </returns>
    private static string GetProfileInitials(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "?";
        }

        var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (parts.Length == 1)
        {
            return string.Create(1, parts[0][0], static (destination, value) =>
            {
                destination[0] = char.ToUpperInvariant(value);
            });
        }

        return string.Create(2, parts, static (destination, value) =>
        {
            destination[0] = char.ToUpperInvariant(value[0][0]);
            destination[1] = char.ToUpperInvariant(value[^1][0]);
        });
    }
}