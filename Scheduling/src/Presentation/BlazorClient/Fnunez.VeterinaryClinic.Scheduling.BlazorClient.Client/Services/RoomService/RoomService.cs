using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomById;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterId;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRoomsFilterName;

namespace Fnunez.VeterinaryClinic.Scheduling.BlazorClient.Client.Services;

public class RoomService : IRoomService
{
    private readonly ISchedulingApiHttpService _httpService;
    private readonly ILogger<RoomService> _logger;

    public RoomService(
        ISchedulingApiHttpService httpService,
        ILogger<RoomService> logger)
    {
        _httpService = httpService;
        _logger = logger;
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

    public async Task<RoomDto> GetByIdAsync(GetRoomByIdRequest request)
    {
        _logger.LogInformation($"GetById: {request.Id}");

        var response = await _httpService
            .HttpGetAsync<GetRoomByIdResponse>($"Room/GetById/{request.Id}");

        if (response is null)
            throw new ArgumentNullException(nameof(response));

        return response.Room;
    }
}