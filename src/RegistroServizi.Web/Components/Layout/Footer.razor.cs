namespace RegistroServizi.Web.Components.Layout;

/// <summary>
/// Represents the application footer component.
/// </summary>
/// <remarks>
/// Displays the copyright year range, the current application name,
/// and localized footer text. The component also listens for
/// <see cref="ApplicazioneOptions"/> changes so the rendered content
/// updates when the application name changes.
/// </remarks>
public partial class Footer : IDisposable
{
    /// <summary>
    /// The first year used in the copyright range.
    /// </summary>
    private const int LicenseStartYear = 2026;

    /// <summary>
    /// Gets the license year text for the footer.
    /// </summary>
    /// <remarks>
    /// Returns a single year when the current year matches
    /// <see cref="LicenseStartYear"/>; otherwise returns a year range.
    /// </remarks>
    private static string YearLicense => DateTime.Now.Year == LicenseStartYear ? LicenseStartYear.ToString() : $"{LicenseStartYear} - {DateTime.Now.Year}";

    /// <summary>
    /// Stores the current application name displayed in the footer.
    /// </summary>
    private string? nomeApplicazione;

    /// <summary>
    /// Tracks the options change subscription so it can be released
    /// when the component is disposed.
    /// </summary>
    private IDisposable? optionsChangeSubscription;

    /// <summary>
    /// Initializes the component state and subscribes to application
    /// option changes.
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        nomeApplicazione = ApplicazioneOptions?.CurrentValue?.NomeApplicazione;
        optionsChangeSubscription = ApplicazioneOptions?.OnChange(options =>
        {
            nomeApplicazione = options.NomeApplicazione;
            _ = InvokeAsync(StateHasChanged);
        });
    }

    /// <summary>
    /// Releases the options change subscription.
    /// </summary>
    public void Dispose()
    {
        optionsChangeSubscription?.Dispose();
        optionsChangeSubscription = null;
    }
}