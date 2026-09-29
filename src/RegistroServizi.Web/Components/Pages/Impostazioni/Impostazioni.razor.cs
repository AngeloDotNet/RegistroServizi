namespace RegistroServizi.Web.Components.Pages.Impostazioni;

public partial class Impostazioni
{
    /// <summary>
    /// Navigates to the '/prezzi-servizi' route.
    /// </summary>
    /// <remarks>Uses NavigationManager to perform a client-side navigation to the Prezzi Servizi page.</remarks>
    private void ViewPrezziServizi() => navigationManager.NavigateTo("/prezzi-servizi");
}