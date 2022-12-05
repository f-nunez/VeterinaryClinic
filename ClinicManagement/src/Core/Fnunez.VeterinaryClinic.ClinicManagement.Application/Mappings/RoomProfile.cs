using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.DeleteRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.UpdateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomDto>();

        CreateMap<RoomDto, Room>();

        CreateMap<CreateRoomRequest, Room>();

        CreateMap<DeleteRoomRequest, Room>();

        CreateMap<UpdateRoomRequest, Room>();
    }
}