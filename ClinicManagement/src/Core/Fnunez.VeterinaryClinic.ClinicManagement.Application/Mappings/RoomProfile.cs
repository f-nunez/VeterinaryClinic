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
        CreateMap<Room, RoomDto>()
            .ForMember(
                dto => dto.RoomId,
                options => options.MapFrom(src => src.Id)
            );

        CreateMap<RoomDto, Room>()
            .ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.RoomId)
            );

        CreateMap<CreateRoomRequest, Room>();

        CreateMap<DeleteRoomRequest, Room>();

        CreateMap<UpdateRoomRequest, Room>()
            .ForMember(
                dto => dto.Id,
                options => options.MapFrom(src => src.RoomId)
            );
    }
}