namespace RegistroServizi.Web.Components.Pages.Opzioni;

/// <summary>
/// Provides navigation actions for the <c>Opzioni</c> page.
/// </summary>
public partial class Opzioni
{
    /// <summary>
    /// Navigates to the <c>/ospedali</c> route.
    /// </summary>
    private void ViewOspedali() => navigationManager.NavigateTo("/ospedali");

    /// <summary>
    /// Navigates to the <c>/colonnine</c> route.
    /// </summary>
    private void ViewColonnine() => navigationManager.NavigateTo("/colonnine");

    /// <summary>
    /// Navigates to the <c>/ortelio</c> route.
    /// </summary>
    private void ViewOrtelio() => navigationManager.NavigateTo("/ortelio");

    /// <summary>
    /// Navigates to the <c>/autoparco</c> route.
    /// </summary>
    private void ViewAutoparco() => navigationManager.NavigateTo("/autoparco");

    /// <summary>
    /// Navigates to the <c>/mappe</c> route.
    /// </summary>
    private void ViewMappe() => navigationManager.NavigateTo("/mappe");

    /// <summary>
    /// Navigates to the <c>/squadre-feriali</c> route.
    /// </summary>
    private void ViewSquadreFeriali() => navigationManager.NavigateTo("/squadre-feriali");

    /// <summary>
    /// Navigates to the <c>/squadre-festive</c> route.
    /// </summary>
    private void ViewSquadreFestive() => navigationManager.NavigateTo("/squadre-festive");
}