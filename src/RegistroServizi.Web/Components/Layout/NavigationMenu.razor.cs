namespace RegistroServizi.Web.Components.Layout;

public partial class NavigationMenu
{
    // private string appName = string.Empty;
    // private string version = string.Empty;

    // private bool _loaded;

    // protected override async Task OnInitializedAsync()
    // {
    //     if (_loaded)
    //     {
    //         return;
    //     }

    //     _loaded = true;

    //     await using var dbContext = await DbContextFactory.CreateDbContextAsync();

    //     var applicazione = await dbContext.Applicazioni
    //         .OrderBy(x => x.Id)
    //         .Select(x => new { x.NomeApplicazione, x.Versione })
    //         .FirstOrDefaultAsync();

    //     if (applicazione is not null)
    //     {
    //         appName = applicazione.NomeApplicazione;
    //         version = applicazione.Versione;
    //     }
    // }

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

    //private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    //{
    //    currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
    //    StateHasChanged();
    //}
}