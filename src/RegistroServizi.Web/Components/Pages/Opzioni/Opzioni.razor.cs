namespace RegistroServizi.Web.Components.Pages.Opzioni;

public partial class Opzioni
{
    /// <summary>
    /// Navigates to the '/ospedali' route.
    /// </summary>
    /// <remarks>Uses NavigationManager to perform a client-side navigation to the hospitals page.</remarks>
    private void ViewOspedali() => navigationManager.NavigateTo("/ospedali");

    /// <summary>
    /// Navigates to the '/colonnine' route.
    /// </summary>
    /// <remarks>Performs a programmatic navigation using NavigationManager to change the current
    /// URI.</remarks>
    private void ViewColonnine() => navigationManager.NavigateTo("/colonnine");
}