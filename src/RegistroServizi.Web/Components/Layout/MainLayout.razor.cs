namespace RegistroServizi.Web.Components.Layout;

/// <summary>
/// Main application layout for the Blazor UI.
/// </summary>
/// <remarks>
/// This layout keeps the header, drawer state, and authentication-dependent actions
/// synchronized with the current application and association options.
/// </remarks>
public partial class MainLayout
{
    private string? currentUrl;
    private bool _drawerOpen = true;

    //private string? _nomeApplicazione;
    //private string? _versioneApplicazione;
    private string? _nomeAssociazione;

    private IDisposable? _associazioneOptionsChangeSubscription;
    //private IDisposable? _applicazioneOptionsChangeSubscription;

    /// <summary>
    /// Initializes the layout state and subscribes to option changes and navigation updates.
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _nomeAssociazione = AssociazioneOptions.CurrentValue.NomeAssociazione;
        //_nomeApplicazione = ApplicazioneOptions.CurrentValue.NomeApplicazione;
        //_versioneApplicazione = ApplicazioneOptions.CurrentValue.VersioneApplicazione;

        _associazioneOptionsChangeSubscription = AssociazioneOptions.OnChange(options =>
        {
            _nomeAssociazione = options.NomeAssociazione;
            _ = InvokeAsync(StateHasChanged);
        });

        //_applicazioneOptionsChangeSubscription = ApplicazioneOptions.OnChange(options =>
        //{
        //    _nomeApplicazione = options.NomeApplicazione;
        //    _versioneApplicazione = options.VersioneApplicazione;
        //    _ = InvokeAsync(StateHasChanged);
        //});

        currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        NavigationManager.LocationChanged += OnLocationChanged;
    }

    /// <summary>
    /// Releases navigation and options subscriptions created by the layout.
    /// </summary>
    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
        _associazioneOptionsChangeSubscription?.Dispose();
        //_applicazioneOptionsChangeSubscription?.Dispose();
    }

    /// <summary>
    /// Updates the current return URL whenever navigation changes.
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="e">Navigation event data.</param>
    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
        StateHasChanged();
    }

    /// <summary>
    /// Toggles the navigation drawer open or closed.
    /// </summary>
    private void ToggleDrawer()
    {
        _drawerOpen = !_drawerOpen;
    }

    /// <summary>
    /// Loads the current authentication state used by the authorization-aware header.
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
    }

    /// <summary>
    /// Builds a two-letter set of initials from a display name for the user avatar.
    /// </summary>
    /// <param name="name">The full display name.</param>
    /// <returns>
    /// A single initial, two initials, or <c>?</c> when the input is null, empty, or whitespace.
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