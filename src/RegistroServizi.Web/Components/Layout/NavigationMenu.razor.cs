namespace RegistroServizi.Web.Components.Layout;

/// <summary>
/// Provides the application's primary navigation menu.
/// </summary>
/// <remarks>
/// The component displays the configured application name and version and
/// refreshes its rendered content when the corresponding
/// <see cref="ApplicazioneOptions"/> values change.
/// </remarks>
public partial class NavigationMenu
{
    /// <summary>
    /// Stores the application name displayed in the navigation menu.
    /// </summary>
    private string? nomeApplicazione;

    /// <summary>
    /// Stores the application version displayed in the navigation menu.
    /// </summary>
    private string? versioneApplicazione;

    /// <summary>
    /// Stores the subscription that monitors application option changes.
    /// </summary>
    /// <remarks>
    /// The subscription must be disposed when the component is removed to
    /// prevent the options monitor from retaining a reference to the component.
    /// </remarks>
    private IDisposable? optionsChangeSubscription;

    /// <inheritdoc />
    /// <remarks>
    /// The initial values are read from the current
    /// <see cref="ApplicazioneOptions"/> instance. An options-change callback
    /// updates the displayed values and schedules a component re-render so the
    /// menu remains synchronized with the latest configuration.
    /// </remarks>
    protected override void OnInitialized()
    {
        nomeApplicazione = ApplicazioneOptions.CurrentValue.NomeApplicazione;
        versioneApplicazione = ApplicazioneOptions.CurrentValue.VersioneApplicazione;

        optionsChangeSubscription = ApplicazioneOptions.OnChange(options =>
        {
            nomeApplicazione = options.NomeApplicazione;
            versioneApplicazione = options.VersioneApplicazione;
            _ = InvokeAsync(StateHasChanged);
        });
    }

    /// <summary>
    /// Releases the application options subscription owned by the component.
    /// </summary>
    /// <remarks>
    /// Disposing the subscription prevents future option-change notifications
    /// from targeting a component that is no longer rendered.
    /// </remarks>
    public void Dispose()
    {
        optionsChangeSubscription?.Dispose();
    }
}