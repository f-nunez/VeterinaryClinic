using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public class RoomService
{
    private readonly HttpService _httpService;
    private readonly ILogger<RoomService> _logger;

    public RoomService(
        HttpService httpService,
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

    public async Task DeleteAsync(int roomId)
    {
        _logger.LogInformation($"Delete: {roomId}");

        await _httpService
            .HttpDeleteAsync<DeleteRoomResponse>("Room/Delete", roomId);
    }

    public async Task<RoomDto> EditAsync(UpdateRoomRequest updateRoomRequest)
    {
        _logger.LogInformation($"Edit: {updateRoomRequest}");

        var response = await _httpService
            .HttpPutAsync<UpdateRoomResponse>("Room/Update", updateRoomRequest);

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Room;
    }

    public async Task<RoomDto> GetByIdAsync(int roomId)
    {
        _logger.LogInformation($"GetById: {roomId}");

        var response = await _httpService
            .HttpGetAsync<GetRoomByIdResponse>($"Room/GetById/{roomId}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Room;
    }

    public async Task<List<RoomDto>> ListAsync()
    {
        _logger.LogInformation("List");

        var response = await _httpService
            .HttpGetAsync<GetRoomsResponse>($"Room/List");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Rooms;
    }

    public async Task<List<RoomDto>> ListPagedAsync(int pageSize)
    {
        _logger.LogInformation($"ListPaged: {pageSize}");

        var response = await _httpService
            .HttpGetAsync<GetRoomsResponse>($"rooms");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Rooms;
    }
}