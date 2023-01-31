using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterId;

public class RoomIdsSpecification : BaseSpecification<Room, string>
{
    public RoomIdsSpecification(string idFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(r => r.IsActive)
            .Where(r => r.Id.ToString().Contains(idFilterValue.Trim()))
            .OrderBy(r => r.Id)
            .Take(10);

        Query
            .Select(r => $"{r.Id}");
    }
}