using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Common;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRoomById;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;

namespace Fnunez.VeterinaryClinic.ClinicManagement.BlazorClient.Client.Services;

public interface IRoomService
{
    public Task<RoomDto> CreateAsync(CreateRoomRequest request);
    public Task<DataGridResponse<RoomDto>> DataGridAsync(GetRoomsRequest request);
    public Task<List<string>> DataGridFilterIdAsync(string filterValue);
    public Task<List<string>> DataGridFilterNameAsync(string filterValue);
    public Task DeleteAsync(DeleteRoomRequest request);
    public Task<RoomDto> GetByIdAsync(GetRoomByIdRequest request);
    public Task<RoomDto> UpdateAsync(UpdateRoomRequest request);
}