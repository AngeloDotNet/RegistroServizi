namespace RegistroServizi.Web.Components.Layout;

public partial class NavigationMenu
{
    private string? _nomeApplicazione;
    private string? _versioneApplicazione;

    private IDisposable? _optionsChangeSubscription;

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

    public void Dispose()
    {
        _optionsChangeSubscription?.Dispose();
    }
}