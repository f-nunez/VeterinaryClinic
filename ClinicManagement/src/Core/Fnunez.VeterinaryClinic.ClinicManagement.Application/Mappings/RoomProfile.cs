using AutoMapper;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.ClinicManagement.Application.SharedModel.Room.CreateRoom;
using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomDto>()
            .ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            );

        CreateMap<RoomDto, Room>()
            .ForMember(
                d => d.CreatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.CreatedOn,
                m => m.Ignore()
            ).ForMember(
                d => d.DomainEvents,
                m => m.Ignore()
            ).ForMember(
                d => d.Id,
                m => m.MapFrom(s => s.Id)
            ).ForMember(
                d => d.IsActive,
                m => m.MapFrom(s => s.IsActive)
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            ).ForMember(
                d => d.UpdatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.UpdatedOn,
                m => m.Ignore()
            );

        CreateMap<CreateRoomRequest, Room>()
            .ForMember(
                d => d.CreatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.CreatedOn,
                m => m.Ignore()
            ).ForMember(
                d => d.DomainEvents,
                m => m.Ignore()
            ).ForMember(
                d => d.Id,
                m => m.Ignore()
            ).ForMember(
                d => d.IsActive,
                m => m.Ignore()
            ).ForMember(
                d => d.Name,
                m => m.MapFrom(s => s.Name)
            ).ForMember(
                d => d.UpdatedBy,
                m => m.Ignore()
            ).ForMember(
                d => d.UpdatedOn,
                m => m.Ignore()
            );
    }
}