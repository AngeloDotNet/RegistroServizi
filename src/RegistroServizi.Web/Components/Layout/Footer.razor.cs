namespace RegistroServizi.Web.Components.Layout;

/// <summary>
/// Represents the application footer component.
/// </summary>
/// <remarks>
/// Renders the copyright year, the configured application name, and localized
/// footer content. The component observes <see cref="ApplicazioneOptions"/>
/// changes so the displayed application name remains synchronized with the
/// current configuration while the component is rendered.
/// </remarks>
public partial class Footer : IDisposable
{
    /// <summary>
    /// Defines the first year displayed in the copyright notice.
    /// </summary>
    private const int LicenseStartYear = 2026;

    /// <summary>
    /// Gets the copyright year text rendered by the footer.
    /// </summary>
    /// <remarks>
    /// A single year is returned when the current year is the license start
    /// year. For subsequent years, the result contains the start and current
    /// years as a range.
    /// </remarks>
    private static string YearLicense => DateTime.Now.Year == LicenseStartYear ? LicenseStartYear.ToString() : $"{LicenseStartYear} - {DateTime.Now.Year}";

    /// <summary>
    /// Stores the configured application name displayed in the footer.
    /// </summary>
    private string? nomeApplicazione;

    /// <summary>
    /// Stores the subscription used to observe application option changes.
    /// </summary>
    /// <remarks>
    /// The subscription is disposed with the component to prevent callbacks
    /// from reaching a component that is no longer rendered.
    /// </remarks>
    private IDisposable? optionsChangeSubscription;

    /// <inheritdoc />
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
    /// Releases the application options change subscription held by the component.
    /// </summary>
    /// <remarks>
    /// Disposing the subscription prevents future configuration callbacks after
    /// the footer has been removed from the render tree.
    /// </remarks>
    public void Dispose()
    {
        optionsChangeSubscription?.Dispose();
        optionsChangeSubscription = null;
    }
}