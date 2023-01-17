using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterId;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomsFilterName;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class RoomService : IRoomService
{
    private readonly IHttpService _httpService;
    private readonly ILogger<RoomService> _logger;

    public RoomService(
        IHttpService httpService,
        ILogger<RoomService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<RoomDto> CreateAsync(CreateRoomRequest createRoomRequest)
    {
        _logger.LogInformation($"Create: {createRoomRequest}");

        var response = await _httpService
            .HttpPostAsync<CreateRoomResponse>("Room/Create", createRoomRequest);

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Room;
    }

    public async Task<DataGridResponse<RoomDto>> DataGridAsync(
        GetRoomsRequest request)
    {
        _logger.LogInformation($"DataGrid: {request.DataGridRequest.ToString()}");

        var response = await _httpService
            .HttpPostAsync<GetRoomsResponse>(
                $"Room/DataGrid",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        if (response.DataGridResponse is null)
            throw new ArgumentNullException(nameof(response.DataGridResponse));

        return response.DataGridResponse;
    }

    public async Task<List<string>> DataGridFilterIdAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterId: {filterValue}");

        var request = new GetRoomsFilterIdRequest
        {
            IdFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetRoomsFilterIdResponse>(
                $"Room/DataGridFilterId",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.RoomIds;
    }

    public async Task<List<string>> DataGridFilterNameAsync(
        string filterValue)
    {
        _logger.LogInformation($"DataGridFilterName: {filterValue}");

        var request = new GetRoomsFilterNameRequest
        {
            NameFilterValue = filterValue
        };

        var response = await _httpService
            .HttpPostAsync<GetRoomsFilterNameResponse>(
                $"Room/DataGridFilterName",
                request
            );

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.RoomNames;
    }

    public async Task DeleteAsync(DeleteRoomRequest request)
    {
        _logger.LogInformation($"Delete: {request.Id}");

        await _httpService
            .HttpDeleteAsync<DeleteRoomResponse>("Room/Delete", request.Id);
    }

    public async Task<RoomDto> GetByIdAsync(GetRoomByIdRequest request)
    {
        _logger.LogInformation($"GetById: {request.Id}");

        var response = await _httpService
            .HttpGetAsync<GetRoomByIdResponse>($"Room/GetById/{request.Id}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Room;
    }

    public async Task<RoomDto> UpdateAsync(UpdateRoomRequest updateRoomRequest)
    {
        _logger.LogInformation($"Edit: {updateRoomRequest}");

        var response = await _httpService
            .HttpPutAsync<UpdateRoomResponse>("Room/Update", updateRoomRequest);

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Room;
    }
}