using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRooms;

public class RoomsOrderedByNameSpecification : BaseSpecification<Room>
{
    public RoomsOrderedByNameSpecification()
    {
        Query.OrderBy(room => room.Name);
    }
}