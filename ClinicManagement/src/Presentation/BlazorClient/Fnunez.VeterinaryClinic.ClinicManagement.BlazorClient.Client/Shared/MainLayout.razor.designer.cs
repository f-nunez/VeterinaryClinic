using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen;
using Radzen.Blazor;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Shared;

public partial class MainLayoutComponent : LayoutComponentBase
{
    protected bool IsBodyExpanded = false;

    protected bool IsSidebarExpanded = true;

    protected RadzenBody RadzenBody;

    protected RadzenSidebar RadzenSidebar;

    [Inject]
    protected ISecurityService SecurityService { get; set; }

    [Inject]
    protected IStringLocalizer<MainLayout> StringLocalizer { get; set; }

    protected async Task SidebarToggleClick(dynamic args)
    {
        await InvokeAsync(() => { RadzenSidebar.Toggle(); });
        await InvokeAsync(() => { RadzenBody.Toggle(); });
        IsSidebarExpanded = !IsSidebarExpanded;
        IsBodyExpanded = !IsBodyExpanded;
    }

    protected void ProfileMenuClick(dynamic args)
    {
        if (args.Value == "Logout")
            SecurityService.Logout();
    }
}