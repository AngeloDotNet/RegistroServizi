namespace RegistroServizi.Web.Components.Layout;

/// <summary>
/// Navigation menu component that displays the application name and version
/// and updates them when the application options change.
/// </summary>
public partial class NavigationMenu
{
    /// <summary>
    /// Gets the current application name shown in the navigation menu.
    /// </summary>
    private string? _nomeApplicazione;

    /// <summary>
    /// Gets the current application version shown in the navigation menu.
    /// </summary>
    private string? _versioneApplicazione;

    /// <summary>
    /// Holds the subscription to application option changes so it can be
    /// disposed when the component is torn down.
    /// </summary>
    private IDisposable? _optionsChangeSubscription;

    /// <summary>
    /// Initializes the component state and subscribes to application option
    /// changes so the menu reflects the latest application name and version.
    /// </summary>
    protected override void OnInitialized()
    {
        _nomeApplicazione = ApplicazioneOptions.CurrentValue.NomeApplicazione;
        _versioneApplicazione = ApplicazioneOptions.CurrentValue.VersioneApplicazione;

        _optionsChangeSubscription = ApplicazioneOptions.OnChange(options =>
        {
            _nomeApplicazione = options.NomeApplicazione;
            _versioneApplicazione = options.VersioneApplicazione;
            _ = InvokeAsync(StateHasChanged);
        });
    }

    /// <summary>
    /// Releases the options change subscription to avoid retaining the component
    /// after disposal.
    /// </summary>
    public void Dispose()
    {
        _optionsChangeSubscription?.Dispose();
    }
}