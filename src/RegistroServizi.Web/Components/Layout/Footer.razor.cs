namespace RegistroServizi.Web.Components.Layout;

public partial class Footer : IDisposable
{
    private const int LicenseStartYear = 2026;
    //private string YearLicense
    //{
    //    get
    //    {
    //        var year = DateTime.Now.Year;
    //        return year == LicenseStartYear ? LicenseStartYear.ToString() : $"{LicenseStartYear} - {year}";
    //    }
    //}
    private string YearLicense => DateTime.Now.Year == LicenseStartYear ? LicenseStartYear.ToString() : $"{LicenseStartYear} - {DateTime.Now.Year}";

    private string? nomeApplicazione;
    private IDisposable? optionsChangeSubscription;

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

    public void Dispose()
    {
        optionsChangeSubscription?.Dispose();
        optionsChangeSubscription = null;
    }
}