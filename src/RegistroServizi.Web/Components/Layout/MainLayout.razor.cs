using Microsoft.AspNetCore.Components.Routing;

namespace RegistroServizi.Web.Components.Layout;

public partial class MainLayout
{
    private string? currentUrl;
    private bool _drawerOpen = true;

    private string? _nomeApplicazione;
    private string? _versioneApplicazione;
    private string? _nomeAssociazione;

    private IDisposable? _optionsChangeSubscription;

    protected override void OnInitialized()
    {
        _nomeAssociazione = AssociazioneOptions.CurrentValue.NomeAssociazione;
        _nomeApplicazione = ApplicazioneOptions.CurrentValue.NomeApplicazione;
        _versioneApplicazione = ApplicazioneOptions.CurrentValue.VersioneApplicazione;

        _optionsChangeSubscription = AssociazioneOptions.OnChange(options =>
        {
            _nomeAssociazione = options.NomeAssociazione;
            _ = InvokeAsync(StateHasChanged);
        });

        _optionsChangeSubscription = ApplicazioneOptions.OnChange(options =>
        {
            _nomeApplicazione = options.NomeApplicazione;
            _versioneApplicazione = options.VersioneApplicazione;
            _ = InvokeAsync(StateHasChanged);
        });

        NavigationManager.LocationChanged += OnLocationChanged;
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
        _optionsChangeSubscription?.Dispose();
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
        StateHasChanged();
    }

    private void ToggleDrawer()
    {
        _drawerOpen = !_drawerOpen;
    }

    protected override async Task OnInitializedAsync()
    {
        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
    }

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