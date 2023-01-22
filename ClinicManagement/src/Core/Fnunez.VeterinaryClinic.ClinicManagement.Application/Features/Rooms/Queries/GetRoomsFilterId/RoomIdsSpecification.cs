using Fnunez.VeterinaryClinic.ClinicManagement.Domain.RoomAggregate;
using Fnunez.VeterinaryClinic.SharedKernel.Application.Specifications;

namespace Fnunez.VeterinaryClinic.ClinicManagement.Application.Features.Rooms.Queries.GetRoomsFilterId;

public class RoomIdsSpecification : BaseSpecification<Room, string>
{
    public RoomIdsSpecification(string idFilterValue)
    {
        Query
            .AsNoTracking()
            .Where(r => r.Id.ToString().Contains(idFilterValue.Trim()))
            .OrderBy(r => r.Id)
            .Take(10);

        Query
            .Select(r => $"{r.Id}");
    }
}