namespace RegistroServizi.Web.Components.Shared.Cards;

public partial class ServiceCard
{
    [Parameter] public string Title { get; set; } = string.Empty;
    [Parameter] public string Subtitle { get; set; } = string.Empty;
    [Parameter] public string IconName { get; set; } = string.Empty;

    [Parameter] public EventCallback OnClick { get; set; }

    private async Task HandleClick() => await OnClick.InvokeAsync();
}