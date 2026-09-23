namespace RegistroServizi.Web.Components.Layout;

public partial class Footer : IDisposable
{
    private const int LicenseStartYear = 2026;
    private string yearLicense
    {
        get
        {
            var year = DateTime.Now.Year;
            return year == LicenseStartYear ? LicenseStartYear.ToString() : $"{LicenseStartYear} - {year}";
        }
    }

    private string? nomeApplicazione;
    //private string? versioneApplicazione;

    private IDisposable? optionsChangeSubscription;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        nomeApplicazione = ApplicazioneOptions?.CurrentValue?.NomeApplicazione;
        //versioneApplicazione = ApplicazioneOptions.CurrentValue.VersioneApplicazione;

        optionsChangeSubscription = ApplicazioneOptions?.OnChange(options =>
        {
            nomeApplicazione = options.NomeApplicazione;
            //versioneApplicazione = options.VersioneApplicazione;
            _ = InvokeAsync(StateHasChanged);
        });
    }

    public void Dispose()
    {
        optionsChangeSubscription?.Dispose();
        optionsChangeSubscription = null;
    }
}