using Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
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

    [Inject]
    protected ISecurityService SecurityService { get; set; }

    [Inject]
    protected IStringLocalizer<MainLayout> StringLocalizer { get; set; }

    protected RadzenBody RadzenBody;

    protected RadzenSidebar RadzenSidebar;

    protected bool IsBodyExpanded = false;

    protected bool IsSidebarExpanded = true;

    protected bool IsAuthenticated = false;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await SecurityService.SetApplicationUserAsync();

        IsAuthenticated = await SecurityService.IsAuthenticatedAsync();
    }

    protected async Task SidebarToggleClick(dynamic args)
    {
        await InvokeAsync(() => { RadzenSidebar.Toggle(); });
        await InvokeAsync(() => { RadzenBody.Toggle(); });
        IsSidebarExpanded = !IsSidebarExpanded;
        IsBodyExpanded = !IsBodyExpanded;
    }

    protected async Task ProfileMenuClick(dynamic args)
    {
        if (args.Value == "Logout")
            await SecurityService.LogoutAsync();
    }
}