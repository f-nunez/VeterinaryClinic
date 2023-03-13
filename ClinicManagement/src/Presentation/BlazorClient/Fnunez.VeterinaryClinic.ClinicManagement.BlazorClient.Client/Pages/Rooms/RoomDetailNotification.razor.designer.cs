using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomById;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Helpers;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;
using Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.ViewModels.Rooms;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Pages.Rooms;

public partial class RoomDetailNotificationComponent : ComponentBase
{
    [Inject]
    private IRoomService _roomService { get; set; }

    [Inject]
    private ILogger<RoomDetailNotificationComponent> _logger { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    protected bool IsNotActive { get; set; }

    protected bool IsNotFound { get; set; }

    protected RoomVm Model { get; set; } = new();

    [Inject]
    protected IStringLocalizer<RoomDetailNotificationComponent> StringLocalizer { get; set; }

    [Parameter]
    public int RoomId { get; set; }

    protected async override Task OnInitializedAsync()
    {
        var request = new GetRoomByIdRequest
        {
            Id = RoomId
        };

        try
        {
            var room = await _roomService
                .GetByIdAsync(request);

            Model = RoomHelper
                .MapRoomViewModel(room);

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
        _navigationManager.NavigateTo("rooms");
    }
}