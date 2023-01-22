using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared;

public partial class MainLayoutComponent : LayoutComponentBase
{
    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected NotificationService NotificationService { get; set; }

    [Inject]
    protected IStringLocalizer<MainLayout> StringLocalizer { get; set; }

    protected RadzenBody RadzenBody;

    protected RadzenSidebar RadzenSidebar;

    protected bool IsBodyExpanded = false;

    protected bool IsSidebarExpanded = true;

    protected async Task SidebarToggleClick(dynamic args)
    {
        await InvokeAsync(() => { RadzenSidebar.Toggle(); });
        await InvokeAsync(() => { RadzenBody.Toggle(); });
        IsSidebarExpanded = !IsSidebarExpanded;
        IsBodyExpanded = !IsBodyExpanded;
    }
}