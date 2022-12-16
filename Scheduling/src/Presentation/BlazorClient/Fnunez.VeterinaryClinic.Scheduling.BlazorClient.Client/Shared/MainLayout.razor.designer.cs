using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Shared;

public partial class MainLayoutComponent : LayoutComponentBase
{

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected NotificationService NotificationService { get; set; }

    protected RadzenBody RadzenBody;

    protected RadzenSidebar RadzenSidebar;

    protected async Task SidebarToggleClick(dynamic args)
    {
        await InvokeAsync(() => { RadzenSidebar.Toggle(); });
        await InvokeAsync(() => { RadzenBody.Toggle(); });
    }
}