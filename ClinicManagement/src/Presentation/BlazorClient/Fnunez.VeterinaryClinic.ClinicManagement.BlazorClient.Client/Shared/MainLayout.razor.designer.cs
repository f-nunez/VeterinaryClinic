using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
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
    protected ISecurityService SecurityService { get; set; }

    [Inject]
    protected IStringLocalizer<MainLayout> StringLocalizer { get; set; }

    protected RadzenBody RadzenBody;

    protected RadzenSidebar RadzenSidebar;

    protected bool IsBodyExpanded = false;

    protected bool IsSidebarExpanded = true;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await SecurityService.SetUserInfoAsync();
    }

    protected async Task SidebarToggleClick(dynamic args)
    {
        await InvokeAsync(() => { RadzenSidebar.Toggle(); });
        await InvokeAsync(() => { RadzenBody.Toggle(); });
        IsSidebarExpanded = !IsSidebarExpanded;
        IsBodyExpanded = !IsBodyExpanded;
    }

    protected async System.Threading.Tasks.Task ProfileMenuClick(dynamic args)
    {
        if (args.Value == "Logout")
            await SecurityService.Logout();
    }
}