using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Client.GetClientDetail;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Clients;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Clients;

public partial class ClientDetailNotificationComponent : ComponentBase
{
    [Inject]
    private IClientService _clientService { get; set; }

    [Inject]
    private ILogger<ClientDetailNotificationComponent> _logger { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    protected bool IsNotActive { get; set; }

    protected bool IsNotFound { get; set; }

    protected ClientDetailVm Model { get; set; } = new();

    [Inject]
    protected IStringLocalizer<ClientDetailNotificationComponent> StringLocalizer { get; set; }

    [Parameter]
    public int ClientId { get; set; }

    protected async override Task OnInitializedAsync()
    {
        var request = new GetClientDetailRequest
        {
            ClientId = ClientId
        };

        try
        {
            var response = await _clientService
                .GetClientDetailAsync(request);

            Model = ClientHelper
                .MapClientDetailViewModel(response.ClientDetail);

            IsNotActive = !Model.IsActive;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            IsNotFound = true;
        }
    }

    protected void OnClickBack()
    {
        _navigationManager.NavigateTo("clients");
    }
}