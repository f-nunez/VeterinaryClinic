using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRoomsFilterName;

public class RoomNamesSpecification : BaseSpecification<Room, string>
{
    public RoomNamesSpecification(string nameFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(r => r.IsActive)
            .Where(r => r.Name.Trim().ToLower().Contains(
                nameFilterValue.Trim().ToLower()))
            .OrderBy(r => r.Name)
            .Take(10);

        Query
            .Select(r => r.Name);
    }
}