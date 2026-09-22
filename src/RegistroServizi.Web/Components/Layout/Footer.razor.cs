namespace RegistroServizi.Web.Components.Layout;

public partial class Footer
{
    private const int LicenseStartYear = 2026;
    private string yearLicense => DateTime.Now.Year == LicenseStartYear ? LicenseStartYear.ToString() : $"{LicenseStartYear} - {DateTime.Now.Year}";

    //private string appName = string.Empty;

    //protected override async Task OnInitializedAsync()
    //{
    //    await using var dbContext = await DbContextFactory.CreateDbContextAsync();
    //
    //    var applicazione = await dbContext.Applicazioni
    //        .OrderBy(x => x.Id)
    //        .Select(x => new { x.NomeApplicazione })
    //        .FirstOrDefaultAsync();
    //
    //    if (applicazione is not null)
    //    {
    //        appName = applicazione.NomeApplicazione;
    //    }
    //}

    private string? _nomeApplicazione;
    private string? _versioneApplicazione;

    private IDisposable? _optionsChangeSubscription;

    protected override void OnInitialized()
    {
        // _nomeAssociazione = AssociazioneOptions.CurrentValue.NomeAssociazione;
        _nomeApplicazione = ApplicazioneOptions.CurrentValue.NomeApplicazione;
        _versioneApplicazione = ApplicazioneOptions.CurrentValue.VersioneApplicazione;

        // _optionsChangeSubscription = AssociazioneOptions.OnChange(options =>
        // {
        //     _nomeAssociazione = options.NomeAssociazione;
        //     _ = InvokeAsync(StateHasChanged);
        // });

        _optionsChangeSubscription = ApplicazioneOptions.OnChange(options =>
        {
            _nomeApplicazione = options.NomeApplicazione;
            _versioneApplicazione = options.VersioneApplicazione;
            _ = InvokeAsync(StateHasChanged);
        });

        // NavigationManager.LocationChanged += OnLocationChanged;
    }

    public void Dispose()
    {
        // NavigationManager.LocationChanged -= OnLocationChanged;
        _optionsChangeSubscription?.Dispose();
    }
}