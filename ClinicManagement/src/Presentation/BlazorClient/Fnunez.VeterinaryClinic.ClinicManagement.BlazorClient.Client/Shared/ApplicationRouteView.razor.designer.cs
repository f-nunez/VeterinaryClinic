using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared;

public partial class ApplicationRouteViewComponent : ComponentBase
{
    [Parameter]
    public RouteData RouteData { get; set; }

    protected void RenderPageWithParameters(RenderTreeBuilder builder)
    {
        builder.OpenComponent(0, RouteData.PageType);

        foreach (var kvp in RouteData.RouteValues)
            builder.AddAttribute(1, kvp.Key, kvp.Value);

        builder.CloseComponent();
    }
}