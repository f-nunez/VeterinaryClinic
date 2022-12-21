using Fnunez.VeterinaryClinic.Scheduling.Application.SharedModel.Room.GetRooms;
using Fnunez.VeterinaryClinic.Scheduling.Domain.SyncedAggregates.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.Scheduling.Application.Features.Rooms.Queries.GetRooms;

public class RoomsCountSpecification : BaseSpecification<Room>
{
    public RoomsCountSpecification(GetRoomsRequest request)
    {
        Query.AsNoTracking();

        ApplyFilterId(request);

        ApplyFilterName(request);

        ApplyFilterSearch(request);
    }

    private void ApplyFilterId(GetRoomsRequest request)
    {
        if (string.IsNullOrEmpty(request.IdFilterValue))
            return;

        string idFilterValue = request.IdFilterValue.Trim();

        Query
            .Where(r => r.Id.ToString().Contains(idFilterValue));
    }

    private void ApplyFilterName(GetRoomsRequest request)
    {
        if (string.IsNullOrEmpty(request.NameFilterValue))
            return;

        string nameFilterValue = request.NameFilterValue.Trim().ToLower();

        Query
            .Where(r => r.Name.Trim().ToLower().Contains(nameFilterValue));
    }

    private void ApplyFilterSearch(GetRoomsRequest request)
    {
        if (string.IsNullOrEmpty(request.SearchFilterValue))
            return;

        string searchFilterValue = request.SearchFilterValue.Trim().ToLower();

        Query
            .Search(r => r.Id.ToString(), $"%{searchFilterValue}%")
            .Search(r => r.Name, $"%{searchFilterValue}%");
    }
}