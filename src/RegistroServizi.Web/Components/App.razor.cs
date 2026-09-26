using static Microsoft.AspNetCore.Components.Web.RenderMode;

namespace RegistroServizi.Web.Components;

public partial class App
{
    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;
    private IComponentRenderMode? PageRenderMode => HttpContext.AcceptsInteractiveRouting() ? InteractiveServer : null;
}