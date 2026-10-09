namespace RegistroServizi.Web.Components.Pages.Impostazioni;

public partial class Impostazioni
{
    /// <summary>
    /// Navigates to the Prezzi Servizi page.
    /// </summary>
    /// <remarks>
    /// Performs a client-side navigation to the <c>/prezzi-servizi</c> route using <c>NavigationManager</c>.
    /// </remarks>
    private void ViewPrezziServizi() => navigationManager.NavigateTo("/prezzi-servizi");
}