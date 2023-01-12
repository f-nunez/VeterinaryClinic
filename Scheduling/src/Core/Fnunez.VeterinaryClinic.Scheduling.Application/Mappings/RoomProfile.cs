using AutoMapper;
using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Mappings;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<Room, RoomDto>();
    }
}